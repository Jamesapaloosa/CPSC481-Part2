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
    /// Interaction logic for paymentItem.xaml
    /// </summary>
    public partial class paymentItem : UserControl
    {


        public OrderItem orderItem { get; private set; }
        public Boolean isSelected = false;

        public decimal totalCost;

        private PaymentAction paymentAction;

        public paymentItem(OrderItem orderItem, PaymentAction paymentAction)
        {

            InitializeComponent();

            this.paymentAction = paymentAction;
            this.orderItem = orderItem;
            this.totalCost = (orderItem.optionsCost + orderItem.menuItem.price) * orderItem.quantity;



            image.Source = new BitmapImage(
                                    new Uri(this.orderItem.menuItem.imageName, UriKind.RelativeOrAbsolute));

            titleLabel.Content = orderItem.menuItem.name;
            priceLabel.Content = "$" + totalCost;
         
        }

        private void toggleStatus(Boolean selected)
        {

            if (selected)
            {
                this.isSelected = true;
                this.Background = new SolidColorBrush(Color.FromArgb(0xFF, 216, 216, 216));
                checkbox.IsChecked = true;

                this.paymentAction.addItem(totalCost);
            } else
            {
                this.isSelected = false;
                this.Background = new SolidColorBrush(Color.FromArgb(0xFF, 255, 255, 255));
                checkbox.IsChecked = false;

                this.paymentAction.removeItem(totalCost);
            }
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            toggleStatus(true);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            toggleStatus(false);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            toggleStatus(!isSelected);
        }

        private void Grid_TouchDown(object sender, TouchEventArgs e)
        {
            toggleStatus(!isSelected);
        }
    }
}
