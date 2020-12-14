using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SwapSim {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		/// <summary>
		/// Creates the Main Window
		/// </summary>
		public MainWindow() => this.InitializeComponent();
		/// <summary>
		/// Resets the computer
		/// </summary>
		/// <param name="sender">Object that sends the event</param>
		/// <param name="e">Parameters of the event</param>
		private void ResetButton_Click(object sender, RoutedEventArgs e) {
			this.Computer.Reset();
			this.Update();
		}
		/// <summary>
		/// Adds a new process to the computer
		/// </summary>
		/// <param name="sender">Object that sends the event</param>
		/// <param name="e">Parameters of the event</param>
		private void AddNewProcessButton_Click(object sender, RoutedEventArgs e) {
			try {
				this.Computer.AddProcess(this.PriorityInput.IsChecked.Value);
				this.Update();
			} catch (Exception) {
			} finally {
				this.PriorityInput.IsChecked = false;
			}
		}
		/// <summary>
		/// Forces UI Update
		/// </summary>
		private void Update() {
			this.DataContext = null;
			this.DataContext = this.Computer;
		}
		/// <summary>
		/// Runs the current iteration
		/// </summary>
		/// <param name="sender">Object that sends the event</param>
		/// <param name="e">Parameters of the event</param>
		private void RunButton_Click(object sender, RoutedEventArgs e) {
			this.Computer.Run();
			this.Update();
		}
	}
}
