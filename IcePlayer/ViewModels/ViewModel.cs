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
		private string _currentTrackPosition;

		/// <summary>
		/// 
		/// </summary>
		private BitmapImage _currentAlbumArt;

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
		public BitmapImage CurrentAlbumArt
		{
			get { return this._currentAlbumArt; }
			set { this.SetProperty(ref this._currentAlbumArt, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public string CurrentTrackPosition
		{
			get { return this._currentTrackPosition; }
			set { this.SetProperty(ref this._currentTrackPosition, value); }
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
		public ViewModels()
		{
			this.Model.Observer.CurrentTrackChanged += async (track) => await this._UpdateAsync(track);
			this.Model.Observer.PositionPropertyChanged += async (position) => await this._PositionPropertyChangedAsync(position);
		}

		#endregion Constractor

		#region Methods

		/// <summary>
		/// 現在のトラック情報（曲情報、アルバムアート）を取得します
		/// </summary>
		private void GetCurrentTrackInfo() =>
			(this.CurrentTrackInfo, this.CurrentAlbumArt) = (this.Model.Properties.CurrentTrack, this.Model.GetAlbumArt());
		
		/// <summary>
		/// イベント発火時、非同期にてトラック情報を更新します
		/// </summary>
		/// <param name="track"></param>
		/// <returns></returns>
		private async Task _UpdateAsync(TrackInfo track) =>
			(this.CurrentTrackInfo, this.CurrentAlbumArt) = (track, this.Model.GetAlbumArt());

		/// <summary>
		/// 非同期にて曲の再生時間を更新します
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private async Task _PositionPropertyChangedAsync(int position) =>
			this.CurrentTrackPosition = await this.Model.GetCurrentTrackPositionAsync(position);
		
		#endregion Methods

	}
}