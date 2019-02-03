using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingHelper.Models
{
    public class CraftingRecipe
    {
        public List<ItemStack> Input { get; set; }
        public ItemStack Output { get; set; }
    }
}
