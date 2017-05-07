using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using iTunesLib;
using IcePlayer.Interop;

using static System.Console;

namespace IcePlayer.Models
{
    /// <summary>
    /// iTunes の機能を提供します。
    /// </summary>
    public class iTunes : IceMediaItemBase, INotifyPlayerStateChanged
    {
        /// <summary>
        /// iTunes が使用するプロセス名。
        /// </summary>
        public static readonly string ProcessName = "iTunes";

        /// <summary>
        /// iTunes クラスをシングルトンパターンに適用させる。
        /// </summary>
        private static readonly iTunes _iSingleInstance = new iTunes();

        /// <summary>
        /// COM Wrapper インスタンス。
        /// </summary>
        private static ComWrapper<iTunesApp> _player;

        /// <summary>
        /// iTunes 起動時、再生の曲を Twitter に投稿する為のクラスインスタンス。
        /// </summary>
        private NowPlaying _np;

        /// <summary>
        /// iTunes で再生している曲の現在時間。
        /// </summary>
        private double CurrentTime; 

        /// <summary>
        /// iTunes の <see cref="iTunesLib.iTunesApp"/> への COM ラッパーを取得します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        protected ComWrapper<iTunesApp> Player
        {
            get
            {
                try
                {
                    _player = _player ?? ComWrapper.Create( new iTunesApp() );
                    IsDisposed = false;
                    return ( _player );
                }

                catch ( TypeInitializationException )
                {
                    throw;
                }

                catch ( Exception ex )
                {
                    throw new TypeInitializationException( typeof( iTunesApp ).FullName, ex );
                }
            }
        }

        /// <summary>
        /// <see cref="IcePlayer.Models.iTunes"/> の新しいインスタンスを作成し、iTunes を初期化します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        private iTunes()
        {
            if ( _player == null )
            {
                Player.Object.OnPlayerPlayEvent += OnCurrentMediaChanged;
                Player.Object.OnAboutToPromptUserToQuitEvent += OnClosed;
            }

            _np         = new NowPlaying();
            CurrentTime = 0.0F;
        }

        /// <summary>
        /// 唯一のインスタンス受け渡し
        /// </summary>
        /// <returns></returns>
        public static iTunes GetInstance()
        {
            return _iSingleInstance;
        }

        /// <summary>
        /// <see cref="IcePlayer.Models.iTunes.CurrentMediaChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="iTrack">再生中の曲。</param>
        protected async void OnCurrentMediaChanged( object iTrack )
        {
            var track = iTrack as IITTrack;

            if ( track != null )
                CurrentMediaChanged?.Invoke( this, new CurrentMediaChangedEventArgs( await GetCurrentMedia( track ) ) );
        }

        /// <summary>
        /// <see cref="IcePlayer.Models.iTunes.Closed"/> イベントを発生させます。
        /// </summary>
        protected void OnClosed()
        {
            Dispose();
            Closed?.Invoke( this, EventArgs.Empty );
        }

        /// <summary>
        /// アートワーク取得(非同期)
        /// </summary>
        /// <param name="artwork"></param>
        /// <returns></returns>
        private Task<Stream> GetArtwork( IITArtwork artwork )
        {
            using ( ComWrapper.Create( artwork ) )
            {
                string path = Path.GetTempFileName();
                artwork.SaveArtworkToFile( path );

                return ReadFile( path ).ContinueWith(task =>
                {
                    File.Delete( path );
                    return task.Result;
                });
            }
        }

        private Task<Stream[]> GetArtworks( IITArtworkCollection artworks ) => 
            Task.WhenAll(artworks.OfType<IITArtwork>().Select( GetArtwork ));

        /// <summary>
        /// 再生中の曲情報の取得(非同期)
        /// </summary>
        /// <param name="currentTrack"></param>
        /// <returns></returns>
        private async Task<IceMediaItem> GetCurrentMedia( IITTrack currentTrack )
        {
            if ( currentTrack == null )
                return null;

            using ( ComWrapper.Create( currentTrack ) )
            using (var artworks = ComWrapper.Create( currentTrack.Artwork ))
            {
                var media = new IceMediaItem
                {
                    Album            = currentTrack.Album ?? "",
                    AlbumArtist      = currentTrack.Artist ?? "",
                    Artist           = currentTrack.Artist ?? "",
                    BitRate          = currentTrack.BitRate * 1000,
                    Category         = ( ( dynamic )currentTrack ).Category ?? "",
                    Composer         = currentTrack.Composer ?? "",
                    DateAdded        = currentTrack.DateAdded,
                    Duration         = TimeSpan.FromSeconds(currentTrack.Duration),
                    FileInfo         = GetFileInfo( ( ( dynamic )currentTrack).Location ),
                    Genre            = currentTrack.Genre ?? "",
                    Kind             = currentTrack.KindAsString ?? "",
                    Lyrics           = ( ( dynamic )currentTrack ).Lyrics ?? "",
                    Name             = currentTrack.Name ?? "",
                    PlayedCount      = currentTrack.PlayedCount,
                    PlayedDate       = currentTrack.PlayedDate,
                    TrackNumber      = currentTrack.TrackNumber,
                    Year             = currentTrack.Year,
                    VolumeAdjustment = currentTrack.VolumeAdjustment
                };
                media.AlbumArtist    = ( ( dynamic )currentTrack ).AlbumArtist ?? media.Artist ?? "";
                media.Artworks       = new Collection<Stream>( await GetArtworks(artworks.Object ).ConfigureAwait( false ));

                // 再生中の曲を Twitter に呟く
                _np.Tweet( media.Name, media.Album, media.Artist, media.PlayedCount );

                return ( media );
            }
        }

        /// <summary>
        /// 非同期操作として現在 iTunes で再生している曲を取得します。
        /// </summary>
        /// <returns>
        /// <para>非同期操作を表すタスク オブジェクト。</para>
        /// <para>タスク オブジェクトの <c>Result</c> プロパティは、<see cref="IcePlayer.Models.IceMediaItem"/> を返します。</para>
        /// </returns>
        public override Task<IceMediaItem> GetCurrentMedia() => GetCurrentMedia( Player.Object.CurrentTrack );

        /// <summary>
        /// iTunes に関連付けられたアンマネージ リソースを解放します。
        /// </summary>
        /// <param name="disposing">明示的な破棄要求元から呼び出された場合は true に設定します。それ以外の場合は false。</param>
        protected override void Dispose( bool disposing )
        {
            if ( IsDisposed )
                return;

            if ( _player != null && disposing )
            {
                _player.Object.OnPlayerPlayEvent -= OnCurrentMediaChanged;
                _player.Object.OnAboutToPromptUserToQuitEvent -= OnClosed;
                _player.Dispose();
            }

            _player    = null;
            IsDisposed = true;
        }

        /// <summary>
        /// iTunes の現在の再生状態を取得します。
        /// </summary>
        public override IceState PlayerState
        {
            get
            {
                switch (Player.Object.PlayerState)
                {
                    case ITPlayerState.ITPlayerStateFastForward:
                        return IceState.FastForward;

                    case ITPlayerState.ITPlayerStatePlaying:
                        return IceState.Playing;

                    case ITPlayerState.ITPlayerStateRewind:
                        return IceState.Rewind;

                    case ITPlayerState.ITPlayerStateStopped:
                        return IceState.Stopped;

                    default:
                        return IceState.Unknown;
                }
            }
        }

        /// <summary>
        /// iTunes の現在の再生位置を取得または設定します。
        /// </summary>
        public override double CurrentPosition()
        {
            try
            {
                if ( this.PlayerState != IceState.Stopped )
                    CurrentTime =  Player.Object.PlayerPosition;
                else
                    return 0;
            }
            catch
            {
                if ( this.PlayerState != IceState.Stopped )
                    return Player.Object.PlayerPosition;
            }
            return ( CurrentTime );
        }

        /// <summary>
        /// 音楽プレーヤーの現在の再生音量を取得または設定します。
        /// </summary>
        public override void CurrentVolume( double arg )
        {
            try
            {
                if ( this.PlayerState != IceState.Stopped )
                    Player.Object.SoundVolume = ( int )arg;
            }
            catch ( Exception ex )
            {
                WriteLine( ex.StackTrace );
            }
            return;
        }

        /// <summary>
        /// 音楽プレーヤーの現在の再生音量を取得します。
        /// </summary>
        public override int GetVolume
        {
            get { return Player.Object.SoundVolume; }
        }

        /// <summary>
        /// iTunes で再生を実行します。
        /// </summary>
        public override void Play() => Player.Object.Play();

        /// <summary>
        /// iTunes で早送りを実行します。
        /// </summary>
        public override void FastForward() => Player.Object.FastForward();

        /// <summary>
        /// iTunes で巻戻しを実行します。
        /// </summary>
        public override void Rewind() => Player.Object.Rewind();

        /// <summary>
        /// iTunes で停止を実行します。
        /// </summary>
        public override void Stop() => Player.Object.Stop();

        /// <summary>
        /// iTunes で一時停止を実行します。
        /// </summary>
        public override void Pause() => Player.Object.Pause();

        /// <summary>
        /// iTunes で次の曲の再生を実行します。
        /// </summary>
        public override void NextTrack() => Player.Object.NextTrack();

        /// <summary>
        /// iTunes で前の曲の再生を実行します。
        /// </summary>
        public override void PreviousTrack() => Player.Object.PreviousTrack();

        /// <summary>
        /// iTunes で再生中の曲が変更されたときに発生します。
        /// </summary>
        public event CurrentMediaChangedEventHandler CurrentMediaChanged;

        /// <summary>
        /// iTunes が終了された時に発生します。
        /// </summary>
        public event EventHandler Closed;
    }
}
