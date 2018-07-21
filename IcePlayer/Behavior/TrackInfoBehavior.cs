using System;
using System.Windows.Controls;
using System.Windows.Interactivity;
using IcePlayer.Views;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 再生中の曲情報を Flyout にて表示する処理
	/// </summary>
	class TrackInfoBehavior : Behavior<Button>
	{
		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public TrackInfoBehavior() { }

		#endregion Constructor

		#region Attach / Detaching

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += this._Open;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= this._Open;
		}

		#endregion Attach / Detaching

		#region Method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _Open(object sender, EventArgs e)
		{
			var window = (MainWindow)App.Current.MainWindow;
			window.flyout.IsOpen = true;
		}

		#endregion Method
	}
}
