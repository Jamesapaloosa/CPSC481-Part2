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

namespace CPSC_481
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartButton_MouseEnter(object sender, RoutedEventArgs e)
        {
            WelcomeLabel.Content = "Click to enter";
        }

        private void StartButton_MouseLeave(object sender, RoutedEventArgs e)
        {
            WelcomeLabel.Content = "Welcome!";
        }


    }
}
