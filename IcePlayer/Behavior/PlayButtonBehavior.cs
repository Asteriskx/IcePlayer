using IcePlayer.Views;
using Legato;
using System;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace IcePlayer.Behavior
{
	/// <summary>
	///  プレイヤーを再生します
	/// </summary>
	class PlayButtonBehavior : Behavior<ToggleButton>
	{
		/// <summary>
		/// 
		/// </summary>
		private AimpCommands _Commands { get; set; } = new AimpCommands();

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += Play;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= Play;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Play(object sender, EventArgs e)
		{
			var tgl = (ToggleButton)sender;

			if ((bool)tgl.IsChecked)
			{
				this._Commands.Play();
			}
			else
			{
				this._Commands.Pause();
			}

			var window = (MainWindow)App.Current.MainWindow;
			window.status.Content = ((bool)tgl.IsChecked) ? "Playing..." : "Pausing...";
		}
	}
}
