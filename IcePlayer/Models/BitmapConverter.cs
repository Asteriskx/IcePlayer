using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace IcePlayer.Models
{
	/// <summary>
	/// Bitmap を BitmapSource へ Convert するための拡張クラス
	/// </summary>
	public static class BitmapConverterExtensions
	{

		#region Extern

		/// <summary>
		/// オブジェクトの解放メソッド
		/// </summary>
		/// <param name="hObject"></param>
		/// <returns></returns>
		[DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject([In] IntPtr hObject);

		#endregion Extern

		#region Methods

		/// <summary>
		/// Convert メソッド
		/// </summary>
		/// <param name="bmp"></param>
		/// <returns></returns>
		public static BitmapSource ToImageSource(this Bitmap bmp)
		{
			var handle = bmp.GetHbitmap();
			try
			{
				var convert = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
				return convert;
			}
			finally { DeleteObject(handle); }
		}

		#endregion Methods

	}
}
