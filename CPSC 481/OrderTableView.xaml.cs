using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPSC_481
{
    public partial class OrderTableView : UserControl
    {
        public OrderTableView()
        {
            InitializeComponent();

            this.Reset();
        }

        public void Reset()
        {
            this.finalizedItemStackPanel.Children.Clear();
            this.pendingItemStackPanel.Children.Clear();

            this.Update();
        }

        private void Update()
        {
            decimal totalPrice = 0.0m;
            foreach (OrderItemCell cell in this.finalizedItemStackPanel.Children)
            {
                totalPrice += cell.orderItem.totalPrice;
            }
            foreach (OrderItemCell cell in this.pendingItemStackPanel.Children)
            {
                totalPrice += cell.orderItem.totalPrice;
            }
            this.totalPriceLabel.Content = String.Format("{0:C}", totalPrice);



            //this.finalizedItemSectionHeader.Height = (this.finalizedItemStackPanel.Children.Count == 0) ? 0.0 : Double.NaN;
            //this.pendingItemSectionHeader.Height = (this.pendingItemStackPanel.Children.Count == 0) ? 0.0 : Double.NaN;

            this.finalizedItemSectionHeader.Height = 0.0;
            this.pendingItemSectionHeader.Height = 0.0;
            this.actionGrid.Height = 0.0;

            if (this.finalizedItemStackPanel.Children.Count > 0)
            {
                this.finalizedItemSectionHeader.Height = Double.NaN;
                this.actionGrid.Height = Double.NaN;
                this.actionButton.Content = "Pay";
            }
            if (this.pendingItemStackPanel.Children.Count > 0)
            {
                this.pendingItemSectionHeader.Height = Double.NaN;
                this.actionGrid.Height = Double.NaN;
                this.actionButton.Content = "Order";
            }



            this.actionButton.Height = Double.NaN;
            this.actionConfirmationView.Height = 0.0;
        }

        public void Add(OrderItem orderItem)
		{
            OrderItemCell orderItemCell = new OrderItemCell(orderItem, this);

            orderItemCell.OnAction += this.OrderItemCell_OnAction;
            this.pendingItemStackPanel.Children.Add(orderItemCell);

			this.Update();

            this.scrollViewer.ScrollToEnd();
		}

        public void Remove(OrderItemCell orderItemCell)
        {
            this.pendingItemStackPanel.Children.Remove(orderItemCell);

            this.Update();
        }

        private void FinalizeOrder()
        {
            foreach (OrderItemCell orderItemCell in this.pendingItemStackPanel.Children)
            {
                OrderItemCell orderItemCellCopy = new OrderItemCell(orderItemCell.orderItem, this);
                orderItemCellCopy.OnAction += this.OrderItemCell_OnAction;

                this.finalizedItemStackPanel.Children.Add(orderItemCellCopy);
                orderItemCellCopy.orderItem.isFinalized = true;
                orderItemCellCopy.update();
                //orderItemCell.IsEnabled = !orderItemCell.orderItem.isFinalized;
            }
            this.pendingItemStackPanel.Children.Clear();
            

            this.Update();
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            //this.totalPriceLabel.Content = "Confirm";
            this.actionButton.Height = 0.0;
            this.actionConfirmationView.Height = Double.NaN;
            this.confirmationNoButton.Content = "Cancel";
            this.confirmationYesButton.Content = this.actionButton.Content;
        }

        private void ConfirmationNoButton_Click(object sender, RoutedEventArgs e)
        {
            this.actionButton.Height = Double.NaN;
            this.actionConfirmationView.Height = 0.0;

            this.Update();
        }

        private void ConfirmationYesButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.pendingItemStackPanel.Children.Count > 0)
            {
                this.FinalizeOrder();
            }
            else if (this.finalizedItemStackPanel.Children.Count > 0)
            {
                Menu menu = (Menu) ((Grid)this.Parent).Parent;
                menu.paymentAction.startPayment();


               /* Task.Delay(5000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        menu.paymentAction.Visibility = Visibility.Hidden;
                    });
                });*/

               //this.Reset();
            }
        }

        private void OrderItemCell_OnAction(OrderItemCell sender, OrderItemCell.ActionType action)
        {
            switch (action)
            {
                case OrderItemCell.ActionType.Edit:
                    MessageBox.Show("TODO: Bring up options menu again");
                    break;
                case OrderItemCell.ActionType.Delete:
                    if (!sender.orderItem.isFinalized)
                        this.Remove(sender);
                    break;
                case OrderItemCell.ActionType.RequestServer:
                    MessageBox.Show("Server is on the way!");
                    break;
            }
        }
    }
}
