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
        public int totalQuant;
        public decimal individualCost;
        public int selectedQuant;

        private PaymentAction paymentAction;

        public paymentItem(OrderItem orderItem, PaymentAction paymentAction)
        {

            InitializeComponent();

            this.paymentAction = paymentAction;
            this.orderItem = orderItem;
            this.totalCost = (orderItem.optionsCost + orderItem.menuItem.price) * orderItem.quantity;
            this.individualCost = (orderItem.optionsCost + orderItem.menuItem.price);
            this.selectedQuant = 0;
            this.totalQuant = orderItem.quantity;


            quantitySub.Visibility = Visibility.Hidden;


            image.Source = new BitmapImage(
                                    new Uri(this.orderItem.menuItem.imageName, UriKind.RelativeOrAbsolute));

            titleLabel.Content = orderItem.menuItem.name;
            priceLabel.Content = selectedQuant.ToString() + "x $" + individualCost;
            totalLeft.Content = totalQuant - selectedQuant;

        }

        private void toggleStatus(Boolean selected)
        {

            if (selected)
            {
                this.isSelected = true;
                this.Background = new SolidColorBrush(Color.FromArgb(0xFF, 216, 216, 216));
               // checkbox.IsChecked = true;

                this.paymentAction.addItem(totalCost);
            } else
            {
                this.isSelected = false;
                this.Background = new SolidColorBrush(Color.FromArgb(0xFF, 255, 255, 255));
               // checkbox.IsChecked = false;

                this.paymentAction.removeItem(totalCost);
            }
        }

        private void updateTotals()
        {
            priceLabel.Content = selectedQuant.ToString() + "x $" + individualCost;
            totalLeft.Content = totalQuant - selectedQuant;
        }

        public void updateAfterPayment()
        {

            totalQuant -= selectedQuant;
            selectedQuant = 0;

            quantityAdd.Visibility = Visibility.Visible;
            quantitySub.Visibility = Visibility.Hidden;

            this.Background = new SolidColorBrush(Color.FromArgb(0xFF, 255, 255, 255));

            updateTotals();
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




        private void quantityAdd_Click(object sender, RoutedEventArgs e)
        {
            this.selectedQuant++;
            this.paymentAction.addItem(individualCost);

            this.isSelected = true;
            this.Background = new SolidColorBrush(Color.FromArgb(0xFF, 216, 216, 216));


            quantitySub.Visibility = Visibility.Visible;

            updateTotals();

            if (selectedQuant == totalQuant)
            {
                quantityAdd.Visibility = Visibility.Hidden;
            }
        }

        private void quantitySub_Click(object sender, RoutedEventArgs e)
        {
            this.selectedQuant--;
            this.paymentAction.removeItem(individualCost);

            quantityAdd.Visibility = Visibility.Visible;

            updateTotals();

            if (this.selectedQuant == 0)
            {
                this.isSelected = false;
                this.Background = new SolidColorBrush(Color.FromArgb(0xFF, 255, 255, 255));

                quantitySub.Visibility = Visibility.Hidden;
            }
        }
    }
}
