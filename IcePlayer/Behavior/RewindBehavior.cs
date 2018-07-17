using System;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using IcePlayer.Views;
using Legato;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 前トラックに戻したい時の処理
	/// </summary>
	class RewindBehavior : Behavior<ToggleButton>
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
			this.AssociatedObject.Click += Rewind;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= Rewind;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Rewind(object sender, EventArgs e)
		{
			this._Commands.Prev();
			var window = (MainWindow)App.Current.MainWindow;
			window.status.Content = "Rewind...";
		}
	}
}
