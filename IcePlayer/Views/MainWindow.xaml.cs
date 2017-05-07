using IcePlayer.Models;
using MahApps.Metro.Controls;

using System.Windows;
using System.Threading;
using System.Windows.Controls.Primitives;


namespace IcePlayer.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// iTunes クラスインスタンス
        /// </summary>
        private iTunes _iPlayer;

        /// <summary>
        /// スプラッシュウィンドウの表示時間
        /// </summary>
        private static readonly int WaitSplash = 3000;

        /// <summary>
        /// クラス多重生成防止フラグ
        /// </summary>
        private bool IncludeGuard;

        /// <summary>
        /// MainWindow コンストラクタ
        /// </summary>
        public MainWindow()
        {
            // Initialize
            InitializeComponent();

            // スプラッシュウィンドウの待ち時間
            Thread.Sleep( WaitSplash );

            // IcePlayer のウィンドウ移動(ドラッグ) を可能にする
            this.MouseLeftButtonDown += (sender, e) => this.DragMove();

            // クラス多重生成防止フラグを false に
            this.IncludeGuard = false;

        }

        #region ボタン コードビハインド
        private void iTunes_Click(object sender, RoutedEventArgs e)
        {
            // 1回きりの√
            if (false == IncludeGuard)
            {
                // iTunes インスタンスを生成
                this._iPlayer = iTunes.GetInstance();

                // iTunes 起動時、オーディオプレーヤーのデフォルト音量を設定
                this.VolumeBar.Value = _iPlayer.GetVolume;

                IncludeGuard = true;
            }
            else
            {
                // Nothing.
            }

            CurrentTrackTime.Value = _iPlayer.CurrentPosition();
        }

        /// <summary>
        /// 再生中の曲情報を Flyout にて表示する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackInfo_Click(object sender, RoutedEventArgs e)
        {
            this.flyout.IsOpen = true;
        }

        /// <summary>
        /// 再生ボタンが押下された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton tgl = (ToggleButton)sender;

            if( true == tgl.IsChecked )
            {
                _iPlayer.Play();
                status.Content = "Playing...";
            }
            else
            {
                _iPlayer.Pause();
                status.Content = "Pausing...";
            }
        }

        /// <summary>
        /// 巻き戻しボタンが押下された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkipBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            status.Content = "Skipping Backward...";
            _iPlayer.PreviousTrack();
        }

        /// <summary>
        /// 早送りボタンが押下された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FastForwardButton_Click(object sender, RoutedEventArgs e)
        {
            status.Content = "FastForward...";
            _iPlayer.FastForward();
        }

        /// <summary>
        /// スキップボタンが押下された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkipForwardButton_Click(object sender, RoutedEventArgs e)
        {
            status.Content = "Skipping Forward...";
            _iPlayer.NextTrack();
        }

        /// <summary>
        /// 前トラックに戻したい時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RewindButton_Click(object sender, RoutedEventArgs e)
        {
            status.Content = "Rewind...";
            _iPlayer.Rewind();
        }

        /// <summary>
        /// 音量ボタンが押下された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolumeButton_Click(object sender, RoutedEventArgs e)
        {
            const int    MuteVol       = 0;
            ToggleButton tgl           = sender as ToggleButton;
            int          beforeVol     = ( int )VolumeBar.Value;

            if (true == tgl.IsChecked)
            {
                _iPlayer.CurrentVolume( MuteVol );
                status.Content = "Muted...";
            }
            else
            {
                _iPlayer.CurrentVolume( beforeVol );
                status.Content = "UnMuted...";
            }
        }

        /// <summary>
        /// 音量調節用スライダーの値が変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _iPlayer.CurrentVolume( VolumeBar.Value );
            ShowVolume.Content = ( int )VolumeBar.Value;

            // Console.WriteLine($"Volume value ; {VolumeBar.Value}");
        }

        /// <summary>
        /// 再生中の曲の経過時間を表すプログレスバー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //CurrentTrackTime.Value = _iPlayer.CurrentPosition();
            //Console.WriteLine($"Time ; {CurrentTrackTime.Value}");
        }
        #endregion
    }
}
