﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace CPSC_481
{
    public partial class OptionsMenu : UserControl
    {
        private MenuItem item;
        private OrderTableView orderTable;
        RadioButton[,] rbs = new RadioButton[5, 5];
        TextBlock[] optionLabels = new TextBlock[5];
        string[] optionTitles = new string[5];
        string[,] optionOptions = new string[5, 5];
        decimal[,] optionPrices = new decimal[5, 5];
        decimal baseCost = 0.0m;
        decimal optionsCost = 0.0m;

        Thickness defaultCorner;
        Thickness optionsCorner;

        int columnGap = 20;
        int optionHeight = 17;
        public int[] oldOrder = new int[5];
        bool editingOldOrder = false;
        Menu menu;

        public OptionsMenu()
        {
            InitializeComponent();

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

            defaultCorner = new Thickness((cancelButton.ActualWidth + addButton.ActualWidth + columnGap), 0, 0, 0);
            optionsCorner = defaultCorner;
        }

        public void SetupOptionsMenu(Menu theMenu, MenuItem sourceItem, OrderTableView destinationOrderTable)
        {
            item = sourceItem;
            orderTable = destinationOrderTable;
            this.menu = theMenu;

            this.NewOptionsMenu();
            this.Reset();
        }

        public void NewOptionsMenu(MenuItem newItem)
        {
            editingOldOrder = false;
            item = newItem;
            NewOptionsMenu();
        }

        public void NewOptionsMenu()
        {
            Reset();
            this.Visibility = Visibility.Visible;
            baseCost = item.price;
            for (int i = 0; ((i < rbs.GetLength(0)) && !(String.IsNullOrEmpty(item.options[i, 0, 0]))); i++)
            {
                rbs[i, 0].IsChecked = true;
                optionLabels[i].Text = item.options[i, 0, 0];
                optionLabels[i].Visibility = Visibility.Visible;
                optionsCorner.Top = rbs[i,0].Margin.Top + rbs[i,0].ActualHeight;
                for (int j = 0; ((j < rbs.GetLength(1)) && !(String.IsNullOrEmpty(item.options[i, (j + 1), 0]))); j++)
                {
                    optionOptions[i, j] = item.options[i, (j + 1), 0];
                    Decimal.TryParse((item.options[i, (j + 1), 1]), out optionPrices[i, j]);
                    if (optionPrices[i, j] > 0)
                    {
                        rbs[i, j].Content = item.options[i, (j + 1), 0] + " (+$" + optionPrices[i, j].ToString("0.00") + ")";
                    }
                    else
                        rbs[i, j].Content = item.options[i, (j + 1), 0];

                    rbs[i, j].Visibility = Visibility.Visible;

                    if (rbs[i, j].Margin.Left > optionsCorner.Left)
                        optionsCorner.Left = rbs[i, j].Margin.Left + rbs[i, j].Width;
                    if ((rbs[i, j].Margin.Top + rbs[i, j].ActualHeight) > optionsCorner.Top)
                        optionsCorner.Top = rbs[i, j].Margin.Top + rbs[i, j].ActualHeight;
                }
            }

            optionsCorner.Left = optionsCorner.Left + columnGap;

            specReqTitle.Margin = new Thickness(28, (optionsCorner.Top + optionHeight), 0, 0);
            specReqEntry.Margin = new Thickness(10, (optionsCorner.Top + 2 * optionHeight), 0, 0);
            specReqEntry.Width = optionsCorner.Left - 40;

            optionsCorner.Top = specReqEntry.Margin.Top + specReqEntry.ActualHeight + optionHeight;

            double left = optionsCorner.Left / 2;

            quantityLabel.Margin = new Thickness((left - (quantityLabel.Width + quantitySub.Width) - 3), optionsCorner.Top, 0, 0);
            quantitySub.Margin = new Thickness((left - quantitySub.Width), optionsCorner.Top, 0, 0);
            quantityAmount.Margin = new Thickness(left, optionsCorner.Top, 0, 0);
            quantityAdd.Margin = new Thickness((left + quantityAmount.Width), optionsCorner.Top, 0, 0);

            totalTitle.Margin = new Thickness((left - totalTitle.Width), (optionsCorner.Top + quantityAmount.Height + 5), 0, 0);
            totalLabel.Margin = new Thickness(left, (optionsCorner.Top + quantityAmount.Height + 5), 0, 0);
            totalLabel.Text = baseCost.ToString("0.00");

            optionsCorner.Top = totalLabel.Margin.Top + optionHeight;

            addButton.Margin = new Thickness((optionsCorner.Left - addButton.ActualWidth), (optionsCorner.Top), 0, 0);
            cancelButton.Margin = new Thickness(0, (optionsCorner.Top), 0, 0);

            this.Width = optionsCorner.Left;
            this.Height = optionsCorner.Top + addButton.ActualHeight;

            this.Margin = new Thickness(((menu.Width / 2) - (this.Width / 2)),
                                                ((menu.Height / 2) - (this.Height / 2)),
                                                0,
                                                0);
        }

        private void Reset()
        {
            this.Visibility = Visibility.Hidden;
            optionsCorner = new Thickness(defaultCorner.Left, defaultCorner.Top, 0, 0);
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

        public void EditOptionsMenu(MenuItem item, int[] chosenOptions)
        {
            this.item = item;
            oldOrder = chosenOptions;
            this.NewOptionsMenu();
            for (int i = 0; i < 5; i++)
            {
                if ((chosenOptions[i] >= 0) && (chosenOptions[i] < 5))
                {
                    rbs[i, chosenOptions[i]].IsChecked = true;
                }
            }
            editingOldOrder = true;
            this.UpdateTotal(null, null);
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
            this.optionsCost = 0.0m;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((rbs[i, j].IsChecked == true) && (rbs[i, j].Visibility.Equals(Visibility.Visible)))
                    {
                        this.optionsCost += optionPrices[i, j];
                    }
                }
            }
            Int32 quantity = Convert.ToInt32(quantityAmount.Text);
            decimal totalCost = (this.baseCost + this.optionsCost) * quantity;
            totalLabel.Text = totalCost.ToString("0.00");
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
                    if ((rbs[i, j].IsChecked == true) && (rbs[i, j].IsVisible))
                    {
                        options[i] = j;
                    }
                }
            }

            try
            {
                OrderItem orderItem = new OrderItem(item, this.optionsCost, options, this);
                this.orderTable.Add(orderItem);
                menu.menuItemListView.SelectedItem = null;
                this.Reset();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
            editingOldOrder = false;
            menu.toggleOptions(true);
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            if (editingOldOrder)
            {
                try
                {
                    // MenuItem menuItem = (MenuItem)this.menuItemListView.SelectedItem;
                    OrderItem orderItem = new OrderItem(item, this.optionsCost, oldOrder, this);
                    this.orderTable.Add(orderItem);
                    this.Reset();
                }
                catch (Exception error)
                {
                    System.Diagnostics.Debug.WriteLine(error);
                }
            }
            else
            {
                menu.menuItemListView.SelectedItem = null;
                this.Reset();
            }
            menu.toggleOptions(true);
        }

    }
}
