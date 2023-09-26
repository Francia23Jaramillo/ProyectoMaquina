using ProyectoMaquina.control;
using ProyectoMaquina.model;
using System;

namespace ProyectoMaquina.view
{
    internal class View
    {
        static void Main(string[] args)
        {
            Controller controller = Controller.GetInstance();

            Console.WriteLine("Bienvenido a la máquina expendedora");

            while (true)
            {
                string inputCliente = GetUserInput("Escoja tipo de cliente: [C] - Cliente o [P] - Proveedor");
                if (inputCliente == "C")
                {
                    ProcessCustomer(controller);
                }
                else if (inputCliente == "P")
                {
                    ProcessOwner(controller);
                }
                else
                {
                    Console.WriteLine("Opción no válida. Por favor, elija [C] o [P].");
                }

                if (ShouldExit())
                {
                    break;
                }
            }
        }

        static void ProcessCustomer(Controller controller)
        {
            Console.WriteLine("Lista de productos:");
            Console.WriteLine(controller.DisplayProductList());

            string productName;
            do
            {
                productName = GetUserInput("Escoja un producto de la lista");
                if (!controller.ProductExists(productName) || !controller.ProductHasInventory(productName))
                {
                    Console.WriteLine("Producto no válido o sin inventario.");
                }
            } while (!controller.ProductExists(productName) || !controller.ProductHasInventory(productName));

            IProduct selectedProduct = controller.GetProductByName(productName);

            int totalPrice = selectedProduct.Price;
            int moneyInserted = CollectMoney(totalPrice);

            if (moneyInserted >= totalPrice)
            {
                Console.WriteLine($"Producto comprado con éxito.");
              Console.WriteLine($"Su cambio puede ser retornarnado en billetes o monedas de 500, 200, 100 y 50");
                Console.WriteLine($"Su Cambio es de : {moneyInserted - totalPrice}");
                controller.UpdateProductQuantity(productName); // Actualizar la cantidad del producto
            }
            else
            {
                Console.WriteLine("El dinero ingresado no es suficiente para comprar el producto.");
            }
        }

        static int CollectMoney(int targetAmount)
        {
            int totalMoney = 0;
            while (totalMoney < targetAmount)
            {
                int money;
                if (int.TryParse(GetUserInput("Ingrese el billete:"), out money))
                {
                    totalMoney += money;
                    Console.WriteLine($"Dinero total ingresado: {totalMoney}");
                }
                else
                {
                    Console.WriteLine("Por favor ingrese un número válido.");
                }
            }
            return totalMoney;
        }

        static void ProcessOwner(Controller controller)
        {
            string productName = GetUserInput("Nombre del producto:");
            int productPrice = int.Parse(GetUserInput("Valor del producto:"));
            int productQuantity = int.Parse(GetUserInput("Cantidad:"));

            Consumable newProduct = new Consumable(productName, productPrice, productQuantity);
            controller.AddProduct(newProduct);

            Console.WriteLine("Producto registrado con éxito.");
        }

        static string GetUserInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        static bool ShouldExit()
        {
            string input = GetUserInput("¿Desea salir? (Sí/No)");
            return input.Equals("Sí", StringComparison.OrdinalIgnoreCase);
        }
    }
}
