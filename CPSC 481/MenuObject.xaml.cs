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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CPSC_481
{
    /// <summary>
    /// Interaction logic for MenuObject.xaml
    /// </summary>
    public partial class MenuObject : UserControl
    {

        public enum Type { Main, Side, Dessert, Drink };

        public String name { get; set; }
        public String description { get; set; }
        public decimal price { get; set; }
        public String priceAsString { get; set; } // Easiest way around stupid WPF issues. 
        public MenuObject.Type type { get; set; }
        public String imageName { get; set; }
        public string[,,] options = new string[5, 6, 2];
        public Menu menu;
        public OrderTableView orderTableView;
        public OptionsMenu optionsMenu;

        public MenuObject(String name, String description, decimal price, MenuObject.Type type, String imageName, string[,,] itemOptions, Menu menu, OrderTableView orderTableView)
        {
            InitializeComponent();

            this.name = name;
            //this.description = description;
            this.description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse interdum lacus libero, sed varius lorem varius id. Sed quis mi orci. Proin ultricies ligula ipsum. In congue mollis elit, non ornare nisl iaculis ut.";
            this.price = price;
            this.priceAsString = "$" + price.ToString("0.00");
            this.type = type;
            this.imageName = imageName;
            this.menu = menu;
            this.orderTableView = orderTableView;

            try
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (!(String.IsNullOrEmpty(itemOptions[i, j, 0])) && !(String.IsNullOrEmpty(itemOptions[i, j, 0])))
                        {
                            //System.Diagnostics.Debug.WriteLine("name = " + itemOptions[i, j, 0] + " price = " + itemOptions[i, j, 1]);
                            options[i, j, 0] = itemOptions[i, j, 0];
                            options[i, j, 1] = itemOptions[i, j, 1];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }


            optionsMenu = new OptionsMenu();
            optionsMenu.SetupOptionsMenu(this.menu, this, this.orderTableView);

            orderOptions.Children.Add(optionsMenu);
        }

        public void expandOptions()
        {
            OptionsExpander.IsExpanded = true;
        }

        public void collapseOptions()
        {

            OptionsExpander.IsExpanded = false;
        }
    }
}

