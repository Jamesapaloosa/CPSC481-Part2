using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Menu()
        {
            InitializeComponent();

            this.tabControl.ItemsSource = Enum.GetValues(typeof(MenuItem.Type));

            CollectionView collectionViewSource = (CollectionView)CollectionViewSource.GetDefaultView(this.loadMenuItems());
            collectionViewSource.GroupDescriptions.Add(new PropertyGroupDescription("type"));
            this.menuItemListView.ItemsSource = collectionViewSource;
        }

        private List<MenuItem> loadMenuItems()
        {
            var items = new List<MenuItem>();

            // Mains
            items.Add(new MenuItem("Burger", "description", 19.99m, MenuItem.Type.Main, @"Images\burger.jpg"));
            items.Add(new MenuItem("Chicken Soup", "description", 9.99m, MenuItem.Type.Main, @"Images\chickensoup.jpg"));
            items.Add(new MenuItem("Mom's Spaghetti", "description", 9.99m, MenuItem.Type.Main, @"Images\momSpaghetti.jpg"));
            items.Add(new MenuItem("Boston Beef Cake", "description", 9.99m, MenuItem.Type.Main, @"Images\bostonBeefCake.jpg"));
            items.Add(new MenuItem("Rib eye Steak", "description", 9.99m, MenuItem.Type.Main, @"Images\Ribeye.jpg"));

            // Sides
            items.Add(new MenuItem("Fries", "description", 9.99m, MenuItem.Type.Side, @"Images\fries.jpg"));
            items.Add(new MenuItem("Onion Rings", "description", 9.99m, MenuItem.Type.Side, @"Images\onionRings.jpg"));
            items.Add(new MenuItem("Chips", "description", 9.99m, MenuItem.Type.Side, @"Images\chips.jpg"));
            items.Add(new MenuItem("Salad", "description", 9.99m, MenuItem.Type.Side, @"Images\salad.jpg"));

            // Desserts
            items.Add(new MenuItem("Pie", "description", 9.99m, MenuItem.Type.Dessert, @"Images\pie.jpg"));
            items.Add(new MenuItem("Chocolate Cake", "description", 9.99m, MenuItem.Type.Dessert, @"Images\cake.jpg"));
            items.Add(new MenuItem("Brownie", "description", 9.99m, MenuItem.Type.Dessert, @"Images\brownie.jpg"));
            items.Add(new MenuItem("Cheesecake", "description", 9.99m, MenuItem.Type.Dessert, @"Images\cheesecake.jpg"));

            // Drinks
            items.Add(new MenuItem("Coke", "description", 9.99m, MenuItem.Type.Drink, @"Images\coke.jpg"));
            items.Add(new MenuItem("Pepsi", "description", 9.99m, MenuItem.Type.Drink, @"Images\Pepsi.jpg"));
            items.Add(new MenuItem("Whiskey", "description", 9.99m, MenuItem.Type.Drink, @"Images\whiskey.jpg"));
            items.Add(new MenuItem("Scotch", "description", 9.99m, MenuItem.Type.Drink, @"Images\scotch.png"));

            return items;
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MenuItem.Type menuItemType = (MenuItem.Type)this.tabControl.SelectedItem;
                CollectionView collectionViewSource = (CollectionView)this.menuItemListView.ItemsSource;
                CollectionViewGroup group = (CollectionViewGroup)collectionViewSource.Groups[this.tabControl.SelectedIndex];
                var menuItem = group.Items[0];
                this.menuItemListView.ScrollIntoView(menuItem);
                ListViewItem listViewItem = this.menuItemListView.ItemContainerGenerator.ContainerFromItem(menuItem) as ListViewItem;
                listViewItem.Focus();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
        }

        private void menuItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MenuItem menuItem = (MenuItem)this.menuItemListView.SelectedItem;
                this.orderTableView.add(menuItem);
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
        }

        private void SendOrPayButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
