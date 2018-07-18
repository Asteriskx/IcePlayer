using AlbumArtExtraction;
using Legato;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
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
		/// AlbumArtExtractor 経由でアルバムアートを取得します。
		/// </summary>
		/// <returns> 取得されたアルバムアート(BitmapImage) </returns>
		public BitmapImage GetAlbumArt()
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
					var image = new BitmapImage();

					image.BeginInit();
					image.CacheOption = BitmapCacheOption.OnLoad;
					image.CreateOptions = BitmapCreateOptions.None;

					image.StreamSource = ms;
					image.EndInit();
					image.Freeze();

					return image;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("album art extraction error:");
				Console.WriteLine(ex);
				return null;
			}
		}

		/// <summary>
		///  非同期にて曲の再生時間を取得します
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public async Task<string> GetCurrentTrackPositionAsync(int position)
		{
			var totalSec = position / 1000;
			var min = totalSec / 60;
			var sec = totalSec % 60;
			return $"{min:D2}:{sec:D2}";
		}

		#endregion Methods

	}
}
