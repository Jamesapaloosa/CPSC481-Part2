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

            this.Reset();
        }

        private void Reset()
        {
            this.totalPrice = 0.0m;
            this.pendingOrderItemCount = 0;
            this.finalizedOrderItemCount = 0;

            this.Update();
        }

        private void Update()
        {
            this.totalPriceLabel.Content = String.Format("{0:C}", this.totalPrice);

            this.actionButton.IsEnabled = !(this.stackPanel.Children.Count == 0);

            if (this.pendingOrderItemCount > 0)
                this.actionButton.Content = "Order";
            else if (this.finalizedOrderItemCount > 0)
                this.actionButton.Content = "Pay";
        }

        public void Add(OrderItem orderItem)
		{
            OrderItemCell orderItemCell = new OrderItemCell(orderItem, this);

            orderItemCell.OnAction += this.OrderItemCell_OnAction;
            this.stackPanel.Children.Add(orderItemCell);
            
            this.totalPrice += orderItem.totalPrice;
            this.pendingOrderItemCount += 1;

			this.Update();
		}

        public void Remove(OrderItemCell orderItemCell)
        {
            this.stackPanel.Children.Remove(orderItemCell);

            this.totalPrice -= orderItemCell.orderItem.menuItem.price;
            this.pendingOrderItemCount -= 1;

            this.Update();
        }

        private void FinalizeOrder()
        {
            this.finalizedOrderItemCount += this.pendingOrderItemCount;
            this.pendingOrderItemCount = 0;

            foreach (var child in this.stackPanel.Children)
            {
                OrderItemCell orderItemCell = (OrderItemCell)child;
                orderItemCell.orderItem.isFinalized = true;
                orderItemCell.update();
                //orderItemCell.IsEnabled = !orderItemCell.orderItem.isFinalized;
            }

            this.Update();
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.pendingOrderItemCount > 0)
            {
                MessageBoxResult result = MessageBox.Show("Confirm Order?",
                                          "PS. You can still order more items.",
                                          MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    this.FinalizeOrder();
                }
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
