using System;
using System.Windows;
using System.Windows.Controls;

namespace CPSC_481
{
    public partial class OrderCell : UserControl
    {
        public OrderCell(MenuItem menuItem)
        {
            InitializeComponent();

            this.titleLabel.Content = menuItem.name;
            this.priceLabel.Content =  menuItem.price;
        }
    }
}
