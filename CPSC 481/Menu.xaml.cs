using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace CPSC_481
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        RadioButton[,] rbs = new RadioButton[5, 5];
        TextBlock[] optionLabels = new TextBlock[5];
        string[] optionTitles = new string[5];
        string[,] optionOptions = new string[5, 5];
        decimal[,] optionPrices = new decimal[5, 5];
        decimal baseCost = 0;

        Thickness defaultCorner = new Thickness(28, 37, 0, 0);
        Thickness optionsCorner = new Thickness(0, 0, 0, 0);

        int columnGap = 20;
        int optionHeight = 17;
        MenuObject currentSelection;
        public int[] oldOrder = new int[5];
        bool editingOldOrder = false;

        Boolean isScrolling = false;

        double prevTouchPoint = 0;

        public Menu()
        {
            InitializeComponent();

            this.tabControl.ItemsSource = Enum.GetValues(typeof(MenuObject.Type));

            CollectionView collectionViewSource = (CollectionView)CollectionViewSource.GetDefaultView(this.LoadMenuItems());
            collectionViewSource.GroupDescriptions.Add(new PropertyGroupDescription("type"));
            this.menuItemListView.ItemsSource = collectionViewSource;

           // OptionsPopUp.SetupOptionsMenu(this, (this.LoadMenuItems()).ElementAt(0), orderTableView);
        }

        private List<MenuObject> LoadMenuItems()
        {
#pragma warning disable IDE0028 // Simplify collection initialization
            var items = new List<MenuObject>();
#pragma warning restore IDE0028 // Simplify collection initialization

            // Mains
            items.Add(new MenuObject("Burger", "description", 19.99m, MenuObject.Type.Main, @"Images\burger.jpg", LoadOptionsFromFile(@"Options\burger.txt"), this, orderTableView));
            items.Add(new MenuObject("Chicken Soup", "description", 9.99m, MenuObject.Type.Main, @"Images\chickensoup.jpg", LoadOptionsFromFile(@"Options\chickensoup.txt"), this, orderTableView));
            items.Add(new MenuObject("Mom's Spaghetti", "description", 9.99m, MenuObject.Type.Main, @"Images\momSpaghetti.jpg", LoadOptionsFromFile(@"Options\momSpaghetti.txt"), this, orderTableView));
            items.Add(new MenuObject("Boston Beef Cake", "description", 9.99m, MenuObject.Type.Main, @"Images\bostonBeefCake.jpg", LoadOptionsFromFile(@"Options\bostonBeefCake.txt"), this, orderTableView));
            items.Add(new MenuObject("Rib eye Steak", "description", 9.99m, MenuObject.Type.Main, @"Images\Ribeye.jpg", LoadOptionsFromFile(@"Options\ribeye.txt"), this, orderTableView));
            items.Add(new MenuObject("Taco", "description", 9.99m, MenuObject.Type.Main, @"Images\taco.jpg", LoadOptionsFromFile(@"Options\ribeye.txt"), this, orderTableView));
            items.Add(new MenuObject("Pizza", "description", 9.99m, MenuObject.Type.Main, @"Images\pizza.jpg", LoadOptionsFromFile(@"Options\ribeye.txt"), this, orderTableView));

            // Sides
            items.Add(new MenuObject("Fries", "description", 9.99m, MenuObject.Type.Side, @"Images\fries.jpg", LoadOptionsFromFile(@"Options\fries.txt"), this, orderTableView));
            items.Add(new MenuObject("Onion Rings", "description", 9.99m, MenuObject.Type.Side, @"Images\onionRings.jpg", LoadOptionsFromFile(@"Options\onionRings.txt"), this, orderTableView));
            items.Add(new MenuObject("Chips", "description", 9.99m, MenuObject.Type.Side, @"Images\chips.jpg", LoadOptionsFromFile(@"Options\chips.txt"), this, orderTableView));
            items.Add(new MenuObject("Salad", "description", 9.99m, MenuObject.Type.Side, @"Images\salad.jpg", LoadOptionsFromFile(@"Options\salad.txt"), this, orderTableView));
            items.Add(new MenuObject("Gravy", "description", 9.99m, MenuObject.Type.Side, @"Images\gravy.jpg", LoadOptionsFromFile(@"Options\salad.txt"), this, orderTableView));
            items.Add(new MenuObject("Bacon", "description", 9.99m, MenuObject.Type.Side, @"Images\bacon.jpg", LoadOptionsFromFile(@"Options\salad.txt"), this, orderTableView));
            items.Add(new MenuObject("Potatoes", "description", 9.99m, MenuObject.Type.Side, @"Images\potatoes.jpg", LoadOptionsFromFile(@"Options\salad.txt"), this, orderTableView));

            // Desserts
            items.Add(new MenuObject("Pie", "description", 9.99m, MenuObject.Type.Dessert, @"Images\pie.jpg", LoadOptionsFromFile(@"Options\pie.txt"), this, orderTableView));
            items.Add(new MenuObject("Chocolate Cake", "description", 9.99m, MenuObject.Type.Dessert, @"Images\cake.jpg", LoadOptionsFromFile(@"Options\cake.txt"), this, orderTableView));
            items.Add(new MenuObject("Brownie", "description", 9.99m, MenuObject.Type.Dessert, @"Images\brownie.jpg", LoadOptionsFromFile(@"Options\brownie.txt"), this, orderTableView));
            items.Add(new MenuObject("Cheesecake", "description", 9.99m, MenuObject.Type.Dessert, @"Images\cheesecake.jpg", LoadOptionsFromFile(@"Options\cheesecake.txt"), this, orderTableView));
            items.Add(new MenuObject("Ice Cream", "description", 9.99m, MenuObject.Type.Dessert, @"Images\icecream.jpg", LoadOptionsFromFile(@"Options\cheesecake.txt"), this, orderTableView));
            items.Add(new MenuObject("Donught", "description", 9.99m, MenuObject.Type.Dessert, @"Images\donught.jpg", LoadOptionsFromFile(@"Options\cheesecake.txt"), this, orderTableView));
            items.Add(new MenuObject("Candy", "description", 9.99m, MenuObject.Type.Dessert, @"Images\candy.jpg", LoadOptionsFromFile(@"Options\cheesecake.txt"), this, orderTableView));

            // Drinks
            items.Add(new MenuObject("Coke", "description", 9.99m, MenuObject.Type.Drink, @"Images\coke.png", LoadOptionsFromFile(@"Options\coke.txt"), this, orderTableView));
            items.Add(new MenuObject("Pepsi", "description", 9.99m, MenuObject.Type.Drink, @"Images\Pepsi.jpg", LoadOptionsFromFile(@"Options\pepsi.txt"), this, orderTableView));
            items.Add(new MenuObject("Whiskey", "description", 9.99m, MenuObject.Type.Drink, @"Images\whiskey.jpg", LoadOptionsFromFile(@"Options\whiskey.txt"), this, orderTableView));
            items.Add(new MenuObject("Scotch", "description", 9.99m, MenuObject.Type.Drink, @"Images\scotch.png", LoadOptionsFromFile(@"Options\scotch.txt"), this, orderTableView));
            items.Add(new MenuObject("Premium Water", "description", 99.99m, MenuObject.Type.Drink, @"Images\water.jpg", LoadOptionsFromFile(@"Options\scotch.txt"), this, orderTableView));
            items.Add(new MenuObject("Tap Water", "description", 0.00m, MenuObject.Type.Drink, @"Images\water.jpg", LoadOptionsFromFile(@"Options\scotch.txt"), this, orderTableView));
            items.Add(new MenuObject("Tea", "description", 9.99m, MenuObject.Type.Drink, @"Images\tea.jpg", LoadOptionsFromFile(@"Options\scotch.txt"), this, orderTableView));

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
                for (int i = 0; (i < temp1.Length && (i < ret.GetLength(0))); i++)
                {
                    if (!(String.IsNullOrEmpty(temp1[i])))
                    {
                        string[] fullSplit = temp1[i].Split('_');
                        ret[i, 0, 0] = fullSplit[0];
                        for (int j = 1; ((j < fullSplit.Length) && (j < ret.GetLength(1))); j++)
                        {
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

        private static bool IsMenuVisible(FrameworkElement element, FrameworkElement container)
        {
            if (!element.IsVisible)
                return false;

            Rect bounds =
                element.TransformToAncestor(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
            var rect = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
            return rect.Contains(bounds.TopLeft) || rect.Contains(bounds.BottomRight);
        }

        private List<object> GetVisibleItemsFromListbox(ListBox listBox, FrameworkElement parentToTestVisibility)
        {
            var items = new List<object>();
            foreach (var item in menuItemListView.Items)
            {
                if (IsMenuVisible((ListBoxItem)listBox.ItemContainerGenerator.ContainerFromItem(item), parentToTestVisibility))
                    items.Add(item);
                else if (items.Any())
                    break;
            }

            return items;
        }

        private bool containsMoreThanHalf(List<object> group, List<object> visibleObjects)
        {
            int count = 0;
            foreach(object item in group)
            {
                foreach(object item2 in visibleObjects)
                {
                    if (item.Equals(item2))
                    {
                        count++;
                        break;
                    }
                }
            }
            if (count > (visibleObjects.Count / 2))
                return true;
            return false;
        }

        private bool tabIsAhead(object groupHead, object firstVisable)
        {
            object item;
            int headIndex = -1;
            int visableIndex = -1;
            for (int i = 0; i < menuItemListView.Items.Count; i++)
            {
                item = menuItemListView.Items[i];
                if (item.Equals(groupHead))
                    headIndex = i;
                else if (item.Equals(firstVisable))
                    visableIndex = i;
            }
            if (visableIndex > headIndex)
                return true;
            return false;
        }

        private bool checkIfItemIsInCollection(object desiredItem, List<object> collection)
        {
            foreach(object item in collection)
            {
                if (desiredItem.Equals(item))
                    return true;
            }
            return false;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isScrolling)
            {
                try
                {
                    MenuObject.Type menuItemType = (MenuObject.Type)this.tabControl.SelectedItem;
                    CollectionView collectionViewSource = (CollectionView)this.menuItemListView.ItemsSource;
                    CollectionViewGroup group = (CollectionViewGroup)collectionViewSource.Groups[this.tabControl.SelectedIndex];
                    List<object> Lgroup = new List<object>();
                    foreach (object item in group.Items)
                    {
                        Lgroup.Add(item);
                    }
                    List<object> visable = GetVisibleItemsFromListbox(menuItemListView, this);
                    bool pass = containsMoreThanHalf(Lgroup, visable);
                    if (!pass)
                    {

                        var offset = 0;


                        // Quick and dirty, just dont change the number of items.
                        switch (menuItemType)
                        {
                            case MenuObject.Type.Main:
                                offset = 0;
                                break;
                            case MenuObject.Type.Side:
                                offset = 1485;
                                break;
                            case MenuObject.Type.Dessert:
                                offset = 2975;
                                break;
                            case MenuObject.Type.Drink:
                                offset = 4465;
                                break;
                        }

                        menuItemScrollViewer.ScrollToVerticalOffset(offset);


                        /*if (tabIsAhead(Lgroup[0], visable[0]))
                        {
                            menuItem = group.Items[0];
                            if (!checkIfItemIsInCollection(menuItem, GetVisibleItemsFromListbox(menuItemListView, this)))
                            {
                                this.menuItemScrollViewer.PageUp();
                            }
                        }
                        else
                        {
                            menuItem = group.Items[6];
                            if (!checkIfItemIsInCollection(menuItem, GetVisibleItemsFromListbox(menuItemListView, this)))
                            {
                                this.menuItemScrollViewer.ScrollToVerticalOffset(200);
                            }
                        }*/


                        /*
                        this.menuItemListView.ScrollIntoView(menuItem);
                        ListViewItem listViewItem = this.menuItemListView.ItemContainerGenerator.ContainerFromItem(menuItem) as ListViewItem;
                        listViewItem.BringIntoView();
                        listViewItem.Focus();
                        */
                    }
                }
                catch (Exception error)
                {
                    System.Diagnostics.Debug.WriteLine(error);
                }
                
            }
        }

        /// <summary>
        /// Passes scroll input events to the scrollviewer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            menuItemScrollViewer.ScrollToVerticalOffset(menuItemScrollViewer.VerticalOffset - e.Delta);
        }

        private void menuItemListView_TouchDrag(object sender, TouchEventArgs t)
        {
            if (prevTouchPoint == 0)
            {
                prevTouchPoint = t.GetTouchPoint(this).Position.Y;
            }
            menuItemScrollViewer.ScrollToVerticalOffset(menuItemScrollViewer.VerticalOffset - (t.GetTouchPoint(this).Position.Y - prevTouchPoint));
            prevTouchPoint = t.GetTouchPoint(this).Position.Y;
        }


        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {


            try
            {
                isScrolling = true;
                List<object> visable = GetVisibleItemsFromListbox(menuItemListView, this);
                for (int i = 0; i < tabControl.Items.Count; i++) {
                    if (((MenuObject)visable[2]).type.Equals(tabControl.Items[i]))
                    {
                        tabControl.SelectedIndex = i;
                    }
                }
                isScrolling = false;
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }

            
        }

        public void toggleOptions(bool onVoff)
        {
            bool val;
            if (onVoff)
                val = true;
            else
                val = false;

            this.tabControl.IsEnabled = val;
            this.menuItemListView.IsEnabled = val;
            this.orderTableView.IsEnabled = val;
        }

        private void MenuItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //toggleOptions(false);


                /*Style style = new Style();
                style.TargetType = typeof(ListViewItem);
                style.Setters.Add(new Setter(ListViewItem.BackgroundProperty, Brushes.Pink));
                style.
                menuItemListView.ItemContainerStyle = style;*/


                MenuObject menuItem = (MenuObject)this.menuItemListView.SelectedItem;

                foreach (MenuObject element in menuItemListView.Items)
                {
                    element.collapseOptions();
                }

                if (menuItem.OptionsExpander.IsExpanded)
                {
                    menuItem.collapseOptions();
                } else
                {
                    menuItem.expandOptions();
                }

                int verticalOffset = 0;
                var temp = menuItemListView.SelectedIndex;

                if (menuItem.type == MenuObject.Type.Main)
                {
                    verticalOffset = menuItemListView.SelectedIndex * 222 + 90;
                } else if (menuItem.type == MenuObject.Type.Side)
                {
                    verticalOffset = menuItemListView.SelectedIndex * 222 + (90 * 2);
                } else if (menuItem.type == MenuObject.Type.Dessert)
                {
                    verticalOffset = menuItemListView.SelectedIndex * 222 + (90 * 3);
                } else
                {
                    verticalOffset = menuItemListView.SelectedIndex * 222 + (90 * 4);
                }


                //verticalOffset -= 615;

                /* menuItem.Transf
                Point relativePOint = menuItem.TransformToAncestor(menuItemScrollViewer).Transform(new Point(0, 0));*/

                //menuItem.optionsMenu.NewOptionsMenu();
                Thread.Sleep(300);
                menuItemScrollViewer.ScrollToVerticalOffset(verticalOffset);
     



                e.Handled = true;
                //OptionsPopUp.NewOptionsMenu(menuItem);
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
        }

   
        private void SendOrPayButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /*
        public void EditOptionsMenu(MenuItem item, int[] chosenOptions)
        {
            oldOrder = chosenOptions;
            //NewOptionsMenu(item);
            for (int i = 0; i < 5; i++)
            {
                if ((chosenOptions[i] >= 0) && (chosenOptions[i] < 5))
                {
                    rbs[i, chosenOptions[i]].IsChecked = true;
                }
            }
            editingOldOrder = true;
        }
        /*
        private void NewOptionsMenu(MenuItem item)
        {
            Reset();
            currentSelection = item;
            this.OptionsPopUp.Visibility = Visibility.Visible;
            //Canvas.SetZIndex(OptionsPopUp, 2);
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

            optionsCorner.Left = optionsCorner.Left + columnGap;

            specReqTitle.Margin = new Thickness(28, (optionsCorner.Top + optionHeight), 0, 0);
            specReqEntry.Margin = new Thickness(10, (optionsCorner.Top + 2 * optionHeight), 0, 0);
            specReqEntry.Width = optionsCorner.Left - 40;

            optionsCorner.Top = optionsCorner.Top + (3 * optionHeight) + specReqEntry.ActualHeight;

            double left = optionsCorner.Left / 2;

            quantityLabel.Margin = new Thickness((left - (quantityLabel.Width + quantitySub.Width) - 3), optionsCorner.Top, 0, 0);
            quantitySub.Margin = new Thickness((left - quantitySub.Width), optionsCorner.Top, 0, 0);
            quantityAmount.Margin = new Thickness(left, optionsCorner.Top, 0, 0);
            quantityAdd.Margin = new Thickness((left + quantityAmount.Width), optionsCorner.Top, 0, 0);

            totalTitle.Margin = new Thickness((left - totalTitle.Width), (optionsCorner.Top + quantityAmount.Height + 5), 0, 0);
            totalLabel.Margin = new Thickness(left, (optionsCorner.Top + quantityAmount.Height + 5), 0, 0);

            optionsCorner.Top = totalLabel.Margin.Top + optionHeight;

            addButton.Margin = new Thickness((optionsCorner.Left - addButton.ActualWidth), (optionsCorner.Top), 0, 0);
            cancelButton.Margin = new Thickness(0, (optionsCorner.Top), 0, 0);

            totalLabel.Text = baseCost.ToString("0.00");

            OptionsPopUp.Width = optionsCorner.Left;
            OptionsPopUp.Height = optionsCorner.Top + addButton.ActualHeight;

            OptionsPopUp.Margin = new Thickness(((this.Width / 2) - (OptionsPopUp.Width / 2)),
                                                ((this.Height / 2) - (OptionsPopUp.Height / 2)),
                                                0,
                                                0);
        }

        private void Reset()
        {
           // Canvas.SetZIndex(OptionsPopUp, -2);
           this.OptionsPopUp.Visibility = Visibility.Hidden;
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
            editingOldOrder = false;
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
            int.TryParse(quantityAmount.Text, out int x);
            quantityAmount.Text = (x + 1).ToString("0");
            UpdateTotal(sender, e);
        }

        private void DecreaseQuantity(object sender, RoutedEventArgs e)
        {
            int.TryParse(quantityAmount.Text, out int x);
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

        private void SpecReqEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            //specReqEntry.Margin = movedRequestLoc;
        }

        private void SpecReqEntry_LostFocus(object sender, RoutedEventArgs e)
        {
            //specReqEntry.Margin = defaultRequestLoc;
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            int[] options = { -1, -1, -1, -1, -1 };
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((rbs[i,j].IsChecked == true) && (rbs[i,j].IsVisible))
                    {
                        options[i] = j;
                    }
                }
            }

            try
            {
                // MenuItem menuItem = (MenuItem)this.menuItemListView.SelectedItem;
                OrderItem orderItem = new OrderItem(this.currentSelection, options, this);
                this.orderTableView.Add(orderItem);
                this.Reset();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
            toggleOptions(true);
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            if (editingOldOrder)
            {
                try
                {
                    // MenuItem menuItem = (MenuItem)this.menuItemListView.SelectedItem;
                    OrderItem orderItem = new OrderItem(this.currentSelection, oldOrder, this);
                    this.orderTableView.Add(orderItem);
                    this.Reset();
                }
                catch (Exception error)
                {
                    System.Diagnostics.Debug.WriteLine(error);
                }
            }
            else
            {
                this.Reset();
            }
            toggleOptions(true);
        }
        */
        private void menuItemListView_TouchUp(object sender, TouchEventArgs e)
        {
            prevTouchPoint = 0;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var temp = 5;
        }

        private void GridViewRowPresenter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var temp = 5;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var temp = 5;
        }

        private void ListViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {


            e.Handled = true;
        }
    }


}
