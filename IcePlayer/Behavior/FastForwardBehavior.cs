using System;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using IcePlayer.Views;
using Legato;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 早送りボタンが押下された時の処理
	/// </summary>
	class FastForwardBehavior : Behavior<ToggleButton>
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
			this.AssociatedObject.Click += FastForward;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= FastForward;
		}

		/// <summary>
		/// 早送り処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FastForward(object sender, EventArgs e)
		{
			// TODO : 早送り処理実装に関しては、Legato 側の早送り実装後に行う
			var window = (MainWindow)App.Current.MainWindow;
			window.status.Content = "FastForward...";
		}
	}
}