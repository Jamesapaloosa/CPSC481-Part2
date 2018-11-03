using System;
using System.Windows;
using System.Windows.Controls;

namespace CPSC_481
{
    public delegate void OrderCellEventHandler(OrderCell sender, EventArgs e);





    public partial class OrderCell : UserControl
    {
        public OrderItem orderItem;
        public event OrderCellEventHandler OnAction;

        public OrderCell(OrderItem orderItem)
        {
            InitializeComponent();

            this.orderItem = orderItem;

            this.titleLabel.Content = orderItem.menuItem.name;
            this.priceLabel.Content = orderItem.menuItem.price;
        }

        private void actionButton_Click(object sender, RoutedEventArgs e)
        {
            this.OnAction(this, new EventArgs());
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
