﻿using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 
	/// </summary>
	class NowPlayingBehavior : Behavior<Button>
	{
		#region Property

		/// <summary>
		/// Model 層 プロパティ
		/// </summary>
		public Models.Model Model { get; set; } = new Models.Model();

		#endregion Property

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public NowPlayingBehavior() { }

		#endregion Constructor

		#region Attach / Detaching

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += this._NowPlaying;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= this._NowPlaying;
		}

		#endregion Attach / Detaching

		#region Method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void _NowPlaying(object sender, EventArgs e) => await this.Model.PostingNowPlaying();

		#endregion Method
	}
}
