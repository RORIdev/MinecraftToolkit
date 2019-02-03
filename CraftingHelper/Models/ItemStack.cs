using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingHelper.Models
{
    public class ItemStack
    {
        public  int Quantity { get; set; }
        public Item Item { get; set; }  
    }
}
