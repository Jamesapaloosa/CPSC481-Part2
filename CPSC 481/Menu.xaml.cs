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

            foreach (var menuItem in this.menuItems)
            {
                this.addItemToAllLists(menuItem);
            }

            //addItemToAllLists("", "", "Meals");
            //addItemToAllLists( "$9.99", @"burger.jpg",  "Burger");
            //addItemToAllLists("$9.99", @"chickensoup.jpg", "Chicken Soup");
            //addItemToAllLists("$9.99", @"momSpaghetti.jpg", "Mom's spaghetti");
            //addItemToAllLists("$9.99", @"Ribeye.jpg", "Rib eye Steak");
            //addItemToAllLists("$9.99", @"bostonBeefCake.jpg", "Boston Beef Cake");

            //addItemToAllLists("", "", "Sides");

            //addItemToAllLists("$9.99", @"fries.jpg", "Fries");
            //addItemToAllLists("$9.99", @"onionRings.jpg", "Onion Rings");
            //addItemToAllLists("$9.99", @"chips.jpg", "Chips");
            //addItemToAllLists("$9.99", @"salad.jpg", "Caesar Salad");

            //addItemToAllLists("", "", "Desserts");

            //addItemToAllLists("$9.99", @"pie.jpg", "Pie");
            //addItemToAllLists("$9.99", @"cake.jpg", "Chocolate Cake");
            //addItemToAllLists("$9.99", @"brownie.jpg", "Brownie");
            //addItemToAllLists("$9.99", @"cheesecake.jpg", "Cheesecake");

            //addItemToAllLists("", "", "Drinks");

            //addItemToAllLists("$9.99", @"coke.png", "Coke");
            //addItemToAllLists("$9.99", @"Pepsi.jpg", "Pepsi");
            //addItemToAllLists("$9.99", @"whiskey.jpg", "Whisky");
            //addItemToAllLists("$9.99", @"scotch.png", "Scotch");


            //addItemToOrder("Whiskey", "10", "$99.90");
            //this.TotalPriceLabel.Content = "$99.90";
        }

        private void loadMenuItems()
        {
            //addItemToAllLists("", "", "Meals");
            //addItemToAllLists( "$9.99", @"burger.jpg",  "Burger");
            //addItemToAllLists("$9.99", @"chickensoup.jpg", "Chicken Soup");
            //addItemToAllLists("$9.99", @"momSpaghetti.jpg", "Mom's spaghetti");
            //addItemToAllLists("$9.99", @"Ribeye.jpg", "Rib eye Steak");
            //addItemToAllLists("$9.99", @"bostonBeefCake.jpg", "Boston Beef Cake");

            //addItemToAllLists("", "", "Sides");

            //addItemToAllLists("$9.99", @"fries.jpg", "Fries");
            //addItemToAllLists("$9.99", @"onionRings.jpg", "Onion Rings");
            //addItemToAllLists("$9.99", @"chips.jpg", "Chips");
            //addItemToAllLists("$9.99", @"salad.jpg", "Caesar Salad");

            //addItemToAllLists("", "", "Desserts");

            //addItemToAllLists("$9.99", @"pie.jpg", "Pie");
            //addItemToAllLists("$9.99", @"cake.jpg", "Chocolate Cake");
            //addItemToAllLists("$9.99", @"brownie.jpg", "Brownie");
            //addItemToAllLists("$9.99", @"cheesecake.jpg", "Cheesecake");

            //addItemToAllLists("", "", "Drinks");

            //addItemToAllLists("$9.99", @"coke.png", "Coke");
            //addItemToAllLists("$9.99", @"Pepsi.jpg", "Pepsi");
            //addItemToAllLists("$9.99", @"whiskey.jpg", "Whisky");
            //addItemToAllLists("$9.99", @"scotch.png", "Scotch");



            // Mains
            this.menuItems.Add(new MenuItem("Burger", "description", 9.99m, MenuItem.Type.Main, @"Images\burger.jpg"));
            this.menuItems.Add(new MenuItem("Chicken Soup", "description", 9.99m, MenuItem.Type.Main, @"Images\chickensoup.jpg"));
            this.menuItems.Add(new MenuItem("Mom's Spaghetti", "description", 9.99m, MenuItem.Type.Main, @"Images\momSpaghetti.jpg"));
            this.menuItems.Add(new MenuItem("Boston Beef Cake", "description", 9.99m, MenuItem.Type.Main, @"ImagesostonBeefCake.jpg"));
            this.menuItems.Add(new MenuItem("Rib eye Steak", "description", 9.99m, MenuItem.Type.Main, @"Images\Ribeye.jpg"));
                              
             // Sides
            this.menuItems.Add(new MenuItem("Fries", "description", 9.99m, MenuItem.Type.Side, @"Images\fries.jpg"));
            this.menuItems.Add(new MenuItem("Onion Rings", "description", 9.99m, MenuItem.Type.Side, @"Images\onionRings.jpg"));
            this.menuItems.Add(new MenuItem("Chips", "description", 9.99m, MenuItem.Type.Side, @"Images\chips.jpg"));
            this.menuItems.Add(new MenuItem("Salad", "description", 9.99m, MenuItem.Type.Side, @"Images\salad.jpg"));

             // Desserts
            this.menuItems.Add(new MenuItem("Pie", "description", 9.99m, MenuItem.Type.Dessert, @"Images\pie.jpg"));
            this.menuItems.Add(new MenuItem("Chocolate Cake", "description", 9.99m, MenuItem.Type.Dessert, @"Images\cake.jpg"));
            this.menuItems.Add(new MenuItem("Brownie", "description", 9.99m, MenuItem.Type.Dessert, @"Images\brownie.jpg"));
            this.menuItems.Add(new MenuItem("Cheesecake", "description", 9.99m, MenuItem.Type.Dessert, @"Images\cheesecake.jpg"));
            
             // Drinks
            this.menuItems.Add(new MenuItem("Coke", "description", 9.99m, MenuItem.Type.Drink, @"Images\coke.jpg"));
            this.menuItems.Add(new MenuItem("Pepsi", "description", 9.99m, MenuItem.Type.Drink, @"Images\Pepsi.jpg"));
            this.menuItems.Add(new MenuItem("Whiskey", "description", 9.99m, MenuItem.Type.Drink, @"Images\whiskey.jpg"));
            this.menuItems.Add(new MenuItem("Scotch", "description", 9.99m, MenuItem.Type.Drink, @"Images\scotch.jpg"));
        }

        private void addItemsToSingleList(string price, string ImagePath, string description, int pageNumber)
        {
            switch(pageNumber)
            {
                case 1:
                    this.MealsListView.Items.Add(new MenuObject { Price2 = price, path = ImagePath, Description = description });
                    break;
                case 2:
                    this.SidesListView.Items.Add(new MenuObject { Price2 = price, path = ImagePath, Description = description });
                    break;
                case 3:
                    this.DessertListView.Items.Add(new MenuObject { Price2 = price, path = ImagePath, Description = description });
                    break;
                case 4:
                    this.DrinksListView.Items.Add(new MenuObject { Price2 = price, path = ImagePath, Description = description });
                    break;
                default:
                    break;
            }
        }

        private void addItemToAllLists(MenuItem menuItem)
        {
            this.MealsListView.Items.Add(menuItem);
            this.SidesListView.Items.Add(menuItem);
            this.DessertListView.Items.Add(menuItem);
            this.DrinksListView.Items.Add(menuItem);
        }

        public class OrderObject
        {
            public string Item { get; set; }
            public string Number { get; set; }
            public string Total { get; set; }
        }

        public class MenuObject
        {
            public string path { get; set; }
            public string Description { get; set; }
            public string Price2 { get; set; }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView listView = (ListView)sender;
                MenuItem menuItem = this.menuItems.ElementAt(listView.SelectedIndex);
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
