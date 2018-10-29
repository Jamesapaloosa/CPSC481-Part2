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

namespace HCImenuOptions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RadioButton[,] rbs = new RadioButton[5, 5];
        TextBlock[] optionLabels = new TextBlock[5];
        string[] oLabels = new string[5];
        string[,] oOptions = new string[5, 5];
        double[,] oPrices = new double[5, 5];
        double baseCost = 0.00;

        Thickness defaultCorner = new Thickness(0, 0, 0, 0);
        Thickness optionsCorner = new Thickness(0, 0, 0, 0);

        int columnGap = 20;
        int optionHeight = 17;


        public MainWindow()
        {
            InitializeComponent();
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
            ///*
            itemTitle.Text = "Club Sandwich";
            oLabels[0] = "Choose Bread:";
            oLabels[1] = "Choose Side:";
            oLabels[2] = "Choose Dessert:";
            oOptions[0, 0] = "White";
            oOptions[0, 1] = "Brown";
            oOptions[0, 2] = "Rye";
            oOptions[1, 0] = "Fries";
            oOptions[1, 1] = "Salad";
            oPrices[1, 1] = 1.50;
            oOptions[2, 0] = "Ice Cream";
            oOptions[2, 1] = "Brownie";
            oPrices[2, 1] = 2.00;
            baseCost = 7.00;
            //*/ 
            /*
            itemTitle.Text = "Orange Juice";
            oLabels[0] = "Size:";
            oOptions[0, 0] = "Small";
            oPrices[0, 0] = 1.0;
            oOptions[0, 1] = "Medium";
            oPrices[0, 1] = 1.5;
            oOptions[0, 2] = "Large";
            oPrices[0, 2] = 2.50;
            baseCost = 0;
            //*/
            for (int i = 0; i < rbs.GetLength(0); i++) { 
                string temp = "Option" + i.ToString();
                for (int j = 0; j < rbs.GetLength(1); j++) {
                    rbs[i, j].GroupName = temp;
                }
            }
            defaultCorner = new Thickness((selection1Label.Margin.Left - columnGap), selection1Label.Margin.Top, 0, 0);
            NewMenu();
        }

        private void NewMenu()
        {
            Reset();
            for (int i = 0; ((i < rbs.GetLength(0)) && !(String.IsNullOrEmpty(oLabels[i]))); i++)
            {
                rbs[i, 0].IsChecked = true;
                optionLabels[i].Text = oLabels[i];
                optionLabels[i].Visibility = Visibility.Visible;
                optionLabels[i].Margin = new Thickness((optionsCorner.Left + columnGap), defaultCorner.Top, 0, 0);
                optionsCorner.Left = optionLabels[i].Margin.Left + optionLabels[i].Width;
                for (int j = 0; ((j < rbs.GetLength(1)) && !(String.IsNullOrEmpty(oOptions[i, j]))); j++)
                {
                    rbs[i, j].Margin = new Thickness(optionLabels[i].Margin.Left, rbs[i, j].Margin.Top, 0, 0);
                    if (oPrices[i,j] > 0)
                        rbs[i, j].Content = oOptions[i, j] + " (+$" + oPrices[i, j].ToString("0.00") + ")";
                    else
                        rbs[i, j].Content = oOptions[i, j];

                    rbs[i, j].Visibility = Visibility.Visible;

                    if (rbs[i,j].Margin.Top > optionsCorner.Top)
                        optionsCorner.Top = rbs[i,j].Margin.Top + rbs[i, j].Height;
                    if ((rbs[i, j].Margin.Left + rbs[i,j].Width) > optionsCorner.Left)
                        optionsCorner.Left = rbs[i, j].Margin.Left + rbs[i, j].Width;
                }
            }

            this.Width = optionsCorner.Left + columnGap;
            this.Height = (optionsCorner.Top) + 256;

            specReqTitle.Margin = new Thickness(28, (optionsCorner.Top + optionHeight), 0, 0);
            specReqEntry.Margin = new Thickness(10, (optionsCorner.Top + 2 * optionHeight), 0, 0);
            specReqEntry.Width = this.Width - 40;

            double left = (this.Width / 2);
            double top = this.Height - 130;

            quantityLabel.Margin = new Thickness((left - (quantityLabel.Width + quantitySub.Width) - 3), top, 0, 0);
            quantitySub.Margin = new Thickness((left - quantitySub.Width), top, 0, 0);
            quantityAmount.Margin = new Thickness(left, top, 0, 0);
            quantityAdd.Margin = new Thickness((left + quantityAmount.Width), top, 0, 0);

            totalTitle.Margin = new Thickness((left - totalTitle.Width), (top + quantityAmount.Height + 5), 0, 0);
            totalLabel.Margin = new Thickness(left, (top + quantityAmount.Height + 5), 0, 0);


            addButton.Margin = new Thickness((this.Width - 75), (this.Height - 75), 0, 0);
            cancel.Margin = new Thickness(10, (this.Height - 75), 0, 0);

            totalLabel.Text = baseCost.ToString("0.00");
        }

        private void Reset()
        {
            optionsCorner = defaultCorner;
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
        }
    }
}
