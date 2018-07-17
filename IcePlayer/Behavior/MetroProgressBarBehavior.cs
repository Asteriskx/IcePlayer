using System.Windows;
using System.Windows.Interactivity;
using Legato;
using MahApps.Metro.Controls;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 再生中の曲の経過時間を表すプログレスバー の処理クラス
	/// </summary>
	class MetroProgressBarBehavior : Behavior<MetroProgressBar>
	{
		/// <summary>
		/// 
		/// </summary>
		private AimpCommands _Commands { get; set; } = new AimpCommands();

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.ValueChanged += MetroProgressBar_ValueChanged;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.ValueChanged -= MetroProgressBar_ValueChanged;
		}

		/// <summary>
		/// 再生中の曲の経過時間を表すプログレスバー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MetroProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
		}
	}
}