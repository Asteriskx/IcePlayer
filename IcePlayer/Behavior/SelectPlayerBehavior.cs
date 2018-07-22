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
		public SelectPlayerBehavior() { }

		#endregion Constuctor

		#region Attach / Detaching

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += this._SelectPlayer;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= this._SelectPlayer;
		}

		#endregion Attach / Detaching

		#region Method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _SelectPlayer(object sender, EventArgs e)
		{
			this._Commands.StartAimp();
			new Models.Model().DoEvents();
		}

		#endregion Method
	}
}
