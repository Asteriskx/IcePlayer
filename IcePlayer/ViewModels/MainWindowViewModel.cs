using Livet;
using Livet.Commands;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using IcePlayer.Models;

namespace IcePlayer.ViewModels
{
    /// <summary>
    /// ViewModel 層の定義クラスです。
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        #region Artwork 変更通知プロパティ

        private BitmapImage _artwork;
        public BitmapImage Artwork
        {
            get { return _artwork; }
            set
            {
                _artwork = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region IceMediaItem 変更通知プロパティ

        private IceMediaItem _mediaItem;
        public IceMediaItem MediaItem
        {
            get { return _mediaItem; }
            set
            {
                _mediaItem = value;
                SetArtwork(value);
                RaisePropertyChanged();
            }
        }

        #endregion

        #region PlayerName 変更通知プロパティ

        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region IsMenuShown 変更通知プロパティ

        private bool _isMenuShown;
        public bool IsMenuShown
        {
            get { return _isMenuShown; }
            set
            {
                _isMenuShown = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region ErrorMessage 変更通知プロパティ

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public iInterFace Player { get; set; }

        /// <summary>
        /// アートワークがない時のデフォルト画像のパス位置。
        /// 表示したい画像のパスを指定すること。
        /// </summary>
        private static readonly string DefaultImage = 
            "※アートワークがない場合に、表示させたい画像を指定すること。";

        /// <summary>
        /// アートワークのセットを行います。
        /// </summary>
        /// <param name="value"></param>
        private void SetArtwork( IceMediaItem value )
        {
            if ( value == null || value.Artworks.FirstOrDefault() == null )
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(DefaultImage);
                image.EndInit();
                image.Freeze();
                this.Artwork = image;
                return;
            }

            try
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = value.Artworks.FirstOrDefault();
                image.EndInit();
                image.Freeze();
                this.Artwork = image;

                // BitmapSourceを保存する
                // Twitter に ジャケット画像を投稿するために、ここで画像を生成しておく。
                using (Stream stream = new FileStream( "artworks.jpg", FileMode.Create ))
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add( BitmapFrame.Create( image ));
                    encoder.Save(stream);
                }
            }

            catch ( FileNotFoundException ne )
            {
                Console.WriteLine( ne.StackTrace );
            }

            catch ( NotSupportedException ne )
            {
                Console.WriteLine( ne.StackTrace );
            }

            catch ( FileFormatException fe )
            {
                Console.WriteLine( fe.StackTrace );
            }

            catch ( IOException e )
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri( DefaultImage );
                image.EndInit();
                image.Freeze();
                this.Artwork = image;
                Console.WriteLine( e.StackTrace );
            }
        }    

        /// <summary>
        /// 初期化を行います。
        /// </summary>S
        public void Initialize()
        {
            this.IsMenuShown = !this.IsMenuShown;
            return;
        }

        #region SelectPlayerCommand

        private ListenerCommand<string> _selectPlayerCommand;

        public ListenerCommand<string> SelectPlayerCommand => 
            _selectPlayerCommand ?? (_selectPlayerCommand = new ListenerCommand<string>(SelectPlayer));

        #endregion

        /// <summary>
        /// プレイヤーの選択をします。
        /// ※現在は iTunes だけですが、他プレイヤーの実装準備も行っています。
        /// </summary>
        /// <param name="parameter"></param>
        public async void SelectPlayer(string parameter)
        {
            this.IsMenuShown = false;
            iInterFace player;

            try
            {
                this.PlayerName   = "Waiting...";
                this.ErrorMessage = null;
                this.MediaItem    = null;

                switch ( parameter )
                {
                    case "iTunes":
                        player = await Task.Run(() => iTunes.GetInstance());
                        break;

                    default:
                        return;
                }

                var p = player as INotifyPlayerStateChanged;
                if ( p != null )
                {
                    p.CurrentMediaChanged += SetCurrentMedia;
                    p.Closed += ResetPlayer;
                }
                SetCurrentMedia(player, new CurrentMediaChangedEventArgs( await player.GetCurrentMedia() ) );

                this.Player     = player;
                this.PlayerName = parameter;
            }
            catch ( Exception ex )
            {
                this.PlayerName   = "Not Found...";
                this.MediaItem    = null;
                this.ErrorMessage = ex.GetType().FullName + "\n" + ex.Message;
            }
        }

        /// <summary>
        /// 現在再生中の曲をセットします。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCurrentMedia(object sender, CurrentMediaChangedEventArgs e)
        {
            this.MediaItem = e.CurrentMedia;
            return;
        }

        /// <summary>
        /// プレイヤーのリセットを行います。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetPlayer(object sender, EventArgs e)
        {
            this.MediaItem  = null;
            this.PlayerName = null;
            return;
        }
    }
}