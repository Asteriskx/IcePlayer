using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace IcePlayer.Splash
{
	/// <summary>
	/// IcePlayer スプラッシュウィンドウクラス
	/// </summary>
	public partial class IceSplashControl : Window, INotifyPropertyChanged
	{
		/// <summary>
		/// プロパティ変更通知
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private IceSplashControl() { }

		/// <summary>
		/// プロパティ変更
		/// </summary>
		/// <param name="name"></param>
		public void OnPropertyChanged(string name) => 
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		/// <summary>
		/// 表示メッセージ変更プロパティ
		/// </summary>
		private string _message;
		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				OnPropertyChanged("Message");
				DoEvents();
			}
		}

		/// <summary>
		/// 実行イベント
		/// </summary>
		public void DoEvents()
		{
			DispatcherFrame frame = new DispatcherFrame();

			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
				new DispatcherOperationCallback(ExitFrames), frame);

			Dispatcher.PushFrame(frame);
		}

		/// <summary>
		/// スプラッシュウィンドウ 抜け出し
		/// </summary>
		/// <param name="frame"></param>
		/// <returns></returns>
		public object ExitFrames(object frame)
		{
			(frame as DispatcherFrame).Continue = false;
			return null;
		}
	}
}



