using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using IcePlayer.Views;
using Legato;

namespace IcePlayer.Behavior
{
	/// <summary>
	/// 音量調節用スライダーの値が変更された時
	/// </summary>
	class VolumeSliderBehavior : Behavior<Slider>
	{
		private AimpCommands _Commands { get; set; } = new AimpCommands();

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.ValueChanged += VolumeSlider_ValueChanged;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.ValueChanged -= VolumeSlider_ValueChanged;
		}

		/// <summary>
		/// 音量調節用スライダーの値が変更された時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			// TODO : 現在の音量を取得
			var window = (MainWindow)App.Current.MainWindow;
			window.ShowVolume.Content = (int)window.VolumeBar.Value;
		}
	}
}