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

            MenuItem burger = new MenuItem("Burger", 16.00m);
            MenuItem chickenSoup = new MenuItem("Chicken Soup", 6.00m);
            MenuItem bostonBeefCake = new MenuItem("Boston Beef Cake", 12.00m);
            MenuItem ribEyeSteak = new MenuItem("Rib eye Steak", 26.00m);
            MenuItem whiskey = new MenuItem("Whiskey", 126.00m);

            this.listBox.Items.Add(burger.name);
            this.listBox.Items.Add(chickenSoup.name);
            this.listBox.Items.Add(bostonBeefCake.name);
            this.listBox.Items.Add(ribEyeSteak.name);

            this.orderTableView.add(burger);
            this.orderTableView.add(chickenSoup);
            this.orderTableView.add(bostonBeefCake);
            this.orderTableView.add(ribEyeSteak);
            this.orderTableView.add(whiskey);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
