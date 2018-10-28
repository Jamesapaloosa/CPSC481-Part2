using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace CPSC_481
{
    public partial class OrderTableView : UserControl
    {
        private decimal totalPrice;
        private int pendingOrderItemCount;
        private int finalizedOrderItemCount;

        public OrderTableView()
        {
            InitializeComponent();

            this.reset();
        }

        private void reset()
        {
            this.totalPrice = 0.0m;
            this.pendingOrderItemCount = 0;
            this.finalizedOrderItemCount = 0;

            this.update();
        }

		public void add(MenuItem menuItem)
		{
            OrderItem orderItem = new OrderItem(menuItem);
            OrderCell orderCell = new OrderCell(orderItem);
            orderCell.OnDelete += this.orderCell_Delete;
            this.stackPanel.Children.Add(orderCell);

            this.totalPrice += menuItem.price;
            this.pendingOrderItemCount += 1;

			this.update();
		}

		public void update()
		{
            this.totalPriceLabel.Content = String.Format("{0:C}", this.totalPrice);
            
            this.actionButton.IsEnabled = !(this.stackPanel.Children.Count == 0);

            if (this.pendingOrderItemCount > 0)
                this.actionButton.Content = "Order";
            else if (this.finalizedOrderItemCount > 0)
                this.actionButton.Content = "Pay";
        }

        private void actionButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.pendingOrderItemCount > 0)
            {
                this.finalizedOrderItemCount += this.pendingOrderItemCount;
                this.pendingOrderItemCount = 0;

                foreach (var child in this.stackPanel.Children)
                {
                    OrderCell orderCell = (OrderCell)child;
                    orderCell.orderItem.isFinalized = true;
                    orderCell.IsEnabled = !orderCell.orderItem.isFinalized;
                }

                this.update();
            }
            else if (this.finalizedOrderItemCount > 0)
            {
                MessageBoxResult result = MessageBox.Show("Close order and pay?",
                                          "Confirmation",
                                          MessageBoxButton.OKCancel,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    MessageBox.Show("Server is on the way!");
                }
            }
        }

        private void orderCell_Delete(OrderCell sender, EventArgs e)
        {
            if (sender.orderItem.isFinalized)
                return;

            this.stackPanel.Children.Remove(sender);

            this.totalPrice -= sender.orderItem.menuItem.price;
            this.pendingOrderItemCount -= 1;

            this.update();
        }
    }
}
