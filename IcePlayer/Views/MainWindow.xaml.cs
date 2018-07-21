using MahApps.Metro.Controls;

namespace IcePlayer.Views
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public MainWindow()
		{
			this.InitializeComponent();
			this.DataContext = new ViewModels.ViewModels();
		}

		#endregion Constructor
	}
}
