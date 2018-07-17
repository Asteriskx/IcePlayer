using System;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Net.Http;
using AlbumArtExtraction;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 
	/// </summary>
	class NowPlayingBehavior : Behavior<Button>
	{

		#region API Keys
		
		/// <summary>
		/// 
		/// </summary>
		private static readonly string _ck = "your consumer key.";

		/// <summary>
		/// 
		/// </summary>
		private static readonly string _cs = "your consumer key secret.";

		/// <summary>
		/// 
		/// </summary>
		private static readonly string _at = "your access token.";

		/// <summary>
		/// 
		/// </summary>
		private static readonly string _ats = "your access token secret.";

		#endregion API Keys

		/// <summary>
		/// 
		/// </summary>
		private Twist.Twitter _Twitter { get; set; } = new Twist.Twitter(_ck, _cs, _at, _ats, new HttpClient(new HttpClientHandler()));

		/// <summary>
		/// Model 層 プロパティ
		/// </summary>
		public Models.Models Model { get; set; } = new Models.Models();

		/// <summary>
		/// 
		/// </summary>
		public NowPlayingBehavior() { }

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Click += NowPlaying;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Click -= NowPlaying;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void NowPlaying(object sender, EventArgs e)
		{
			//Process.Start(await this._Twitter.GenerateAuthorizeAsync());
			//var pin = string.Empty;
			//await _Twitter.GetAccessTokenAsync(pin);

			var track = this.Model.Properties.CurrentTrack;
			var tweet = $"🎵 {track.Title}\r\n🎙{track.Artist}\r\n💿 {track.Album}\r\n#nowplaying #LegatoWPF #AIMP4";
			var trackFilePath = this.Model.Properties.CurrentTrack.FilePath;
			var selector = new Selector();
			var extractor = selector.SelectAlbumArtExtractor(trackFilePath);
			var source = extractor.Extract(trackFilePath);

			await _Twitter.UpdateWithMediaAsync(tweet, source);
		}
	}
}
