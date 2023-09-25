using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMaquina.model
{
    public interface IProduct
    {
        string Name { get; set; }
        int Quantity {  get; set; }
        string DisplayProduct();
    }
}
