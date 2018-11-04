using System;
using System.Windows.Media;

namespace CPSC_481
{
    public class MenuItem
    {
        public enum Type { Main, Side, Dessert, Drink };
        
        public String name { get; set; }
        public String description { get; set; }
        public decimal price { get; set; }
        public MenuItem.Type type;
        public String imageName { get; set; }

        public MenuItem(String name, String description, decimal price, MenuItem.Type type, String imageName)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.type = type;
            this.imageName = imageName;
        }
    }
}
