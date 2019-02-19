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
                    Console.WriteLine($"Elemental Item Found >  {item.Name}");

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

                        
                        string polishedItemName = (item.Name.Contains("_ingot") ? $"{item.Name.Split('_')[0]}" : "");
                        if (item.Name == "emerald" || item.Name == "diamond" || item.Name == "redstone")
                        {
                            polishedItemName = item.Name;
                        }
                        if (item.Name == "lapis_lazuli") {
                            polishedItemName = "lapis";
                        }
                        if (!String.IsNullOrWhiteSpace(polishedItemName) )
                        {
                          
                            item.Elemental = true;
                            if (!FinalItemList.Contains(item))
                            {
                                Console.WriteLine($"Elemental Item Found >  {item.Name}");
                                FinalItemList.Add(item);
                            }
                        }
                        else {
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
