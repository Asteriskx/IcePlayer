using AlbumArtExtraction;
using IcePlayer.Exceptions;
using IcePlayer.Properties;
using Legato;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace IcePlayer.Models
{
	/// <summary>
	/// IcePlayer Model 層 管理クラス
	/// </summary>
	public class Model
	{
		#region Field

		#region API Keys

		/// <summary>
		/// Consumer Key
		/// </summary>
		private static readonly string _ck = "your consumer key";

		/// <summary>
		/// Consumer Secret
		/// </summary>
		private static readonly string _cs = "your consumer secret";

		/// <summary>
		/// Access Token
		/// </summary>
		private static readonly string _at = "your access token";

		/// <summary>
		/// Access Token Secret
		/// </summary>
		private static readonly string _ats = "your access token secret";

		#endregion API Keys

		#endregion Field

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public AimpProperties Properties { get; set; } = new AimpProperties();

		/// <summary>
		/// 
		/// </summary>
		public AimpObserver Observer { get; set; } = new AimpObserver();

		/// <summary>
		/// 
		/// </summary>
		private Twist.Twitter _Twitter { get; set; } = new Twist.Twitter(_ck, _cs, _at, _ats, new HttpClient(new HttpClientHandler()));

		#endregion Properties

		#region Constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Model() { }

		#endregion Constructor

		#region Methods

		/// <summary>
		/// AlbumArtExtractor 経由でアルバムアートを取得します
		/// </summary>
		/// <returns> 取得されたアルバムアート(BitmapImage) </returns>
		public BitmapImage GetAlbumArt()
		{
			try
			{
				using (var ms = new MemoryStream())
				{
					var bitmap = this.GetBitmap();
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
			catch (Exception e)
			{
				new IcePlayerException("album art extraction error:", e);

				var image = new BitmapImage();
				image.BeginInit();
				image.CacheOption = BitmapCacheOption.OnLoad;
				image.CreateOptions = BitmapCreateOptions.None;

				image.UriSource = new Uri("pack://application:,,,/None.png", UriKind.Absolute);
				image.EndInit();
				image.Freeze();

				return image;
			}
		}

		/// <summary>
		/// アルバムアート：Stream の状態 を取得します
		/// </summary>
		/// <returns>アルバムアート：Stream</returns>
		public Stream GetAlbumArtStream()
		{
			try
			{
				var trackFilePath = this.Properties.CurrentTrack.FilePath;
				var selector = new Selector();
				var extractor = selector.SelectAlbumArtExtractor(trackFilePath);
				return extractor.Extract(trackFilePath);
			}
			catch (Exception e)
			{
				new IcePlayerException("Get album art stream error:", e);
				return null;
			}
		}

		/// <summary>
		/// Bitmap を取得します
		/// </summary>
		/// <returns>アルバムアート：Bitmap形式</returns>
		public Bitmap GetBitmap()
		{
			try
			{
				return new Bitmap(this.GetAlbumArtStream());
			}
			catch (Exception e)
			{
				new IcePlayerException("get bitmap error:", e);
				return null;
			}
		}

		/// <summary>
		/// 非同期にて曲の再生時間を取得します
		/// </summary>
		/// <param name="position">再生位置</param>
		/// <returns>現在の再生時間</returns>
		public async Task<string> GetCurrentTrackPositionAsync(int position)
		{
			var totalSec = position / 1000;
			var min = totalSec / 60;
			var sec = totalSec % 60;
			return $"{min:D2}:{sec:D2}";
		}

		/// <summary>
		/// 非同期にて音量を取得します
		/// </summary>
		/// <param name="volume">音量：変更値</param>
		/// <returns>現在の音量</returns>
		public async Task<int> GetCurrentVolumeAsync(int volume)
		{
			this.Properties.Volume = volume;
			return this.Properties.Volume;
		}

		/// <summary>
		/// アルバムアートをビューワにて表示します
		/// </summary>
		/// <param name="e">マウスボタンのイベント値</param>
		public void ShowAlbumArtWithViewer(MouseButtonEventArgs e)
		{
			try
			{
				if (e.ClickCount != 2)
				{
					return;
				}

				using (var source = (MemoryStream)this.GetAlbumArtStream())
				using (var stream = new FileStream("temp.jpg", FileMode.OpenOrCreate, FileAccess.Write))
				{
					stream.Write(source.ToArray(), 0, (int)source.Length);
				}

				Process.Start("temp.jpg");
			}
			catch (Exception ex)
			{
				new IcePlayerException("show albumart viewer error:", ex);
			}
		}

		/// <summary>
		/// バルーン通知 / トースト通知を行います
		/// </summary>
		public void CallNotification()
		{
			try
			{
				using (var notifyIcon = new NotifyIcon())
				{
					var os = Environment.OSVersion;
					var track = this.Properties.CurrentTrack;

					notifyIcon.Icon = Resources.Ice_Middle;
					notifyIcon.Visible = true;

					// トースト通知
					if (os.Version.Major >= 6 && os.Version.Minor >= 2)
					{
						notifyIcon.BalloonTipTitle = $"IcePlayer NowPlaying\r\n{track.Title} - {track.Artist}";
						notifyIcon.BalloonTipText = $"Album : {track.Album}";
						Debug.WriteLine("トースト通知が表示されました。");
					}
					// バルーン通知
					else
					{
						notifyIcon.BalloonTipTitle = $"IcePlayer NowPlaying";
						notifyIcon.BalloonTipText = $"{track.Title} - {track.Artist}\r\nAlbum : {track.Album}";
						Debug.WriteLine("バルーン通知が表示されました。");
					}
					notifyIcon.ShowBalloonTip(3000);
				}
			}
			catch (Exception e)
			{
				new IcePlayerException("notify error:", e);
			}
		}

		/// <summary>
		/// NowPlaying 投稿を行います
		/// </summary>
		public async Task PostingNowPlaying()
		{
			try
			{
				// OOB ベースでの認証を行う場合は、以下3行を有効化する
				//Process.Start(await this._Twitter.GenerateAuthorizeAsync());
				//var pin = string.Empty;
				//await _Twitter.GetAccessTokenAsync(pin);

				var track = this.Properties.CurrentTrack;
				var tweet = $"🎵 {track.Title}\r\n🎙{track.Artist}\r\n💿 {track.Album}\r\n#nowplaying #LegatoWPF #AIMP4";
				await _Twitter.UpdateWithMediaAsync(tweet, this.GetAlbumArtStream());
			}
			catch (Exception e)
			{
				new IcePlayerException("posting nowplaying error:", e);
			}
		}

		#endregion Methods
	}
}
