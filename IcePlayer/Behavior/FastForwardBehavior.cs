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
		public FastForwardBehavior() { }

		#endregion Constructor

		#region Attach / Detaching

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += this._FastForward;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= this._FastForward;
		}

		#endregion Attach / Detacing

		#region Method

		/// <summary>
		/// 早送り処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _FastForward(object sender, EventArgs e)
		{
			// TODO : 早送り処理実装に関しては、Legato 側の早送り実装後に行う
			var window = (MainWindow)App.Current.MainWindow;
			window.status.Content = "FastForward...";
		}

		#endregion Method
	}
}