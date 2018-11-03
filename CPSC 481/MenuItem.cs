using System;

namespace CPSC_481
{
    public class MenuItem
    {
        public enum Type { Food, Drink };

        public String name;
        public MenuItem.Type type;
        public decimal price;

        public MenuItem(String name, MenuItem.Type type, decimal price)
        {
            this.name = name;
            this.type = type;
            this.price = price;
        }
    }
}
