using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace IcePlayer.Models
{
	/// <summary>
	/// IcePlayer スプラッシュウィンドウクラス
	/// </summary>
	public partial class IceSplashControl : Window, INotifyPropertyChanged
	{
		#region Field

		/// <summary>
		/// 表示メッセージ変更プロパティ
		/// </summary>
		private string _message;

		#endregion Field

		#region Property

		/// <summary>
		/// 表示メッセージ変更プロパティ
		/// </summary>
		public string Message
		{
			get { return _message; }
			set { this.OnPropertyChanged("Message", value); }
		}

		#endregion Property

		#region Event

		/// <summary>
		/// プロパティ変更通知
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Event

		#region Constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private IceSplashControl() { }

		#endregion Constructor

		#region Methods

		/// <summary>
		/// プロパティ変更
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void OnPropertyChanged(string name, string value)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
			this._message = value;
			this._DoEvents();
		}

		/// <summary>
		/// 実行イベント
		/// </summary>
		private void _DoEvents()
		{
			var frame = new DispatcherFrame();
			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
				new DispatcherOperationCallback(this._ExitFrames), frame);

			Dispatcher.PushFrame(frame);
		}

		/// <summary>
		/// スプラッシュウィンドウ 抜け出し
		/// </summary>
		/// <param name="frame"></param>
		/// <returns></returns>
		private object _ExitFrames(object frame)
		{
			(frame as DispatcherFrame).Continue = false;
			return null;
		}

		#endregion Methods
	}
}
