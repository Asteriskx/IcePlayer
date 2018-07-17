using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IcePlayer.Models
{
	/// <summary>
	/// 
	/// </summary>
	public static class BitmapConverterExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="hObject"></param>
		/// <returns></returns>
		[DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject([In] IntPtr hObject);

		/// <summary>
		/// 
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
	}
}
