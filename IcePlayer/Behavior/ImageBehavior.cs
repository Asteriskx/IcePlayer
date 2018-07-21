using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 画像の処理管理クラス
	/// </summary>
	class ImageBehavior : Behavior<Image>
	{
		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public ImageBehavior() { }

		#endregion Constructor

		#region Attach / Detaching

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.MouseDown += this._ShowImage;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.MouseDown -= this._ShowImage;
		}

		#endregion Attach / Detaching

		#region Method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _ShowImage(object sender, MouseButtonEventArgs e) =>
			new Models.Model().ShowAlbumArtWithViewer(e);

		#endregion Method
	}
}
