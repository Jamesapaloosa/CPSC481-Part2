using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace CPSC_481
{
    public partial class OrderItemCell : UserControl
    {
        public enum ActionType { Edit, Delete, RequestServer }
        public delegate void OrderItemCellActionTypeHandler(OrderItemCell sender, OrderItemCell.ActionType action);
        public event OrderItemCellActionTypeHandler OnAction;



        public OrderItem orderItem { get; private set; }
        public OrderTableView otable { get; private set; }


        private Boolean actionIsRefil = false;
        private Boolean callingServer = false;

        private Menu menu;


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
                    case MenuObject.Type.Drink:
                        actionIsRefil = true;
                        actionButtonImage.Source = new BitmapImage(
                                new Uri("pack://application:,,,/CPSC 481;component/Images/menu/refil_button.png", UriKind.RelativeOrAbsolute));
                        break;
                    default:
                        actionIsRefil = false;
                        actionButtonImage.Source = new BitmapImage(
                              new Uri("pack://application:,,,/CPSC 481;component/Images/menu/server.png", UriKind.RelativeOrAbsolute));
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
                if (actionIsRefil)
                {
                    Menu.toastMessager.displayMessage("Your server is on the way \nwith your drink!");

                } else
                {

                    if (!callingServer)
                    {

                        Menu.toastMessager.displayMessage("Your server is on their way!");

                        callingServer = true;
                        actionButtonImage.Source = new BitmapImage(
                                       new Uri("pack://application:,,,/CPSC 481;component/Images/menu/server_o.png", UriKind.RelativeOrAbsolute));

                        Task.Delay(5000).ContinueWith(_ =>
                        {
                            Dispatcher.Invoke(() =>
                            {
                                if (callingServer)
                                {
                                    actionButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                                    callingServer = false;
                                }
                            });
                        });
                    } else
                    {
                        actionButtonImage.Source = new BitmapImage(
                                      new Uri("pack://application:,,,/CPSC 481;component/Images/menu/server.png", UriKind.RelativeOrAbsolute));
                    }

                }

               // this.OnAction(this, ActionType.RequestServer);
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

            if (OptionsMenu.isOrdering != null)
            {
                OptionsMenu.isOrdering.CancelButtonClickedWorker();

            }

            if (this.orderItem.isFinalized)
                return;
            otable.Remove(this);
            OptionsMenu.isOrdering = this.orderItem.menuItem.optionsMenu;
            this.orderItem.sourceMenu.EditOptionsMenu(this.orderItem.menuItem, this.orderItem.options, this.orderItem);
        }
    }
}
