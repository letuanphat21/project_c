namespace Doan.Models
{
    public class Cart
    {
        public Dictionary<int, Item> Items { get; set; }

        public Cart()
        {
            Items = new Dictionary<int, Item>();
        }

        public Item GetItemById(int id)
        {
            Items.TryGetValue(id, out Item item);
            return item;
        }

        public int GetSize()
        {
            return Items.Count;
        }


        public int GetQuantityById(int id)
        {
            var item = GetItemById(id);
            return item != null ? item.Quantity : 0;
        }

        // Thêm Item vào giỏ hàng
        public void AddItem(Item t)
        {
            if (Items.ContainsKey(t.product.Id))
            {
                Items[t.product.Id].Quantity += t.Quantity;
            }
            else
            {
                Items.Add(t.product.Id, t);
            }
        }

        // Xóa Item khỏi giỏ hàng
        public void RemoveItem(int id)
        {
            if (Items.ContainsKey(id))
            {
                Items.Remove(id);
            }
        }
        // Tính tổng tiền
        public double GetTotalMoney()
        {
            double total = 0;
            foreach (var item in Items.Values)
            {
                total += item.Quantity * item.product.Price;
            }
            return total;
        }

        // Kiểm tra giỏ hàng có rỗng không
        public bool IsCartEmpty()
        {
            return Items.Count == 0;
        }
    }
}
