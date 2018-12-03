using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace CPSC_481
{
    public partial class OrderTableView : UserControl
    {
        public OrderTableView()
        {
            InitializeComponent();

            this.Reset();
        }

        private void Reset()
        {
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
            
            if (this.pendingItemStackPanel.Children.Count > 0)
            {
                this.pendingItemSectionHeader.Height = Double.NaN;
                this.actionGrid.Height = Double.NaN;
                this.actionButton.Content = "Order";
            }
            else if (this.finalizedItemStackPanel.Children.Count > 0)
            {
                this.finalizedItemSectionHeader.Height = Double.NaN;
                this.actionGrid.Height = Double.NaN;
                this.actionButton.Content = "Pay";
            }
            else
            {
                this.finalizedItemSectionHeader.Height = 0.0;
                this.pendingItemSectionHeader.Height = 0.0;
                this.actionGrid.Height = 0.0;
            }
        }

        public void Add(OrderItem orderItem)
		{
            OrderItemCell orderItemCell = new OrderItemCell(orderItem, this);

            orderItemCell.OnAction += this.OrderItemCell_OnAction;
            this.pendingItemStackPanel.Children.Add(orderItemCell);

			this.Update();
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
            if (this.pendingItemStackPanel.Children.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Confirm Order?",
                                          "PS. You can still order more items.",
                                          MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    this.FinalizeOrder();
                }
            }
            else if (this.finalizedItemStackPanel.Children.Count > 0)
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
