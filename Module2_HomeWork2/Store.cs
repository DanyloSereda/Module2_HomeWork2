using System;

public class Store
{
    private string[] _storeItems;
    private int[] _inStock;
    private int[] _prices;

    public Store()
    {
        _storeItems = new string[]
        {
            "Bananas",
            "Apples",
            "Oranges",
            "Penuts",
            "Pineapples",
            "Strawberry",
            "Plums",
            "Milk",
            "Chocolate",
            "Icecream"
        };

        _inStock = new int[10];
        _prices = new int[10];

        Random random = new Random();

        // Заполнение массивов случайными значениями
        for (int i = 0; i < _inStock.Length; i++)
        {
            _inStock[i] = random.Next(0, 20);
            _prices[i] = random.Next(1, 100);
        }
    }

    // Проверка, доступен ли товар с заданным индексом
    public bool IsItemAvailable(int index)
    {
        return _inStock[index] > 0;
    }

    // Вычитание проданного количества товара из общего количества
    public void SellItem(int index, int quantity)
    {
        if (_inStock[index] >= quantity)
        {
            _inStock[index] -= quantity;
        }
    }

    // Возвращает массив названий товаров в магазине
    public string[] GetStoreItems()
    {
        return _storeItems;
    }

    // Возвращает количество товара с заданным индексом в наличии
    public int GetStockQuantity(int index)
    {
        return _inStock[index];
    }

    // Возвращает цену товара с заданным индексом
    public int GetPriceForItem(int index)
    {
        return _prices[index];
    }

    public void DisplayStock()
    {
        Console.WriteLine("Currently in stock:\n");
        bool allInStock = true;

        // Вывод товаров, которые есть в наличии, с их количеством и ценой
        for (int i = 0; i < _inStock.Length; i++)
        {
            int amount = _inStock[i];
            int price = _prices[i];
            if (amount > 0)
            {
                Console.WriteLine($"{_storeItems[i]} - quantity: {amount} price: {price} UAH");
                allInStock = false;
            }
        }

        if (allInStock)
        {
            Console.WriteLine("Everything in stock.");
        }

        Console.WriteLine("\nOut of stock:\n");
        bool outOfStockExists = false;

        // Вывод товаров, которых нет в наличии
        for (int i = 0; i < _inStock.Length; i++)
        {
            int amount = _inStock[i];
            if (amount == 0)
            {
                Console.WriteLine($"{_storeItems[i]} - quantity: {amount} price: {0} UAH");
                outOfStockExists = true;
            }
        }

        if (!outOfStockExists)
        {
            Console.WriteLine("No items are out of stock.");
        }
    }
}