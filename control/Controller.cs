using ProyectoMaquina.model;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace ProyectoMaquina.control
{
    public sealed class Controller
    {
        private static Controller _instance;
        private readonly ConcurrentDictionary<string, IProduct> _productDictionary;

        private Controller()
        {
            _productDictionary = new ConcurrentDictionary<string, IProduct>();

            AddProduct(new Consumable("Coca Cola", 3200, 2));
            AddProduct(new Consumable("Snacks", 2500, 10));
            AddProduct(new Consumable("Chocolatina", 2800, 8));
        }

        public static Controller GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Controller();
            }
            return _instance;
        }

        public void AddProduct(Consumable product)
        {
            _productDictionary.TryAdd(product.Name, product);
        }

        public string DisplayProductList()
        {
            var productList = new StringBuilder();

            foreach (var product in _productDictionary.Values)
            {
                productList.AppendLine(product.DisplayProduct());
            }

            return productList.ToString();
        }

        public bool ProductExists(string productName)
        {
            return _productDictionary.ContainsKey(productName);
        }

        public bool ProductHasInventory(string productName)
        {
            if (_productDictionary.TryGetValue(productName, out var product))
            {
                return product.Quantity > 0;
            }

            return false;
        }

        public IProduct GetProductByName(string productName)
        {
            _productDictionary.TryGetValue(productName, out var product);
            return product;
        }

        public void UpdateProductQuantity(string productName)
        {
            if (_productDictionary.TryGetValue(productName, out var product))
            {
                // Resta 1 a la cantidad del producto después de una venta
                product.Quantity -= 1;

                // Verificar si la cantidad es menor o igual a cero y eliminar el producto si es necesario
                if (product.Quantity <= 0)
                {
                    Console.WriteLine("Producto no disponible, se excluirá de la lista:");
                    

                    // _productDictionary.TryGetValue(productName, out _);
                    _productDictionary.TryRemove(productName, out _);
                }
            }
        }
    }
}
