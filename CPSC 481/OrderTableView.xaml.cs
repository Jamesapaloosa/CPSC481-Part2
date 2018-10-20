using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace CPSC_481
{
    public partial class OrderTableView : UserControl
    {
        private List<MenuItem> menuItems;

        public OrderTableView()
        {
            InitializeComponent();

            this.menuItems = new List<MenuItem>();
        }

		public void add(MenuItem menuItem)
		{
			this.menuItems.Add(menuItem);         



            OrderCell orderCell = new OrderCell(menuItem);
            this.stackPanel.Children.Add(orderCell);         



			decimal totalPrice = 0.0m;
            foreach (var item in this.menuItems) {
				totalPrice += item.price;
            }
            this.totalPriceLabel.Content = String.Format("{0:C}", totalPrice);
		}
    }
}
