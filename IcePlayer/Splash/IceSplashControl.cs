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
        // プロパティ変更通知
        public event PropertyChangedEventHandler PropertyChanged;
        
        // コンストラクタ
        private IceSplashControl(){ }

        // プロパティ変更
        public void OnPropertyChanged( string name )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( name ));
        }

        // 表示メッセージ変更プロパティ
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

        // 実行イベント
        public void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();

            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback( ExitFrames ), frame);

            Dispatcher.PushFrame( frame );
        }

        // スプラッシュウィンドウ 抜け出し
        public object ExitFrames( object frame )
        {
            ( frame as DispatcherFrame ).Continue = false;
            return null;
        }
    }
}



