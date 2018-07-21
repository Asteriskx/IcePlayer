using System.Threading;
using System.Windows;

namespace IcePlayer
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		#region Constructor 

		/// <summary>
		/// 
		/// </summary>
		public App() : base()
		{
			Startup += Application_Startup;
			Thread.Sleep(3000);
		}

		#endregion Constructor

		#region Method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Application_Startup(object sender, StartupEventArgs e) { }

		#endregion Method
	}
}
