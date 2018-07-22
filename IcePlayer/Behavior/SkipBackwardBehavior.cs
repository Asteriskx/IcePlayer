using System;
using System.Windows.Controls;
using System.Windows.Interactivity;
using IcePlayer.Views;
using Legato;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 曲戻しボタンが押下された時の処理
	/// </summary>
	class SkipBackwardBehavior : Behavior<Button>
	{
		#region Properties

		/// <summary>
		/// 
		/// </summary>
		private AimpCommands _Commands { get; set; } = new AimpCommands();

		/// <summary>
		/// 
		/// </summary>
		private AimpProperties _Properties { get; set; } = new AimpProperties();

		#endregion Properties

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public SkipBackwardBehavior() { }

		#endregion Constructor

		#region Attach / Detaching

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += this._BackWord;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= this._BackWord;
		}

		#endregion Attach / Detaching

		#region Method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _BackWord(object sender, EventArgs e)
		{
			if (_Properties.IsRunning)
			{
				this._Commands.Prev();
			}
			var window = (MainWindow)App.Current.MainWindow;
			window.status.Content = "Skipping Backward...";
		}

		#endregion Method
	}
}
