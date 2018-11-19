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
        public MenuItem.Type type { get; set; }
        public String imageName { get; set; }
        public string[,,] options = new string[5, 6, 2];

        public MenuItem(String name, String description, decimal price, MenuItem.Type type, String imageName)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.type = type;
            this.imageName = imageName;
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
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}
