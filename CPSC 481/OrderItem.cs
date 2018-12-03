using System;

namespace CPSC_481
{
    public class OrderItem
    {
        public OptionsMenu sourceMenu { get; private set; }
        public MenuItem menuItem { get; private set; }
        public decimal optionsCost;
        public int[] options;
        public bool isFinalized;
        public decimal totalPrice
        {
            get
            {
                // TODO: add options prices
                return this.menuItem.price + this.optionsCost;
            }
        }


        public OrderItem(MenuItem menuItem, decimal optionsCost, int[] options, OptionsMenu sourceOptionsMenu)
        {
            this.menuItem = menuItem;
            this.optionsCost = optionsCost;
            this.options = options;
            this.isFinalized = false;
            this.sourceMenu = sourceOptionsMenu;
        }
    }
}
