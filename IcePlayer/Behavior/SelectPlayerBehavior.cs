using Legato;
using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace IcePlayer.Behavior
{
	/// <summary>
	///  プレイヤーを選択します
	/// </summary>
	class SelectPlayerBehavior : Behavior<Button>
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
			this.AssociatedObject.Click += SelectPlayer;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= SelectPlayer;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectPlayer(object sender, EventArgs e)
		{
			if (!_Properties.IsRunning)
			{
				this._Commands.StartAimp();
			}
		}
	}
}
