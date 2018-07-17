using Legato.Interop.AimpRemote.Entities;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace IcePlayer.ViewModels
{
	/// <summary>
	/// IcePlayer ViewModel 層 管理クラス
	/// </summary>
	public class ViewModels : BindableBase
	{
		/// <summary>
		/// 
		/// </summary>
		public Models.Models Model { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ViewModels() => this.Model = new Models.Models();

		/// <summary>
		/// 
		/// </summary>
		private TrackInfo _CurrentTrackInfo;

		/// <summary>
		/// 
		/// </summary>
		public TrackInfo CurrentTrackInfo
		{
			get { return this._CurrentTrackInfo; }
			set { this.SetProperty(ref this._CurrentTrackInfo, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		private BitmapImage _CurrentArtwork;

		/// <summary>
		/// 
		/// </summary>
		public BitmapImage CurrentArtwork
		{
			get { return this._CurrentArtwork; }
			set { this.SetProperty(ref this._CurrentArtwork, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		private DelegateCommand _CurrentTrackInfoComamnd;

		/// <summary>
		/// 
		/// </summary>
		public DelegateCommand CurrentTrackInfoComamnd
		{
			get
			{
				return this._CurrentTrackInfoComamnd = this._CurrentTrackInfoComamnd ?? new DelegateCommand(GetCurrentTrackInfo);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void GetCurrentTrackInfo()
		{
			(this.CurrentTrackInfo, this.CurrentArtwork) = (this.Model.Properties.CurrentTrack, this.Model.GetArtwork());
		}

	}
}