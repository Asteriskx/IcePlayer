﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace IcePlayer.Models
{
    /// <summary>
    /// 音楽プレーヤーの基本実装を提供します。これは <c>abstract</c> クラスです。
    /// </summary>
    public abstract class IceMediaItemBase : iInterFace
    {
        /// <summary>
        /// <see cref="IcePlayer.Models.IceMediaItemBase"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected IceMediaItemBase() { }

        /// <summary>
        /// 指定したパスからすべてのバイトを非同期に読み取り、新しいストリームに書き込みます。
        /// </summary>
        /// <param name="path">ファイルの相対パスまたは絶対パス。</param>
        /// <returns>
        /// <para>非同期操作を表すタスク オブジェクト。</para>
        /// <para>タスク オブジェクトの <c>Result</c> プロパティは、<see cref="System.IO.Stream"/> を返します。</para>
        /// </returns>
        protected async Task<Stream> ReadFile( string path )
        {
            var stream = new MemoryStream();
            using (var file = new FileStream( path, FileMode.Open ))
            {
                await file.CopyToAsync( stream ).ConfigureAwait( false );
            }
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// 指定したパスが null 参照 (Visual Basic では Nothing) または空でない場合、<see cref="System.IO.FileInfo"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="path">パス。</param>
        /// <returns>作成された <see cref="System.IO.FileInfo"/> クラス、または null 参照 (Visual Basic では Nothing)。</returns>
        protected FileInfo GetFileInfo( string path ) => 
            string.IsNullOrEmpty( path ) ? null : new FileInfo( path );

        /// <summary>
        /// ガベージ コレクターがオブジェクトを破棄する前に、最後のクリーンアップを実行します。
        /// </summary>
        ~IceMediaItemBase()
        {
            Dispose( false );
        }

        /// <summary>
        /// 音楽プレーヤーのリソースが解放されているかどうかを示す値を取得します。
        /// </summary>
        public static bool IsDisposed { get; protected set; }

        /// <summary>
        /// 音楽プレーヤーに関連付けられたアンマネージ リソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// 音楽プレーヤーに関連付けられたアンマネージ リソースを解放します。
        /// </summary>
        /// <param name="disposing">明示的な破棄要求元から呼び出された場合は true に設定します。それ以外の場合は false。</param>
        protected abstract void Dispose( bool disposing );

        /// <summary>
        /// 非同期操作として現在音楽プレーヤーで再生している曲を取得します。
        /// </summary>
        /// <returns>
        /// <para>非同期操作を表すタスク オブジェクト。</para>
        /// <para>タスク オブジェクトの <c>Result</c> プロパティは、<see cref="IcePlayer.Models.IceMediaItem"/> を返します。</para>
        /// </returns>
        public abstract Task<IceMediaItem> GetCurrentMedia();

        /// <summary>
        /// 音楽プレーヤーの現在の再生状態を取得します。
        /// </summary>
        public abstract IceState PlayerState { get; }

        /// <summary>
        /// 音楽プレーヤーの現在の再生位置を取得または設定します。
        /// </summary>
        public abstract double CurrentPosition();

        /// <summary>
        /// 音楽プレーヤーの現在の再生音量を設定します。
        /// </summary>
        public abstract void CurrentVolume( double arg );

        /// <summary>
        /// 音楽プレーヤーの現在の再生音量を取得します。
        /// </summary>
        public abstract int GetVolume { get; }

        /// <summary>
        /// 音楽プレーヤーで再生を実行します。
        /// </summary>
        public abstract void Play();

        /// <summary>
        /// 音楽プレーヤーで早送りを実行します。
        /// </summary>
        public abstract void FastForward();

        /// <summary>
        /// 音楽プレーヤーで巻戻しを実行します。
        /// </summary>
        public abstract void Rewind();

        /// <summary>
        /// 音楽プレーヤーで停止を実行します。
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// 音楽プレーヤーで一時停止を実行します。
        /// </summary>
        public abstract void Pause();

        /// <summary>
        /// 音楽プレーヤーで次の曲の再生を実行します。
        /// </summary>
        public abstract void NextTrack();

        /// <summary>
        /// 音楽プレーヤーで前の曲の再生を実行します。
        /// </summary>
        public abstract void PreviousTrack();
    }
}