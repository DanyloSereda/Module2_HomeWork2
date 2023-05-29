public class Basket
{
    private Store _store;

    public Basket(Store store)
    {
        _store = store;
        BasketItems = new int?[10];
        ItemCount = new int[10];
    }

    public int?[] BasketItems { get; private set; }
    public int[] ItemCount { get; private set; }

    public void SelectAndAddItems()
    {
        // Выбор и добавление товаров в корзину до заполнения
        while (GetTotalItemCount() < 10)
        {
            Console.WriteLine("\nEnter the items you want to add to the basket (for example Bananas 2, Apples 3):");
            string input = Console.ReadLine();

            string[] items = input.Split(',');

            int totalAdded = 0;
            string[] lastItemName = null;
            int lastItemQuantity = 0;

            foreach (string item in items)
            {
                string[] parts = item.Trim().Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out int quantity))
                {
                    int index = GetItemIndex(parts[0]);

                    if (index >= 0 && _store.IsItemAvailable(index) && quantity > 0 && quantity <= _store.GetStockQuantity(index))
                    {
                        int remainingSpace = 10 - totalAdded;

                        if (totalAdded + quantity > 10)
                        {
                            lastItemName = _store.GetStoreItems();
                            lastItemQuantity = remainingSpace;
                            Console.WriteLine($"Added only {lastItemQuantity} {lastItemName[index]} to the basket. Basket is full.");
                            break;
                        }

                        AddItem(index, quantity);
                        _store.SellItem(index, quantity);
                        totalAdded += quantity;
                    }
                    else
                    {
                        Console.WriteLine($"It's out of stock! {item.Trim()}");
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid item format! {item.Trim()}");
                }
            }

            if (lastItemName != null && lastItemQuantity > 0)
            {
                int lastItemIndex = GetItemIndex(lastItemName[lastItemName.Length - 1]);
                AddItem(lastItemIndex, lastItemQuantity);
                _store.SellItem(lastItemIndex, lastItemQuantity);
            }

            if (GetTotalItemCount() >= 10)
            {
                Console.WriteLine("\nBasket is full. Order is complete.");
                break;
            }

            Console.WriteLine("\nDo you want to add more items? (yes/no)");
            string response = Console.ReadLine();

            if (response.ToLower() != "yes")
            {
                Console.WriteLine("\nOrder is complete.");
                break;
            }
        }
    }

    public void AddItem(int index, int quantity)
    {
        int itemCount = GetTotalItemCount();

        // Добавление товара в корзину с учетом доступного места
        if (BasketItems[index] == null)
        {
            if (quantity + itemCount > 10)
            {
                quantity = 10 - itemCount;
            }

            BasketItems[index] = index; // Добавление индекса товара в корзину
            ItemCount[index] = quantity; // Установка количества товара
        }
        else
        {
            int availableQuantity = 10 - itemCount;

            if (quantity > availableQuantity)
            {
                quantity = availableQuantity;
            }

            ItemCount[index] += quantity; // Увеличение количества товара в корзине
        }

        Console.WriteLine($"Added: {quantity} {_store.GetStoreItems()[index]} to the basket.");
    }

    public void DisplayBasketItems()
    {
        Console.WriteLine("\nItems in the basket (Total ammount 10):\n");

        bool isEmpty = true;

        // Вывод выбранных товаров и их количества
        for (int i = 0; i < BasketItems.Length; i++)
        {
            if (BasketItems[i] != null && ItemCount[i] > 0)
            {
                Console.WriteLine($"{_store.GetStoreItems()[i]} - quantity: {ItemCount[i]}");
                isEmpty = false;
            }
        }

        if (isEmpty)
        {
            Console.WriteLine("Basket is empty.");
        }
    }

    private int GetTotalItemCount()
    {
        int totalCount = 0;

        // Получение общего количества товаров в корзине
        for (int i = 0; i < ItemCount.Length; i++)
        {
            totalCount += ItemCount[i];
        }

        return totalCount;
    }

    private int GetItemIndex(string itemName)
    {
        string[] storeItems = _store.GetStoreItems();

        // Получение индекса товара по его названию
        for (int i = 0; i < storeItems.Length; i++)
        {
            if (storeItems[i] == itemName)
            {
                return i;
            }
        }

        return -1;
    }
}