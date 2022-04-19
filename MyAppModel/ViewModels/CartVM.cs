using MyAppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppModels.ViewModels
{
    public class CartVM
    {
        public double Total { get; set; }
        public IEnumerable<Cart> Listofcart { get; set; }
       

    }
}
