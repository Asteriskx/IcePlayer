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
			if (_Properties.IsRunning)
			{
				this._Commands.Prev();
			}
			var window = (MainWindow)App.Current.MainWindow;
			window.status.Content = "Rewind...";
		}

		#endregion Method
	}
}
