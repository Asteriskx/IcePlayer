using System;
using System.Windows.Controls;
using System.Windows.Interactivity;
using IcePlayer.Views;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 前トラックに戻したい時の処理
	/// </summary>
	class RewindBehavior : Behavior<Button>
	{
		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public RewindBehavior() { }

		#endregion Constructor

		#region Attach / Detaching

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += this._Rewind;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= this._Rewind;
		}

		#endregion Attach / Detaching

		#region Method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _Rewind(object sender, EventArgs e)
		{
			var window = (MainWindow)App.Current.MainWindow;
			window.status.Content = "Rewind...";
		}

		#endregion Method
	}
}
