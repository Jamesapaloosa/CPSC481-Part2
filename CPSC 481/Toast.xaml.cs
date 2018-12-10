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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CPSC_481
{
    /// <summary>
    /// Interaction logic for Toast.xaml
    /// </summary>
    public partial class Toast : UserControl
    {

        public Boolean animating = false;

        public Toast()
        {
            InitializeComponent();
        }



        public void displayMessage(String message)
        {

            if (!animating)
            {

                toastPanel.Opacity = 1;
                animating = true;
                text.Content = message;
                Visibility = Visibility.Visible;


                Task.Delay(3000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {

                        DoubleAnimation da = new DoubleAnimation();
                        da.From = 1;
                        da.To = 0;
                        da.Duration = new Duration(TimeSpan.FromSeconds(1));
                        da.AutoReverse = true;
                        toastPanel.BeginAnimation(OpacityProperty, da);

                        Task.Delay(1000).ContinueWith(__ =>
                        {
                            Dispatcher.Invoke(() =>
                            {

                                Visibility = Visibility.Hidden;
                                animating = false;
                            });
                        });
                    });
                });
            }


        }
    }
}


   
