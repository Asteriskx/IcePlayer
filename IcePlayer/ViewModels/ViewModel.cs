using Legato.Interop.AimpRemote.Entities;
using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace IcePlayer.ViewModels
{
	/// <summary>
	/// IcePlayer ViewModel 層 管理クラス
	/// </summary>
	public class ViewModels : BindableBase
	{

		#region Fields

		/// <summary>
		/// 
		/// </summary>
		private DelegateCommand _currentTrackInfoComamnd;

		/// <summary>
		/// 
		/// </summary>
		private TrackInfo _currentTrackInfo;

		/// <summary>
		/// 
		/// </summary>
		private BitmapImage _currentArtwork;

		#endregion Fields

		#region Properties

		/// <summary>
		/// Model 層 プロパティ
		/// </summary>
		public Models.Models Model { get; set; } = new Models.Models();

		/// <summary>
		/// 
		/// </summary>
		public TrackInfo CurrentTrackInfo
		{
			get { return this._currentTrackInfo; }
			set { this.SetProperty(ref this._currentTrackInfo, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public BitmapImage CurrentArtwork
		{
			get { return this._currentArtwork; }
			set { this.SetProperty(ref this._currentArtwork, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public DelegateCommand CurrentTrackInfoComamnd
		{
			get { return this._currentTrackInfoComamnd = this._currentTrackInfoComamnd ?? new DelegateCommand(GetCurrentTrackInfo); }
		}

		#endregion Properties

		#region Constractor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ViewModels() =>
			this.Model.Observer.CurrentTrackChanged += async (track) => await this.UpdateAsync(track);

		#endregion Constractor

		#region Methods

		/// <summary>
		/// 現在のトラック情報（曲情報、アートワーク）を取得します
		/// </summary>
		private void GetCurrentTrackInfo() =>
			(this.CurrentTrackInfo, this.CurrentArtwork) = (this.Model.Properties.CurrentTrack, this.Model.GetArtwork());
		
		/// <summary>
		/// イベント発火時、非同期にてトラック情報を更新します
		/// </summary>
		/// <param name="track"></param>
		/// <returns></returns>
		private async Task UpdateAsync(TrackInfo track) =>
			(this.CurrentTrackInfo, this.CurrentArtwork) = (track, this.Model.GetArtwork());

		#endregion Methods

	}
}