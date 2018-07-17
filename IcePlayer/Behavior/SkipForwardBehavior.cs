using System;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using IcePlayer.Views;
using Legato;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// スキップボタンが押下された時の処理
	/// </summary>
	class SkipForwardBehavior : Behavior<ToggleButton>
	{
		/// <summary>
		/// 
		/// </summary>
		private AimpCommands _Commands { get; set; } = new AimpCommands();

		/// <summary>
		/// 
		/// </summary>
		private AimpProperties _Properties { get; set; } = new AimpProperties();

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += SkipForward;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= SkipForward;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SkipForward(object sender, EventArgs e)
		{
			if (_Properties.IsRunning) 
			{
				this._Commands.Next();
			}
			var window = (MainWindow)App.Current.MainWindow;
			window.status.Content = "Skipping Forward...";
		}
	}
}
