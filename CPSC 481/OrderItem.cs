using System;

namespace CPSC_481
{
    public class OrderItem
    {
        public MenuItem menuItem { get; private set; }
        public int[] options;
        public bool isFinalized;
        public decimal totalPrice
        {
            get
            {
                // TODO: add options prices
                return this.menuItem.price;
            }
        }





        public OrderItem(MenuItem menuItem, int[] options)
        {
            this.menuItem = menuItem;
            this.options = options;
            this.isFinalized = false;
        }
    }
}
