public class Order
{
    private Basket _basket;
    private Store _store;
    private int _orderNumber;

    public Order(Basket basket, Store store)
    {
        _basket = basket;
        _store = store;
        _orderNumber = GenerateOrderNumber();
    }

    public void DisplayTotalPrice()
    {
        int totalPrice = 0;

        // Вычисление общей стоимости заказа на основе выбранных товаров
        for (int i = 0; i < _basket.BasketItems.Length; i++)
        {
            if (_basket.BasketItems[i] != null && _basket.ItemCount[i] > 0)
            {
                int index = _basket.BasketItems[i].Value;
                int quantity = _basket.ItemCount[i];
                int price = _store.GetPriceForItem(index);

                int itemTotalPrice = quantity * price;
                totalPrice += itemTotalPrice;
            }
        }

        Console.WriteLine($"\nOrder number: {_orderNumber}");
        Console.WriteLine($"Total price: {totalPrice} UAH");
    }

    private int GenerateOrderNumber()
    {
        Random random = new Random();
        return random.Next(1000, 9999);
    }
}
