using ProyectoMaquina.model;
using System.Security.Cryptography.X509Certificates;

namespace ProyectoMaquina.control
{

    public sealed class Controller
    {
        private static Controller _instance;
        public List<IProduct> ListaProductos { get; set; }


        private Controller()
        {
            ListaProductos = new List<IProduct>();

            ListaProductos.Add(new Consumable("Coca Cola", 3200, 5));
            ListaProductos.Add(new Consumable("Snacks", 2500, 10));
            ListaProductos.Add(new Consumable("Chocolatina", 2800, 8));
        }

        public void AgregarProduct(Consumable product)    
        
        {
            ListaProductos.Add(product);
        }
        public static Controller GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Controller();
            }
            return _instance;
        }
        public string DisplayProductList()
        {
            string value = "";

            foreach (IProduct product in ListaProductos)
            {
                value += product.DisplayProduct() + '\n';
            }
            return value;
        }
        public bool ProductExists(string product_name)
        {
            bool product_exists = false;
            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == product_name)
                {
                    product_exists = true;
                }
            }
            return product_exists;
        }
        public bool ProductHasInventory(string product_name)

        {
            bool has_inventory = false;
            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == product_name && product.Quantity > 0)
                {
                    has_inventory = true;
                }

            }
            return has_inventory;

        }

     
    }
}

