using CraftingHelper.Models;
using Newtonsoft.Json;
using RecipeConverter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeConverter
{
    class Program
    {

        static void Main(string[] args)
        {
            string path = @"C:\Users\cliente\AppData\Roaming\.minecraft\versions\1.13\recipes";
            List<MojangShapedModel> RegisteredMojangShaped = new List<MojangShapedModel>();
            List<MojangShapelessModel> RegisteredMojangShapeless = new List<MojangShapelessModel>();
            List<MojangItem> RegisteredItems = new List<MojangItem>();
            List<Item> UnElementizedItems = new List<Item>();
            List<Item> FinalItemList = new List<Item>();
            List<CraftingRecipe> UnelementizedCraftings = new List<CraftingRecipe>();
            List<CraftingRecipe> FinalCraftList = new List<CraftingRecipe>();
            DirectoryInfo dir = new DirectoryInfo(path);
            List<FileInfo> files = dir.GetFiles("*.json").ToList();
            List<string> exceptionFiles = new List<string>();
            foreach (FileInfo file in files) {
                string fileR = file.OpenText().ReadToEnd();
                if (fileR.Contains("shapeless"))
                {
                    try
                    {
                        var Model = JsonConvert.DeserializeObject<MojangShapelessModel>(file.OpenText().ReadToEnd());
                        RegisteredMojangShapeless.Add(Model);
                        Console.WriteLine($"Registered {Model.result.item} @ Shapeless");
                        foreach (var ingredient in Model.ingredients)
                        {
                            var push = new MojangItem { item = ingredient.item, count = 0 };
                            if (RegisteredItems.Find(x => x.item == push.item) == null)
                            {

                                RegisteredItems.Add(push);
                                Console.WriteLine($"Item Registry | Added  {ingredient.item}");
                            }

                        }
                        var result = new MojangItem { item = Model.result.item, count = 0 };
                        if (RegisteredItems.Find(x => x.item == result.item) == null)
                        {
                            RegisteredItems.Add(result);
                            Console.WriteLine($"Item Registry | Added  {Model.result.item}");
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptionFiles.Add(file.FullName);
                        //Console.WriteLine($"EX : {ex} \n\n@ {file.FullName} || Shapeless");
                        //break;
                    }


                }
                else if (fileR.Contains("shaped"))
                {
                    try
                    {
                        var Model = JsonConvert.DeserializeObject<MojangShapedModel>(file.OpenText().ReadToEnd());
                        RegisteredMojangShaped.Add(Model);
                        Console.WriteLine($"Registered {Model.result.item} @ Shaped");
                        foreach (var iFrame in Model.keys.Values)
                        {
                            foreach (var it in iFrame.Values)
                            {
                                MojangItem ingredient = new MojangItem { item = it, count = 0 };
                                if (RegisteredItems.Find(x => x.item == ingredient.item) == null)
                                {
                                    RegisteredItems.Add(ingredient);
                                    Console.WriteLine($"Item Registry | Added  {ingredient.item}");
                                }
                            }


                        }
                        var result = new MojangItem { item = Model.result.item, count = 0 };
                        if (RegisteredItems.Find(x => x.item == result.item) == null)
                        {
                            RegisteredItems.Add(result);
                            Console.WriteLine($"Item Registry | Added  {Model.result.item}");
                        }

                    }
                    catch (Exception ex)
                    {
                        exceptionFiles.Add(file.FullName);
                        //Console.WriteLine($"EX : {ex} \n\n@ {file.FullName} || Shaped");
                        //break;

                    }
                }
                else {
                    
                }
            }

            Console.WriteLine($"Initializing Transference [MojangItem -> Item]");
            foreach (MojangItem i in RegisteredItems) {
                if (i.item != null) {
                    Item push = new Item { Name = i.item.Split(':')[1] };
                    UnElementizedItems.Add(push);
                    Console.WriteLine($"[MojangItem -> Item ] {push.Name}");
                }

            }
            
            foreach (var mojangCraft in RegisteredMojangShaped) {
                var craft = fromMojangShaped(mojangCraft, UnElementizedItems);
                UnelementizedCraftings.Add(craft);
                Console.WriteLine($"Recipe for {craft.Output.Item.Name} [x {craft.Output.Quantity}] Added. @ Shaped");
            }
            

            foreach (var mojangCraft in RegisteredMojangShapeless) {
                var craft = fromMojangShapeless(mojangCraft,UnElementizedItems);
                UnelementizedCraftings.Add(craft);
                Console.WriteLine($"Recipe for {craft.Output.Item.Name} [x {craft.Output.Quantity}] Added. @ Shapeless");

            }
           
            Console.WriteLine($"Elementirization of all the items. Could take some time.");

            foreach (Item item in UnElementizedItems) {
                var allCraft = UnelementizedCraftings.FindAll(x => x.Output.Item == item);
                if (allCraft.Count == 0)
                {
                    

                    item.Elemental = true;
                    if (!FinalItemList.Contains(item)) {
                        FinalItemList.Add(item);
                    }


                }
                else {
                    foreach (var cRecipe in allCraft)
                    {
                        //there are craftings that origins from a orename_block [e.g. iron_block]
                        //such item would create a loop on the recursion [ingot -> block -> ingot ...] and should be discarted.
                        //need to get itemlist real quick.
                        //god fucking sake. recursion wont help because of the loop.
                        //this is as best as i can get it. the problem is that, as an mc player i could easely hardcode every single elemental item.
                        //but mojang.
                        if (item.Name != null) {
                            if (item.Name.Contains("_ingot"))
                            {
                                string polishedItemName = $"{item.Name.Split('_')[0]}";
                                if (item.Name == "emerald" || item.Name == "diamond" || item.Name == "redstone")
                                {
                                    polishedItemName = item.Name;
                                }
                                if (item.Name == "lapis_lazuli")
                                {
                                    polishedItemName = "lapis";
                                }

                                if (!String.IsNullOrWhiteSpace(polishedItemName))
                                {

                                    item.Elemental = true;
                                    if (!FinalItemList.Contains(item))
                                    {
                                        
                                        FinalItemList.Add(item);
                                    }
                                }
                                else
                                {
                                    if (item.Name != null)

                                    {
                                        item.Elemental = false;
                                        if (!FinalItemList.Contains(item))
                                        {
                                            FinalItemList.Add(item);
                                        }
                                    }


                                }

                            }
                            else if (item.Name.Contains("_block"))
                            {
                                

                                //take an example as hay_block.
                                //9x wheat -> 1x hay_block
                                List<string> allItemNames = item.Name.Split('_').ToList();
                                allItemNames.Remove(allItemNames.Last());


                                bool craftable = false;
                                foreach (var craft in allCraft)
                                {
                                    foreach (ItemStack i in craft.Input)
                                    {
                                        if (allCraft.FindAll(x => x.Output.Item.Name == i.Item.Name).Count != 0)
                                        { //inner crafting search.
                                            var innerCraft = allCraft.FindAll(x => x.Output.Item.Name == i.Item.Name);
                                            foreach (var inner in innerCraft)
                                            {
                                                if (inner.Input.Any(x => x.Item.Elemental == true))
                                                {
                                                    craftable = true;
                                                    break;
                                                }
                                            }

                                        }
                                    }
                                    if (craft.Input.Any(x => x.Item.Elemental == true))
                                    {
                                        craftable = true;
                                        break;
                                    }

                                }

                                if (craftable)
                                {
                                    var nItem = new Item();
                                    nItem.Elemental = false;
                                    nItem.Name = item.Name;
                                    if (!FinalItemList.Contains(nItem) && item.Name != null)
                                    {
                                        FinalItemList.Add(nItem);
                                    }


                                }
                                else
                                {
                                    var nItem = new Item();
                                    nItem.Elemental = true;
                                    nItem.Name = item.Name;
                                    if (!FinalItemList.Contains(nItem) && item.Name != null)
                                    {
                                       
                                        FinalItemList.Add(nItem);
                                    }
                                }
                                
                            }
                            else {
                                bool craftable = false;
                                foreach (var craft in allCraft)
                                {
                                    foreach (ItemStack i in craft.Input)
                                    {
                                        if (allCraft.FindAll(x => x.Output.Item.Name == i.Item.Name).Count != 0)
                                        { //inner crafting search.
                                            var innerCraft = allCraft.FindAll(x => x.Output.Item.Name == i.Item.Name);
                                            foreach (var inner in innerCraft)
                                            {
                                                if (inner.Input.Any(x => x.Item.Elemental == true))
                                                {
                                                    craftable = true;
                                                    break;
                                                }
                                            }

                                        }
                                    }
                                    if (craft.Input.Any(x => x.Item.Elemental == true))
                                    {
                                        craftable = true;
                                        break;
                                    }

                                }

                                if (craftable)
                                {
                                    var nItem = new Item();
                                    nItem.Elemental = false;
                                    nItem.Name = item.Name;
                                    if (!FinalItemList.Contains(nItem) && item.Name != null)
                                    {
                                        FinalItemList.Add(nItem);
                                    }


                                }
                                else
                                {
                                    var nItem = new Item();
                                    nItem.Elemental = true;
                                    nItem.Name = item.Name;
                                    if (!FinalItemList.Contains(nItem) && item.Name != null)
                                    {
                                       
                                        FinalItemList.Add(nItem);
                                    }
                                }
                            }

                        }

                    }
                }


            }

            ///////////////////////////////////////////////////////////////////
            ////fakeElemental Detection
            ///////////////////////////////////////////////////////////////////
            //Common Vars > FinalItemList = All items added. , checkedItems = items passed throught this filter.
            
            List<Item> checkedItems = new List<Item>(); //I'll have to walk trought [slime_ball]
            string MasterNameFromFullName(List<string> fullName)
            {
                string innerMasterName = "";
                List<string> innerName = fullName;
                innerName.Remove(innerName.Last());
                innerName.ForEach(x => innerMasterName += $"{x}_");
                innerMasterName = innerMasterName.Remove(innerMasterName.Count() - 1);
                return innerMasterName;
            }
            foreach (Item itemToBeChecked in FinalItemList.Distinct())
            {
                Item savedItem = new Item();
                int QuantityOfCrafts = UnelementizedCraftings.FindAll(x => x.Output.Item.Name == itemToBeChecked.Name).Count;
                if (QuantityOfCrafts == 1)
                {
                    List<CraftingRecipe> craftingRecipes = UnelementizedCraftings.FindAll(x => x.Output.Item.Name == itemToBeChecked.Name);
                    //non-explicit outer IbI checking.
                    foreach (CraftingRecipe innerCraftings in craftingRecipes)
                    {
                        foreach (ItemStack innerItem in innerCraftings.Input)
                        {
                            List<CraftingRecipe> innerCraft = UnelementizedCraftings.FindAll(x => x.Output.Item.Name == innerItem.Item.Name);
                            
                            
                            foreach (CraftingRecipe recipe in innerCraft)
                            {
                                if (itemToBeChecked.Name.Contains("_block"))
                                {
                                    string innerMasterName = MasterNameFromFullName(itemToBeChecked.Name.Split('_').ToList());
                                    if (recipe.Input.All(x => x.Item.Name == itemToBeChecked.Name))
                                    {

                                        //thats an item_IBI 90%
                                        Console.WriteLine($"\n ====[Depkg Data]====\n\n" +
                                            $"itemToBeChecked = {itemToBeChecked.Name}\n" +
                                            $"recipe.Output.Item.Name = {recipe.Output.Item.Name}\n" +
                                            $"checkedItems.Exists(i) = {checkedItems.Exists(x => x.Name == itemToBeChecked.Name)}\n" +
                                            $"checkedItems.Exists(r) = {checkedItems.Exists(x => x.Name == recipe.Output.Item.Name)}\n" +
                                            $"====[END OF Depkg]====\n\n");
                                        savedItem.Name = innerItem.Item.Name;
                                        savedItem.Elemental = true;
                                        savedItem.Updated = false;
                                        
                                        if (!checkedItems.Exists(x => x.Name == innerItem.Item.Name))
                                        {
                                            Console.WriteLine($"[Elemental] Item \t|{savedItem.Name}| \tpassed on elemental checking <[Inner]item_IBI>");
                                            checkedItems.Add(savedItem);
                                        }
                                        else
                                        {
                                            savedItem.Name = innerItem.Item.Name;
                                            savedItem.Updated = true;
                                            checkedItems.Remove(checkedItems.Find(x => x.Name == innerItem.Item.Name));
                                            Console.WriteLine($"[Elemental-Update] Item \t|{savedItem.Name}| \tpassed on elemental checking <[Inner]item_IBI> [Updated]");
                                            checkedItems.Add(savedItem);

                                        }
                                                                               

                                        
                                    }
                                    else if (recipe.Output.Item.Name.Contains("_slab"))
                                    {
                                        savedItem.Name = innerItem.Item.Name;
                                        savedItem.Elemental = false;
                                        if (checkedItems.Find(x => x.Name == savedItem.Name) == null)
                                        {
                                            checkedItems.Add(savedItem);
                                        }
                                    }
                                }
                               
                            }
                            
                        }

                    }
                    //TODO : filter for 1 craft only. [E.g. Acacia Door, Armor, building blocks.]
                    savedItem.Elemental = false;
                    savedItem.Name = itemToBeChecked.Name;
                    if (checkedItems.Find(x => x.Name == savedItem.Name) == null)
                    {
                        checkedItems.Add(savedItem);
                    }
                }
                else if (QuantityOfCrafts == 2)
                {
                    //TODO : here ibi loops may happen. [E.g. iron_ingot -> iron_block / slime_ball -> slime_block ]

                        List<CraftingRecipe> craftingRecipes = UnelementizedCraftings.FindAll(x => x.Output.Item.Name == itemToBeChecked.Name);
                        List<string> separatedItemNames = itemToBeChecked.Name.Split('_').ToList();
                        
                        string masterName = (separatedItemNames.Count > 1 ? MasterNameFromFullName(separatedItemNames) : separatedItemNames[0]);
                    //TODO : CHECK FOR IBI LOOP
                    if (craftingRecipes.Any(y => y.Input.All(x => x.Item.Name.Contains(masterName))) && itemToBeChecked.Name.Contains("_block")) // expects 9xiron_ingot -> iron_block [E.g.]
                    {
                        //thats an iBi inner item.
                        savedItem.Name = itemToBeChecked.Name;
                        savedItem.Elemental = false;
                        if (checkedItems.Find(x => x.Name == savedItem.Name) == null)
                        {
                            checkedItems.Add(savedItem);
                        }
                    }
                    else if (craftingRecipes.Any(y => y.Input.All(x => x.Item.Name.Contains(masterName + "_block"))))
                    {
                        //this is an outer IbI item (gems/ores)
                        savedItem.Name = itemToBeChecked.Name;
                        savedItem.Elemental = true;
                        if (checkedItems.Find(x => x.Name == savedItem.Name) == null)
                        {
                            Console.WriteLine($"[Elemental] Item \t|{itemToBeChecked.Name}| \tpassed on elemental checking <item_IBI>");
                            checkedItems.Add(savedItem);
                        }
                    }
                    else
                    {
                        savedItem.Elemental = false;
                        savedItem.Name = itemToBeChecked.Name;
                        if (checkedItems.Find(x => x.Name == savedItem.Name) == null)
                        {
                            checkedItems.Add(savedItem);
                        }
                    }
                   

                    
                }
                else if (QuantityOfCrafts == 0)
                {
                    
                    savedItem.Elemental = true;
                    savedItem.Name = itemToBeChecked.Name;
                    if (checkedItems.Find(x => x.Name == savedItem.Name) == null)
                    {
                        Console.WriteLine($"[Elemental] Item \t|{itemToBeChecked.Name}|\t \tpassed on elemental checking <0craft>");
                        checkedItems.Add(savedItem);
                    }

                }
                else
                {
                    
                    savedItem.Elemental = false;
                    savedItem.Name = itemToBeChecked.Name;
                    if (checkedItems.Find(x => x.Name == savedItem.Name) == null)
                    {
                        Console.WriteLine($"[RARE <{QuantityOfCrafts}x>] Craftings found for Item {itemToBeChecked.Name} setting to non-elemental");
                        checkedItems.Add(savedItem);
                    }
                }
            }


            if (checkedItems.Count == FinalItemList.Count)
            {
                Console.WriteLine($"Item Fix is done. [FIL <- CI] ");
                FinalItemList = checkedItems;
            }
            else {
                Console.WriteLine($"FIL -> {FinalItemList.Count} != CI -> {checkedItems.Count}");
                Console.WriteLine($"Discrepancies Written in diff.json\nCI Written in CI.json");
                var discrepant = FinalItemList;
                foreach (Item item in checkedItems)
                {
                    var Ritem = discrepant.Find(x => x.Name == item.Name);
                    if (Ritem != null)
                    {
                        discrepant.Remove(Ritem);

                    }
                }
                File.WriteAllText(Directory.GetCurrentDirectory() + @"\diff.json", JsonConvert.SerializeObject(discrepant, Formatting.Indented));
                File.WriteAllText(Directory.GetCurrentDirectory() + @"\ci.json", JsonConvert.SerializeObject(checkedItems, Formatting.Indented));

                Console.WriteLine($"Do you want to merge ? [DISC => CI] ");
                var input = Console.ReadKey();
                Console.WriteLine("\n");
                if (input.KeyChar == 'y')
                {
                    
                    foreach (Item uniqueItem in discrepant)
                    {
                        if (!checkedItems.Exists(x => x.Name == uniqueItem.Name))
                        {
                            Console.WriteLine($"[MERGE] Treated discrepancy {uniqueItem.Name} [E : {uniqueItem.Elemental} | U: {uniqueItem.Updated}] as checkedItem and Merged. \n");
                            checkedItems.Add(uniqueItem);
                        }

                    }

                }
                Console.WriteLine($"Do you want to try experimental method ? [FIL = CI] ");
                var inputb = Console.ReadKey();
                Console.WriteLine("\n");
                if(inputb.KeyChar == 'y')
                {
                    FinalItemList = checkedItems.Distinct().ToList();
                }
            }

            ///////////////////////////////////////////////////////////////////
            ///

            foreach (CraftingRecipe recipe in UnelementizedCraftings)
            {
                CraftingRecipe ElementizedCrafting = new CraftingRecipe();
                ElementizedCrafting.Input = new List<ItemStack>();
                ElementizedCrafting.Output = new ItemStack();
                foreach (var itemstack in recipe.Input)
                {
                    var item = FinalItemList.Find(x => x.Name == itemstack.Item.Name);
                    ElementizedCrafting.Input.Add(new ItemStack { Item = item, Quantity = itemstack.Quantity });

                }
                ElementizedCrafting.Output.Item = FinalItemList.Find(x => x.Name == recipe.Output.Item.Name);
                ElementizedCrafting.Output.Quantity = recipe.Output.Quantity;
                FinalCraftList.Add(ElementizedCrafting);
                Console.WriteLine($"[Imouto']\t{ElementizedCrafting.Output.Item.Name} [ADDED] -> FCL");
            }

            File.WriteAllText(Directory.GetCurrentDirectory() + @"\Craft.json", JsonConvert.SerializeObject(FinalCraftList, Formatting.Indented));
            Console.WriteLine($"\n\n Done Converting {(RegisteredMojangShaped.Count + RegisteredMojangShapeless.Count)} Recipes from {files.Count} .json files.\n\n" +
                $"|| Item Registry Results || {RegisteredItems.Count} distinct items.");
            Console.WriteLine($"\n\n  [MojangItem  <{RegisteredItems.Count}> -> Item <{FinalItemList.Count}>] Conversion Completed.");
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\Items.json", JsonConvert.SerializeObject(FinalItemList, Formatting.Indented));
            Console.WriteLine($"\nWritten all Item to Item.json");
            Console.WriteLine($"\n\n  Done! Crafings added to Craft.json");


            //////////////////////////////////////////////////////////////////////////////
            CraftingRecipe fromMojangShaped(MojangShapedModel mojangCraft, List<Item> itemList)
            {
                List<char> totalkeys = new List<char>();
                List<ItemStack> itemStackList = new List<ItemStack>();
                foreach (var x in mojangCraft.keys.Keys)
                {
                    totalkeys.Add(x[0]);
                }
                Dictionary<char, int> quantity = new Dictionary<char, int>();
                //This gets the quantity for each item key.
                foreach (string line in mojangCraft.pattern)
                {
                    foreach (char c in totalkeys)
                    {
                        var count = line.Count(x => x == c);
                        if (!quantity.Keys.Contains(c))
                        {
                            quantity.Add(c, count);
                        }
                        else
                        {
                            var oldint = quantity[c];
                            var nInt = oldint + count;
                            quantity.Remove(c);
                            quantity.Add(c, nInt);
                        }
                    }
                }
                Console.WriteLine($"Quantity of items for {mojangCraft.result.item}");
                List<ItemStack> input4craft = new List<ItemStack>();
                foreach (var x in quantity)
                {
                    var y = mojangCraft.keys[x.Key.ToString()];
                    Console.WriteLine($"{x.Key}[{(y.Keys.Contains("item") ? y["item"] : y["tag"])}] > {x.Value} x");
                    string name = (y.Keys.Contains("item") ? y["item"] : y["tag"]);
                    ItemStack inputa = new ItemStack { Item = itemList.Find(ax => ax.Name == name.Split(':')[1]), Quantity = x.Value };
                    input4craft.Add(inputa);
                }
                CraftingRecipe cr = new CraftingRecipe
                {
                    Input = input4craft,
                    Output = new ItemStack { Item = itemList.Find(bx => bx.Name == mojangCraft.result.item.Split(':')[1]), Quantity = (mojangCraft.result.count == 0?1:mojangCraft.result.count) }
                };
                return cr;
            }








            ///////////////////////////////
            CraftingRecipe fromMojangShapeless(MojangShapelessModel mojangCraft, List<Item> itemList)
            {
                List<char> totalkeys = new List<char>();
                List<ItemStack> itemStackList = new List<ItemStack>();
                
                foreach (var ingredient in mojangCraft.ingredients) {
                    if (ingredient.item != null) {
                        ItemStack its = new ItemStack
                        {
                            Item = itemList.Find(ax => ax.Name == ingredient.item.Split(':')[1]),
                            Quantity = (ingredient.count == 0 ? 1 : ingredient.count)
                        };
                        itemStackList.Add(its);
                    }
                   
                }
                CraftingRecipe cr = new CraftingRecipe
                {
                    Input = itemStackList,
                    Output = new ItemStack
                    {
                        Item = itemList.Find(bx => bx.Name == mojangCraft.result.item.Split(':')[1]),
                        Quantity = (mojangCraft.result.count == 0 ? 1 : mojangCraft.result.count)
                    }

                };
                return cr;
            }

            Console.ReadLine();
        }



      
    }
}
