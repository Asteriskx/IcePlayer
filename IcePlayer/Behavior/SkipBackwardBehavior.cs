﻿using System;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using IcePlayer.Views;
using Legato;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 巻き戻しボタンが押下された時の処理
	/// </summary>
	class SkipBackwardBehavior : Behavior<ToggleButton>
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
			// TODO : 巻き戻し処理実装に関しては、Legato 側の巻き戻し実装後に行う
			var window = (MainWindow)App.Current.MainWindow;
			window.status.Content = "Skipping Backward...";
		}

		#endregion Method
	}
}
