using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CPSC_481
{
    public partial class HeaderImageGetter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            MenuObject.Type type = (MenuObject.Type)value;

            switch (type)
            {
                case MenuObject.Type.Main:
                    return "Images/menu/mains_header.png";
                case MenuObject.Type.Drink:
                    return "Images/menu/drinks_header.png";
                case MenuObject.Type.Side:
                    return "Images/menu/sides_header.png";
                case MenuObject.Type.Dessert:
                    return "Images/menu/desserts_header.png";
            }

            return "Images/menu/drink_header.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            // Dont really care...
            return value;
        }
    }
}
