Store store = new Store();
store.DisplayStock();
Basket basket = new Basket(store);
basket.SelectAndAddItems();
basket.DisplayBasketItems();
Order order = new Order(basket, store);
order.DisplayTotalPrice();