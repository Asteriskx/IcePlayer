using System;

namespace IcePlayer.Models
{
    /// <summary>
    /// 音楽プレーヤーで再生中の曲が変更されたときに発生する <see cref="IcePlayer.Models.INotifyPlayerStateChanged.CurrentMediaChanged"/> イベントを処理するメソッドを表します。
    /// </summary>
    /// <param name="sender">イベントのソース。</param>
    /// <param name="e">イベント データを格納している <see cref="IcePlayer.Models.CurrentMediaChangedEventArgs"/>。</param>
    public delegate void CurrentMediaChangedEventHandler( object sender, CurrentMediaChangedEventArgs e );

    /// <summary>
    /// <see cref="IcePlayer.INotifyPlayerStateChanged.CurrentMediaChanged"/> イベントにデータを提供します。
    /// </summary>
    public class CurrentMediaChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 曲情報を取得します。
        /// </summary>
        public IceMediaItem CurrentMedia { get; }

        /// <summary>
        /// <see cref="IcePlayer.Models.CurrentMediaChangedEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="currentMedia">曲情報。</param>
        public CurrentMediaChangedEventArgs( IceMediaItem currentMedia )
        {
            this.CurrentMedia = currentMedia;
        }
    }
}
