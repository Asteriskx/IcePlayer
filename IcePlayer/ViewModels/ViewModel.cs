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
		private TrackInfo _currentTrackInfo;

		/// <summary>
		/// 
		/// </summary>
		private DelegateCommand _currentTrackInfoComamnd;

		/// <summary>
		/// 
		/// </summary>
		private BitmapImage _currentAlbumArt;

		/// <summary>
		/// 
		/// </summary>
		private string _currentTrackPosition;

		/// <summary>
		/// 
		/// </summary>
		private int _currentVolume;

		#endregion Fields

		#region Properties

		/// <summary>
		/// Model 層 プロパティ
		/// </summary>
		public Models.Model Model { get; set; } = new Models.Model();

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
		public int CurrentVolume
		{
			get { return this._currentVolume; }
			set { this.SetProperty(ref this._currentVolume, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public DelegateCommand CurrentTrackInfoComamnd
		{
			get { return this._currentTrackInfoComamnd = this._currentTrackInfoComamnd ?? new DelegateCommand(GetCurrentTrackInfo); }
		}

		#endregion Properties

		#region Constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ViewModels()
		{
			this.Model.Observer.CurrentTrackChanged += async (track) => await this._UpdateAsync(track);
			this.Model.Observer.PositionPropertyChanged += async (position) => await this._PositionPropertyChangedAsync(position);
			this.Model.Observer.VolumePropertyChanged += async (volume) => await this._VolumePropertyChangedAsync(volume);
		}

		#endregion Constructor

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
		private async Task _UpdateAsync(TrackInfo track)
		{
			(this.CurrentTrackInfo, this.CurrentAlbumArt) = (track, this.Model.GetAlbumArt());
			this.Model.CallNotification();
		}

		/// <summary>
		/// 非同期にて曲の再生時間を更新します
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private async Task _PositionPropertyChangedAsync(int position) =>
			this.CurrentTrackPosition = await this.Model.GetCurrentTrackPositionAsync(position);

		/// <summary>
		/// 非同期にて音量を更新します
		/// </summary>
		/// <param name="volume"></param>
		/// <returns></returns>
		private async Task _VolumePropertyChangedAsync(int volume) =>
			this.CurrentVolume = await this.Model.GetCurrentVolumeAsync(volume);

		#endregion Methods
	}
}