namespace Doan.Models
{
    public class Cart
    {
        public List<Item> Items { get; set; }

        public Cart()
        {
            Items = new List<Item>();
        }

        public Item getItemById(int id)
        {
            foreach (Item item in Items)
            {
                if (item.product.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public int getQuantityById(int id)
        {
            return getItemById(id).Quantity;
        }

        public void addItem(Item t)
        {
            if (getItemById(t.product.Id) != null)
            {
                Item m = getItemById(t.product.Id);
                //nếu có rồi thì chỉ cần set lại số lượng
                m.Quantity = m.Quantity + t.Quantity;
            }
            else
            {
                Items.Add(t);
            }
        }

        public void removeItem(int id)
        {
            if (getItemById(id) != null)
            {
                Items.Remove(getItemById(id));
            }


        }

        public double getTotalMoney()
        {
            double t = 0;
            foreach (Item item in Items)
            {
                t += (item.Quantity * item.Price);


            }
            return t;
        }
        public bool checkCartNoItems()
        {
            if (Items.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
