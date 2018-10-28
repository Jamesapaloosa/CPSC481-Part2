using System;

namespace CPSC_481
{
    public class OrderItem
    {
        public MenuItem menuItem;
        public bool isFinalized;

        public OrderItem(MenuItem menuItem)
        {
            this.menuItem = menuItem;
            this.isFinalized = false;
        }
    }
}
