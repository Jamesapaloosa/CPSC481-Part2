using System;

namespace CPSC_481
{
    public class OrderItem
    {
        public OptionsMenu sourceMenu { get; private set; }
        public MenuObject menuItem { get; private set; }
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

        public int quantity;

        public OrderItem(MenuObject menuItem, decimal optionsCost, int[] options, OptionsMenu sourceOptionsMenu)
        {
            this.menuItem = menuItem;
            this.optionsCost = optionsCost;
            this.options = options;
            this.isFinalized = false;
            this.sourceMenu = sourceOptionsMenu;
            this.quantity = Int32.Parse(this.menuItem.optionsMenu.quantityAmount.Text);
        }
    }
}
