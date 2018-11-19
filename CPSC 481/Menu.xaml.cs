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
            
            // Options menu initialization

            rbs[0, 0] = rb0;
            rbs[0, 1] = rb1;
            rbs[0, 2] = rb2;
            rbs[0, 3] = rb3;
            rbs[0, 4] = rb4;
            rbs[1, 0] = rb5;
            rbs[1, 1] = rb6;
            rbs[1, 2] = rb7;
            rbs[1, 3] = rb8;
            rbs[1, 4] = rb9;
            rbs[2, 0] = rb10;
            rbs[2, 1] = rb11;
            rbs[2, 2] = rb12;
            rbs[2, 3] = rb13;
            rbs[2, 4] = rb14;
            rbs[3, 0] = rb15;
            rbs[3, 1] = rb16;
            rbs[3, 2] = rb17;
            rbs[3, 3] = rb18;
            rbs[3, 4] = rb19;
            rbs[4, 0] = rb20;
            rbs[4, 1] = rb21;
            rbs[4, 2] = rb22;
            rbs[4, 3] = rb23;
            rbs[4, 4] = rb24;
            optionLabels[0] = selection1Label;
            optionLabels[1] = selection2Label;
            optionLabels[2] = selection3Label;
            optionLabels[3] = selection4Label;
            optionLabels[4] = selection5Label;
            for (int i = 0; i < rbs.GetLength(0); i++)
            {
                string temp = "Option" + i.ToString();
                for (int j = 0; j < rbs.GetLength(1); j++)
                {
                    rbs[i, j].GroupName = temp;
                }
            }
            Reset();
        }

        private List<MenuItem> loadMenuItems()
        {
            var items = new List<MenuItem>();

            // Mains
            items.Add(new MenuItem("Burger", "description", 19.99m, MenuItem.Type.Main, @"Images\burger.jpg", LoadOptionsFromFile(@"Options\burger.txt")));
            items.Add(new MenuItem("Chicken Soup", "description", 9.99m, MenuItem.Type.Main, @"Images\chickensoup.jpg", LoadOptionsFromFile(@"Options\chickensoup.txt")));
            items.Add(new MenuItem("Mom's Spaghetti", "description", 9.99m, MenuItem.Type.Main, @"Images\momSpaghetti.jpg", LoadOptionsFromFile(@"Options\momSpaghetti.txt")));
            items.Add(new MenuItem("Boston Beef Cake", "description", 9.99m, MenuItem.Type.Main, @"Images\bostonBeefCake.jpg", LoadOptionsFromFile(@"Options\bostonBeefCake.txt")));
            items.Add(new MenuItem("Rib eye Steak", "description", 9.99m, MenuItem.Type.Main, @"Images\Ribeye.jpg", LoadOptionsFromFile(@"Options\ribeye.txt")));

            // Sides
            items.Add(new MenuItem("Fries", "description", 9.99m, MenuItem.Type.Side, @"Images\fries.jpg", LoadOptionsFromFile(@"Options\fries.txt")));
            items.Add(new MenuItem("Onion Rings", "description", 9.99m, MenuItem.Type.Side, @"Images\onionRings.jpg", LoadOptionsFromFile(@"Options\onionRings.txt")));
            items.Add(new MenuItem("Chips", "description", 9.99m, MenuItem.Type.Side, @"Images\chips.jpg", LoadOptionsFromFile(@"Options\chips.txt")));
            items.Add(new MenuItem("Salad", "description", 9.99m, MenuItem.Type.Side, @"Images\salad.jpg", LoadOptionsFromFile(@"Options\salad.txt")));

            // Desserts
            items.Add(new MenuItem("Pie", "description", 9.99m, MenuItem.Type.Dessert, @"Images\pie.jpg", LoadOptionsFromFile(@"Options\pie.txt")));
            items.Add(new MenuItem("Chocolate Cake", "description", 9.99m, MenuItem.Type.Dessert, @"Images\cake.jpg", LoadOptionsFromFile(@"Options\cake.txt")));
            items.Add(new MenuItem("Brownie", "description", 9.99m, MenuItem.Type.Dessert, @"Images\brownie.jpg", LoadOptionsFromFile(@"Options\brownie.txt")));
            items.Add(new MenuItem("Cheesecake", "description", 9.99m, MenuItem.Type.Dessert, @"Images\cheesecake.jpg", LoadOptionsFromFile(@"Options\cheesecake.txt")));

            // Drinks
            items.Add(new MenuItem("Coke", "description", 9.99m, MenuItem.Type.Drink, @"Images\coke.jpg", LoadOptionsFromFile(@"Options\coke.txt")));
            items.Add(new MenuItem("Pepsi", "description", 9.99m, MenuItem.Type.Drink, @"Images\Pepsi.jpg", LoadOptionsFromFile(@"Options\pepsi.txt")));
            items.Add(new MenuItem("Whiskey", "description", 9.99m, MenuItem.Type.Drink, @"Images\whiskey.jpg", LoadOptionsFromFile(@"Options\whiskey.txt")));
            items.Add(new MenuItem("Scotch", "description", 9.99m, MenuItem.Type.Drink, @"Images\scotch.png", LoadOptionsFromFile(@"Options\scotch.txt")));

            return items;
        }
        
        private string[,,] LoadOptionsFromFile(string path)
        {
            string[,,] ret = new string[5, 6, 2];
            try
            {
                string contents = File.ReadAllText(System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\")) + path);
                char[] deliminaters = new char[] { '\r', '\n' };
                string[] temp1 = contents.Split(deliminaters, StringSplitOptions.RemoveEmptyEntries);
                //System.Diagnostics.Debug.WriteLine("contents = " + contents.ToString());
                for (int i = 0; (i < temp1.Length && (i < ret.GetLength(0))); i++)
                {
                    if (!(String.IsNullOrEmpty(temp1[i])))
                    {
                        //System.Diagnostics.Debug.WriteLine("temp1 = " + temp1[i]);
                        string[] fullSplit = temp1[i].Split('_');
                        ret[i, 0, 0] = fullSplit[0];
                        for (int j = 1; ((j < fullSplit.Length) && (j < ret.GetLength(1))); j++)
                        {
                            //System.Diagnostics.Debug.WriteLine("Full Split = " + fullSplit[j]);
                            string[] temp2 = fullSplit[j].Split(',');
                            if (temp2.Length == 2)
                            {
                                ret[i, j, 0] = temp2[0];
                                ret[i, j, 1] = temp2[1];
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Incorrect options formatting in " + path + " " + temp2.ToString());
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return ret;
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
        
        private void NewOptionsMenu(MenuItem item)
        {
            Reset();
            currentSelection = item;
            Canvas.SetZIndex(OptionsPopUp, 2);
            addButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;
            quantityAdd.Visibility = Visibility.Visible;
            quantityAmount.Visibility = Visibility.Visible;
            quantitySub.Visibility = Visibility.Visible;
            quantityLabel.Visibility = Visibility.Visible;
            specReqTitle.Visibility = Visibility.Visible;
            specReqEntry.Visibility = Visibility.Visible;
            totalTitle.Visibility = Visibility.Visible;
            totalLabel.Visibility = Visibility.Visible;
            itemTitle.Visibility = Visibility.Visible;
            itemTitle.Text = item.name;
            baseCost = item.price;
            //System.Diagnostics.Debug.WriteLine("x = " + item.options[0, 0, 0].ToString());
            for (int i = 0; ((i < rbs.GetLength(0)) && !(String.IsNullOrEmpty(item.options[i,0,0]))); i++)
            {
                rbs[i, 0].IsChecked = true;
                optionLabels[i].Text = item.options[i,0,0];
                optionLabels[i].Visibility = Visibility.Visible;
                optionsCorner.Left = optionLabels[i].Margin.Left + optionLabels[i].Width;
                for (int j = 0; ((j < rbs.GetLength(1)) && !(String.IsNullOrEmpty(item.options[i, (j+1), 0]))); j++)
                {
                    optionOptions[i, j] = item.options[i, (j+1), 0];
                    Decimal.TryParse((item.options[i, (j+1), 1]), out optionPrices[i, j]);
                    if (optionPrices[i, j] > 0)
                    {
                        rbs[i, j].Content = item.options[i, (j+1), 0] + " (+$" + optionPrices[i, j].ToString("0.00") + ")";
                    }
                    else
                        rbs[i, j].Content = item.options[i, (j+1), 0];

                    rbs[i, j].Visibility = Visibility.Visible;

                    if (rbs[i, j].Margin.Top > optionsCorner.Top)
                        optionsCorner.Top = rbs[i, j].Margin.Top + rbs[i, j].Height;
                    if ((rbs[i, j].Margin.Left + rbs[i, j].Width) > optionsCorner.Left)
                        optionsCorner.Left = rbs[i, j].Margin.Left + rbs[i, j].Width;
                }
            }

            OptionsPopUp.Width = optionsCorner.Left + columnGap;
            OptionsPopUp.Height = (optionsCorner.Top) + 256;

            OptionsPopUp.Margin = new Thickness(((MainWindow.Width / 2) - (OptionsPopUp.Width / 2)),
                                                ((MainWindow.Height / 2) - (OptionsPopUp.Height / 2)), 
                                                0, 
                                                0);

            specReqTitle.Margin = new Thickness(28, (optionsCorner.Top + optionHeight), 0, 0);
            specReqEntry.Margin = new Thickness(10, (optionsCorner.Top + 2 * optionHeight), 0, 0);
            specReqEntry.Width = OptionsPopUp.Width - 40;

            double left = (OptionsPopUp.Width / 2);
            double top = OptionsPopUp.Height - 130;

            quantityLabel.Margin = new Thickness((left - (quantityLabel.Width + quantitySub.Width) - 3), top, 0, 0);
            quantitySub.Margin = new Thickness((left - quantitySub.Width), top, 0, 0);
            quantityAmount.Margin = new Thickness(left, top, 0, 0);
            quantityAdd.Margin = new Thickness((left + quantityAmount.Width), top, 0, 0);

            totalTitle.Margin = new Thickness((left - totalTitle.Width), (top + quantityAmount.Height + 5), 0, 0);
            totalLabel.Margin = new Thickness(left, (top + quantityAmount.Height + 5), 0, 0);


            addButton.Margin = new Thickness((OptionsPopUp.Width - 75), (OptionsPopUp.Height - 75), 0, 0);
            cancelButton.Margin = new Thickness(10, (OptionsPopUp.Height - 75), 0, 0);

            totalLabel.Text = baseCost.ToString("0.00");
        }

        private void Reset()
        {
            Canvas.SetZIndex(OptionsPopUp, -2);
            addButton.Visibility = Visibility.Hidden;
            cancelButton.Visibility = Visibility.Hidden;
            quantityAdd.Visibility = Visibility.Hidden;
            quantityAmount.Visibility = Visibility.Hidden;
            quantityAmount.Text = "1";
            quantitySub.Visibility = Visibility.Hidden;
            quantityLabel.Visibility = Visibility.Hidden;
            specReqTitle.Visibility = Visibility.Hidden;
            specReqEntry.Visibility = Visibility.Hidden;
            specReqEntry.Text = "";
            totalTitle.Visibility = Visibility.Hidden;
            totalLabel.Visibility = Visibility.Hidden;
            itemTitle.Visibility = Visibility.Hidden;
            optionsCorner = new Thickness(defaultCorner.Left + rbs[0,0].Width + columnGap, defaultCorner.Top, 0, 0);
            for (int i = 0; i < rbs.GetLength(0); i++)
            {
                optionLabels[i].Visibility = Visibility.Hidden;
                optionLabels[i].Text = "";
                for (int j = 0; j < rbs.GetLength(1); j++)
                {
                    rbs[i, j].Visibility = Visibility.Hidden;
                    rbs[i, j].Content = "";
                }
            }
        }

        private void IncreaseQuantity(object sender, RoutedEventArgs e)
        {
            int x = 0;
            int.TryParse(quantityAmount.Text, out x);
            quantityAmount.Text = (x + 1).ToString("0");
            UpdateTotal(sender, e);
        }

        private void DecreaseQuantity(object sender, RoutedEventArgs e)
        {
            int x = 0;
            int.TryParse(quantityAmount.Text, out x);
            x--;
            if (x < 1)
            {
                x = 1;
            }
            quantityAmount.Text = x.ToString("0");
            UpdateTotal(sender, e);
        }

        private void UpdateTotal(object sender, RoutedEventArgs e)
        {
            decimal temp = baseCost;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((rbs[i, j].IsChecked == true) && (rbs[i, j].Visibility.Equals(Visibility.Visible)))
                    {
                        temp = temp + optionPrices[i, j];
                    }
                }
            }
            temp = temp * (Convert.ToInt32(quantityAmount.Text));
            totalLabel.Text = temp.ToString("0.00");
        }

        private void specReqEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            //specReqEntry.Margin = movedRequestLoc;
        }

        private void specReqEntry_LostFocus(object sender, RoutedEventArgs e)
        {
            //specReqEntry.Margin = defaultRequestLoc;
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            int[] selected = { -1, -1, -1, -1, -1 };
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((rbs[i,j].IsChecked == true) && (rbs[i,j].IsVisible))
                    {
                        selected[i] = j;
                    }
                }
            }
            //this.orderTableView.add(menuItem, selected);
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Reset();
        }
    }
}
