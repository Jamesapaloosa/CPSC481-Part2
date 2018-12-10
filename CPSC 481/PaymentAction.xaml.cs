using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for PaymentAction.xaml
    /// </summary>
    public partial class PaymentAction : UserControl
    {

        private decimal totalForPayment = 0;
        private Boolean callingServer = false;


        public PaymentAction()
        {
            InitializeComponent();
        }

        private void Pay_All_Button_Click(object sender, RoutedEventArgs e)
        {

            paymentTypeSelector.Visibility = Visibility.Hidden;
            payPrompt.Visibility = Visibility.Visible;


            Task.Delay(4000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    payThankYou.Visibility = Visibility.Visible;



                    Task.Delay(4000).ContinueWith(a =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            reset();
                            var parent = ((Menu)((Grid)Parent).Parent).orderTableView;
                            parent.Reset();
                        });
                    });
                });
            });

           
        }

        public void startPayment()
        {



            decimal total = 0;
            var parent = ((Menu)((Grid)Parent).Parent).orderTableView;
            foreach (OrderItemCell orderItemCell in parent.finalizedItemStackPanel.Children)
            {
                // Didnt use capital letter for it oops.
                paymentItem item = new paymentItem(orderItemCell.orderItem, this);
                total += item.totalCost;
            }

            totalCostMain.Content = total.ToString();

            Visibility = Visibility.Visible;
        }

        public void addItem(decimal price)
        {
            this.totalForPayment += price;
            this.paymentTotal.Content = totalForPayment.ToString();

            makeIndividualPaymentButton.Visibility = Visibility.Visible;
        }

        public void removeItem(decimal price)
        {
            this.totalForPayment -= price;
            this.paymentTotal.Content = totalForPayment.ToString();

            if (totalForPayment <= 0)
            {
                makeIndividualPaymentButton.Visibility = Visibility.Hidden;
            }
        }

        private void Split_Button_Click(object sender, RoutedEventArgs e)
        {

            makeIndividualPaymentButton.Visibility = Visibility.Hidden;
            paymentTypeSelector.Visibility = Visibility.Hidden;
            splitItemSelector.Visibility = Visibility.Visible;

            var parent = ((Menu)((Grid)Parent).Parent).orderTableView;

            foreach (OrderItemCell orderItemCell in parent.finalizedItemStackPanel.Children)
            {
                // Didnt use capital letter for it oops.
                paymentItem item = new paymentItem(orderItemCell.orderItem, this);
                paymentItems.Children.Add(item);
            }
        }

        private void reset()
        {

            splitItemSelector.Visibility = Visibility.Hidden;
            payThankYou.Visibility = Visibility.Hidden;
            paymentTypeSelector.Visibility = Visibility.Visible;
            payPrompt.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Hidden;
            this.totalForPayment = 0;
            this.paymentTotal.Content = "0";
            paymentItems.Children.Clear();
        }

        private void removeSelectedItems()
        {

            List<paymentItem> toRemove = new List<paymentItem>();

            foreach (paymentItem item in paymentItems.Children)
            {

                item.updateAfterPayment();

                if (item.totalQuant == 0)
                {
                    toRemove.Add(item);
                }
            }

            totalForPayment = 0;
            paymentTotal.Content = 0;

            foreach (paymentItem item in toRemove)
            {
                paymentItems.Children.Remove(item);
            }
        }

        private void makeIndividualPaymentButton_Click(object sender, RoutedEventArgs e)
        {

            splitItemSelector.Visibility = Visibility.Hidden;
            payPrompt.Visibility = Visibility.Visible;
            var parent = ((Menu)((Grid)Parent).Parent).orderTableView;

            Task.Delay(4000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    payThankYou.Visibility = Visibility.Visible;

                    Task.Delay(2000).ContinueWith(a =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            splitItemSelector.Visibility = Visibility.Visible;
                            payPrompt.Visibility = Visibility.Hidden;
                            payThankYou.Visibility = Visibility.Hidden;

                            removeSelectedItems();

                            // If there are no more itemsw, quit and reset and stuff.
                            if (paymentItems.Children.Count == 0)
                            {
                                reset();
                                parent.Reset();
                            } 
                        });
                    });
                });
            });

        }



        private void callServerButton_Click(object sender, RoutedEventArgs e)
        {

            if (!callingServer)
            {

                Menu.toastMessager.displayMessage("Your server is on their way!");

                serverButtonImage.Source = new BitmapImage(
                                    new Uri("pack://application:,,,/CPSC 481;component/Images/menu/server_o.png", UriKind.RelativeOrAbsolute));

                Task.Delay(5000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (callingServer)
                        {
                            callServerButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        }
                    });
                });

                callingServer = true;
            }
            else
            {
                serverButtonImage.Source = new BitmapImage(
                                    new Uri("pack://application:,,,/CPSC 481;component/Images/menu/server.png", UriKind.RelativeOrAbsolute));

                callingServer = false;
            }



        }

    }
}
