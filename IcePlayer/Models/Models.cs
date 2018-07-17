using AlbumArtExtraction;
using Legato;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;

namespace IcePlayer.Models
{
	/// <summary>
	/// IcePlayer Model 層 管理クラス
	/// </summary>
	public class Models
	{

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public AimpProperties Properties { get; set; } = new AimpProperties();

		/// <summary>
		/// 
		/// </summary>
		public AimpObserver Observer { get; set; } = new AimpObserver();

		#endregion Properties

		#region Constractor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Models() { }

		#endregion Constractor

		#region Methods

		/// <summary>
		/// AlbumArtExtractor 経由でアートワークを取得します。
		/// </summary>
		/// <returns></returns>
		public BitmapImage GetArtwork()
		{
			try
			{
				var trackFilePath = Properties.CurrentTrack.FilePath;
				var selector = new Selector();
				var extractor = selector.SelectAlbumArtExtractor(trackFilePath);
				var source = extractor.Extract(trackFilePath);
				var bitmap = new Bitmap(source);

				using (var ms = new System.IO.MemoryStream())
				{
					bitmap.Save(ms, ImageFormat.Bmp);
					ms.Position = 0;
					var bitmapImage = new BitmapImage();

					bitmapImage.BeginInit();
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.CreateOptions = BitmapCreateOptions.None;

					bitmapImage.StreamSource = ms;
					bitmapImage.EndInit();
					bitmapImage.Freeze();

					return bitmapImage;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("album art extraction error:");
				Console.WriteLine(ex);
				return null;
			}
		}

		#endregion Methods

	}
}
