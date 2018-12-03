using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CPSC_481
{
    public partial class OrderItemCell : UserControl
    {
        public enum ActionType { Edit, Delete, RequestServer }
        public delegate void OrderItemCellActionTypeHandler(OrderItemCell sender, OrderItemCell.ActionType action);
        public event OrderItemCellActionTypeHandler OnAction;



        public OrderItem orderItem { get; private set; }
        public OrderTableView otable { get; private set; }





        public OrderItemCell(OrderItem orderItem, OrderTableView otable)
        {
            InitializeComponent();

            this.otable = otable;
            this.orderItem = orderItem;
            this.imageView.Source = new BitmapImage(new Uri(this.orderItem.menuItem.imageName, UriKind.Relative));
            this.titleLabel.Content = this.orderItem.menuItem.name;

            this.update();
        }

        public void update()
        {
            this.priceLabel.Content = String.Format("{0:C}", orderItem.totalPrice);

           // this.actionButton.Visibility = (this.orderItem.isFinalized) ? Visibility.Hidden : Visibility.Visible;
            if (this.orderItem.isFinalized)
            {
                switch (this.orderItem.menuItem.type)
                {
                    case MenuItem.Type.Drink:
                        actionButtonImage.Source = new BitmapImage(
                                new Uri("pack://application:,,,/CPSC 481;component/Images/menu/refil_button.png", UriKind.RelativeOrAbsolute));
                        break;
                    default:
                        actionButtonImage.Source = new BitmapImage(
                              new Uri("pack://application:,,,/CPSC 481;component/Images/menu/problem_button.png", UriKind.RelativeOrAbsolute));
                        break;
                }

                editButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                actionButtonImage.Source = new BitmapImage(
                                new Uri("pack://application:,,,/CPSC 481;component/Images/menu/delete_button.png", UriKind.RelativeOrAbsolute));
            }

        }

        private void actionButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.orderItem.isFinalized)
            {
                this.OnAction(this, ActionType.RequestServer);
            }
            else
            {
                this.OnAction(this, ActionType.Delete);
            }
        }

        private void selectedButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.orderItem.isFinalized)
                return;

            this.OnAction(this, ActionType.Edit);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.orderItem.isFinalized)
                return;
            otable.Remove(this);
            this.orderItem.sourceMenu.EditOptionsMenu(this.orderItem.menuItem, this.orderItem.options);
        }
    }
}
