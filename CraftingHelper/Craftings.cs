using System;
using CraftingHelper.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingHelper
{
    public class Craftings
    {
        public void ask() {
            Item redstone_dust = new Item { Name = "redstone_dust" };
            Item wood = new Item { Name = "x_log" };
            Item planks = new Item { Name = "x_planks" };
            Item stone = new Item { Name = "stone" };
            Item stick = new Item { Name = "stick" };
            Item redstone_torch = new Item { Name = "redstone_torch" };
            Item repeater = new Item { Name = "redstone_repeater" };
            Item nether_quartz = new Item { Name = "nether_quartz" };
            Item redstone_comparator = new Item { Name = "redstone_comparator" };
            Item iron = new Item { Name = "iron_ingot" };
            Item chest = new Item { Name = "chest" };
            Item hopper = new Item { Name = "hopper" };
            Item cobblestone = new Item { Name = "cobblestone" };
            Item furnace = new Item { Name = "furnace" };
            Item coal = new Item { Name = "x_coal" };
            CraftingRecipe Stone = new CraftingRecipe
            {
                Input = new List<ItemStack> {
                new ItemStack{Item= cobblestone, Quantity= 8},
                new ItemStack{ Item= coal, Quantity = 1}
                },
                Output = new ItemStack { Item = stone, Quantity = 8 }
            };
            CraftingRecipe Chest = new CraftingRecipe
            {
                Input = new List<ItemStack> {
                new ItemStack{Item= planks, Quantity= 8}
                },
                Output = new ItemStack {Item = chest,Quantity =1}
            };

            CraftingRecipe Furnace = new CraftingRecipe
            {
                Input = new List<ItemStack> {
                new ItemStack{Item= cobblestone, Quantity= 8}
                },
                Output = new ItemStack { Item = furnace, Quantity = 1 }
            };
            CraftingRecipe Repeater = new CraftingRecipe
            {
                Input = new List<ItemStack> {
                new ItemStack { Item = stone,Quantity=3 },
                new ItemStack { Item = redstone_torch, Quantity = 2 },
                new ItemStack { Item= redstone_dust , Quantity =1 } },
                Output = new ItemStack { Item = repeater, Quantity = 1 }
            };
            CraftingRecipe Comparator = new CraftingRecipe
            {
                Input = new List<ItemStack> {
                new ItemStack { Item = stone,Quantity=3 },
                new ItemStack{ Item = nether_quartz , Quantity = 1 },
                new ItemStack { Item = redstone_torch, Quantity = 3 },
                new ItemStack { Item= redstone_dust , Quantity =1 } },
                Output = new ItemStack { Item = repeater, Quantity = 1 }
            };
            CraftingRecipe RedstoneTorch = new CraftingRecipe
            {
                Input = new List<ItemStack> {
                new ItemStack { Item = stick,Quantity=1 },
                new ItemStack { Item= redstone_dust , Quantity =1 } },
                Output = new ItemStack { Item = redstone_torch, Quantity = 1 }
            };



        }

    }
}
