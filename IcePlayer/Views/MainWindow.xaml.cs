using System;
using System.Windows;

using IcePlayer.Models;
using IcePlayer.ViewModels;
using System.Threading;

using MahApps.Metro.Controls;
using MahApps.Metro;
using System.Windows.Controls.Primitives;

namespace IcePlayer.Views
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		/// <summary>
		/// スプラッシュウィンドウの表示時間
		/// </summary>
		private static readonly int WaitSplash = 2000;

		/// <summary>
		/// 音量保持用変数
		/// </summary>
		private int[] saveVolume = new int[2] { 0, 0 };

		/// <summary>
		/// MainWindow コンストラクタ
		/// </summary>
		public MainWindow()
		{
			// Initialize
			InitializeComponent();

			// スプラッシュウィンドウの待ち時間
			Thread.Sleep(WaitSplash);

			// IcePlayer のウィンドウ移動(ドラッグ) を可能にする
			this.MouseLeftButtonDown += (sender, e) => this.DragMove();
		}

		#region Methods

		/// <summary>
		/// プレイヤーを選択します
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectPlayer_Click(object sender, RoutedEventArgs e)
		{
		}

		/// <summary>
		/// 再生中の曲情報を Flyout にて表示する処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TrackInfo_Click(object sender, RoutedEventArgs e) => this.flyout.IsOpen = true;

		/// <summary>
		/// 再生ボタンが押下された時の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlayButton_Click(object sender, RoutedEventArgs e)
		{
			ToggleButton tgl = (ToggleButton)sender;

			// TODO : 再生・停止の動作
			status.Content = ((bool)tgl.IsChecked) ? "Playing..." : "Pausing...";
		}

		/// <summary>
		/// 巻き戻しボタンが押下された時の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SkipBackwardButton_Click(object sender, RoutedEventArgs e)
		{
			// TODO : 巻き戻し時の動作
			status.Content = "Skipping Backward...";
		}

		/// <summary>
		/// 早送りボタンが押下された時の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FastForwardButton_Click(object sender, RoutedEventArgs e)
		{
			// TODO : 早送り時の動作
			status.Content = "FastForward...";
		}

		/// <summary>
		/// スキップボタンが押下された時の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SkipForwardButton_Click(object sender, RoutedEventArgs e)
		{
			// TODO : スキップ時の動作
			status.Content = "Skipping Forward...";
		}

		/// <summary>
		/// 前トラックに戻したい時の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RewindButton_Click(object sender, RoutedEventArgs e)
		{
			// TODO : 戻し時の動作
			status.Content = "Rewind...";
		}

		/// <summary>
		/// 音量ボタンが押下された時の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VolumeButton_Click(object sender, RoutedEventArgs e)
		{
			const int MuteVol = 0;
			ToggleButton tgl = (ToggleButton)sender;
			int beforeVol = (int)VolumeBar.Value;

			saveVolume[0] = beforeVol;

			if ((bool)tgl.IsChecked)
			{
				// 値の保持
				saveVolume[1] = saveVolume[0];

				ShowVolume.Content = MuteVol;
				VolumeBar.Value = MuteVol;
				status.Content = "Muted...";
			}
			else
			{
				// TODO : 現在の音量を設定
				ShowVolume.Content = saveVolume[1];
				VolumeBar.Value = saveVolume[1];
				status.Content = "unMuted...";
			}
		}

		/// <summary>
		/// 音量調節用スライダーの値が変更された時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			// TODO : 現在の音量を取得
			ShowVolume.Content = (int)VolumeBar.Value;
		}

		/// <summary>
		/// 再生中の曲の経過時間を表すプログレスバー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MetroProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
		}

		#endregion
	}
}
