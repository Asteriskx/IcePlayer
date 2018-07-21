using System;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using IcePlayer.Views;
using Legato;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 音量ボタンが押下された時の処理
	/// </summary>
	class VolumeBehavior : Behavior<ToggleButton>
	{
		#region Field

		/// <summary>
		/// 音量保持用変数
		/// </summary>
		private int[] saveVolume = new int[2] { 0, 0 };

		#endregion Field

		#region Property

		/// <summary>
		/// 
		/// </summary>
		private AimpCommands _Commands { get; set; } = new AimpCommands();

		#endregion Property

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public VolumeBehavior() { }

		#endregion Constructor

		#region Attach / Detaching

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += GetVolume;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= GetVolume;
		}

		#endregion Attach / Detaching

		#region Method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GetVolume(object sender, EventArgs e)
		{
			// 音量取得処理に関しては、Legato 側の実装が行われ次第。
			const int MuteVol = 0;
			var window = (MainWindow)App.Current.MainWindow;
			var tgl = (ToggleButton)sender;
			var beforeVol = (int)window.VolumeBar.Value;

			saveVolume[0] = beforeVol;

			if ((bool)tgl.IsChecked)
			{
				// 値の保持
				saveVolume[1] = saveVolume[0];

				window.ShowVolume.Content = MuteVol;
				window.VolumeBar.Value = MuteVol;
				window.status.Content = "Muted...";
			}
			else
			{
				// TODO : 現在の音量を設定
				window.ShowVolume.Content = saveVolume[1];
				window.VolumeBar.Value = saveVolume[1];
				window.status.Content = "unMuted...";
			}
		}

		#endregion Method
	}
}
