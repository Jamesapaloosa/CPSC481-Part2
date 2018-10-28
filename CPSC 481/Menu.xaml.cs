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
        public Menu()
        {
            InitializeComponent();
            addItemsToSingleList( "$9.99", "Z/image Burger",  "Burger" , 1);
            this.MealsListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Chicken Soup" });
            this.MealsListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Boston Beef Cake" });
            this.MealsListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Rib eye Steak" });
            this.MealsListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Mom's spaghetti" });

            this.DrinksListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Coke" });
            this.DrinksListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Pepsi" });
            this.DrinksListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Whisky" });
            this.DrinksListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Scotch" });

            this.SidesListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Fries" });
            this.SidesListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Onion Rings" });
            this.SidesListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Chips" });
            this.SidesListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Caesar Sald" });

            this.DessertListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Pie" });
            this.DessertListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Chocolate Cake" });
            this.DessertListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Brownie" });
            this.DessertListView.Items.Add(new MenuObject { Price = "$9.99", Image = "Z/image Burger", Description = "Cheese Cake" });

            this.listBox1.Items.Add("Whisky 12X - $126.00");
            this.TotalPriceLabel.Content = "$126.00";
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addItemToMenu(string ItemName, string photoPath)
        {

        }

        private void addItemsToSingleList(string price, string ImagePath, string description, int pageNumber)
        {
            switch(pageNumber)
            {
                case 1:
                    this.MealsListView.Items.Add(new MenuObject { Price = price, Image = ImagePath, Description = description });
                    break;
                case 2:
                    this.SidesListView.Items.Add(new MenuObject { Price = price, Image = ImagePath, Description = description });
                    break;
                case 3:
                    this.DessertListView.Items.Add(new MenuObject { Price = price, Image = ImagePath, Description = description });
                    break;
                case 4:
                    this.DrinksListView.Items.Add(new MenuObject { Price = price, Image = ImagePath, Description = description });
                    break;
                default:
                    break;
            }
        }

        private void addItemToAllLists(string price, string ImagePath, string description)
        {
            this.MealsListView.Items.Add(new MenuObject { Price = price, Image = ImagePath, Description = description });
            this.SidesListView.Items.Add(new MenuObject { Price = price, Image = ImagePath, Description = description });
            this.DessertListView.Items.Add(new MenuObject { Price = price, Image = ImagePath, Description = description });
            this.DrinksListView.Items.Add(new MenuObject { Price = price, Image = ImagePath, Description = description });
        }

        public class MenuObject
        {
            public string Image { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
        }
    }
}
