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
using System.Windows.Shapes;

namespace CPSC_481
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private List<MenuItem> menuItems = new List<MenuItem>();

        public Menu()
        {
            InitializeComponent();

            this.loadMenuItems();

            foreach (var item in this.menuItems)
            {
                this.listBox.Items.Add(item.name);
            }
        }

        private void loadMenuItems()
        {
            this.menuItems.Add(new MenuItem("Burger", MenuItem.Type.Food, 16.00m));
            this.menuItems.Add(new MenuItem("Chicken Soup", MenuItem.Type.Food, 6.00m));
            this.menuItems.Add(new MenuItem("Boston Beef Cake", MenuItem.Type.Food, 12.00m));
            this.menuItems.Add(new MenuItem("Rib eye Steak", MenuItem.Type.Food, 26.00m));
            this.menuItems.Add(new MenuItem("Whiskey", MenuItem.Type.Drink, 126.00m));
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MenuItem menuItem = this.menuItems.ElementAt(this.listBox.SelectedIndex);
                this.OrderItemData.Content = menuItem.name;
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem menuItem = this.menuItems.ElementAt(this.listBox.SelectedIndex);
                this.orderTableView.add(menuItem);
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
        }
    }
}
