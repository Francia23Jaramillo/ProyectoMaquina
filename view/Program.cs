using ProyectoMaquina.control;
using ProyectoMaquina.model;
using System;
using System.Drawing;

namespace ProyectoMaquina.view
{
    internal class View
    {


        static void Main(string[] args)
        {

            Controller controller = Controller.GetInstance();
      
            string texto_bienvenida = "Bienvenido a la maquina expendedora ";
            Console.WriteLine(texto_bienvenida);
            string input_cliente = "";
            string input_pro = "";
            int input_valor;
            int input_cant;
            while (true)
            {




                do
                {
                    Console.WriteLine("escoja tipo de cliente: [C] o [P]");
                    input_cliente = Console.ReadLine();
                } while (input_cliente != "C" && input_cliente != "P");
            

                if (input_cliente == "C")
                {

                    Console.WriteLine("la lista de productos es:"); // TO-DO: Lista de productos
                    Console.WriteLine(controller.DisplayProductList());

                    Console.WriteLine("Escoja un producto de la lista...");
                    bool valid_product = false;
                    do
                    {
                        string input_producto = Console.ReadLine();
                        valid_product = controller.ProductExists(input_producto) && controller.ProductHasInventory(input_producto);
                        if (valid_product == false)
                        {
                            Console.WriteLine("Escoga un producto valido");
                        }
                    } while (!valid_product);


                    Console.WriteLine("Ingrese los billetes para el pago del producto");
                    int suma_billetes = 0;
                    while (true)
                    {
                        Console.WriteLine("Ingrese el billete...");
                        try
                        {
                            suma_billetes += Convert.ToInt32(Console.ReadLine());

                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine($"Por favor ingrese un dato con valor numerico: {e.Message}");
                           
                        }


                        Console.WriteLine("Para dejar de ingresar billetes, escriba [STOP] de lo contrario presione ENTER");
                        string input_cash = Console.ReadLine();
                        if (input_cash == "STOP")
                        {
                            Console.WriteLine($"Billete ingresado y producto comprado con éxito");
                            break;
                        }


                    }
                }
                else if (input_cliente == "P")
                {
                    Console.WriteLine("Nombre producto");
                    input_pro = Console.ReadLine();
                    Console.WriteLine("Valor del producto");

                    input_valor = Convert.ToInt32(Console.ReadLine());
                    
                    Console.WriteLine("Cantidad");
                    input_cant = Convert.ToInt32(Console.ReadLine());

                    Consumable nuevoProducto = new Consumable(input_pro, input_valor, input_cant);



                    controller.AgregarProduct(nuevoProducto);

                    Console.WriteLine("Producto registrado con éxito.");
                }


            }
        }
    }
}