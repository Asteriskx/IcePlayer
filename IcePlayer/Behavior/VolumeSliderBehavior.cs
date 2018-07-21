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
		#region Property

		/// <summary>
		/// 
		/// </summary>
		private AimpCommands _Commands { get; set; } = new AimpCommands();

		#endregion Peoperty

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public VolumeSliderBehavior() { }

		#endregion Constructor

		#region Attach / Detaching

		/// <summary>
		/// 
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.ValueChanged += this._VolumeSlider_ValueChanged;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.ValueChanged -= this._VolumeSlider_ValueChanged;
		}

		#endregion Attached / Detaching

		#region Method

		/// <summary>
		/// 音量調節用スライダーの値が変更された時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void _VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			// TODO : 現在の音量を取得
			var window = (MainWindow)App.Current.MainWindow;

			await new Models.Model().GetCurrentVolumeAsync((int)window.VolumeBar.Value);

			window.ShowVolume.Content = (int)window.VolumeBar.Value;
		}

		#endregion Method
	}
}