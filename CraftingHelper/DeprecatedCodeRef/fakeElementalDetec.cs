using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingHelper.DeprecatedCodeRef
{
    class fakeElementalDetec
    {
        //v0.1

        //List<Item> checkedItems = new List<Item>(); //I'll have to walk trought [slime_ball]
        //    foreach (Item uncheckedItem in FinalItemList) {
        //        Item checkedItem = new Item();
        //        var allCraft = UnelementizedCraftings.FindAll(x => x.Output.Item.Name == uncheckedItem.Name);
        //        if (allCraft.Count == 0)
        //        {
        //            checkedItem.Name = uncheckedItem.Name;
        //            checkedItem.Elemental = true;
                    
        //            if (checkedItems.Find(x => x.Name == uncheckedItem.Name) == null)
        //            {
        //                Console.WriteLine($"Elemental Item Found >  {checkedItem.Name}");
        //                checkedItems.Add(checkedItem); }

        //        }
        //        else if (allCraft.Count == 2)
        //        {
        //            //todo: Find where the issue is within slime_ball [ELEMENTAL] -> slime_block conversion.
        //            var firstCraft = allCraft[0];
        //            var secondCraft = allCraft[1]; 
        //            var cleanedIngotName = (uncheckedItem.Name.Contains("_ingot") ? $"{uncheckedItem.Name.Split('_')[0]}" : "");
        //            var cleanedBlockName = "";

        //            if (uncheckedItem.Name.Contains("_block"))
        //            {
        //                List<string> list = uncheckedItem.Name.Split('_').ToList();
        //                list.Remove(list.Last());
        //                list.ForEach(x => cleanedBlockName += $"{x}_");
        //                cleanedBlockName.Remove(cleanedBlockName.Count() - 1, cleanedBlockName.Count());

        //            }
        //            if (string.IsNullOrWhiteSpace(cleanedIngotName) && !string.IsNullOrWhiteSpace(cleanedBlockName)) //only triggers for ingots.
        //            {
        //                checkedItem.Name = uncheckedItem.Name;
        //                checkedItem.Elemental = false;
        //                if (checkedItems.Find(x => x.Name.Contains(uncheckedItem.Name)) == null)
        //                { checkedItems.Add(checkedItem); }
        //            }
        //            else if (!string.IsNullOrWhiteSpace(cleanedIngotName) && string.IsNullOrWhiteSpace(cleanedBlockName)) //triggers for any x_blocks.
        //            {
        //                if (firstCraft.Input.All(x => x.Item.Name.Contains(cleanedIngotName + "_nugget")) || firstCraft.Input.All(x => x.Item.Name.Contains(cleanedIngotName + "_block"))) //nugget checking.
        //                {
        //                    //this checks for what.
        //                    checkedItem.Name = uncheckedItem.Name;
        //                    checkedItem.Elemental = true;// this will add elemental to ingot / gem types.

        //                    if (checkedItems.Find(x => x.Name == uncheckedItem.Name) == null)
        //                    {
        //                        Console.WriteLine($"Elemental Item Found >  {checkedItem.Name}");
        //                        checkedItems.Add(checkedItem); //and this will add elemental if its not craftable.
        //                    }
        //                }
        //                else //Seems like x_block only does the checking for nuggets. what it should do its checks for everything.
        //                {
        //                    if (firstCraft.Input.All(x => x.Item.Name.Contains(cleanedBlockName))) //slime_ball -c> slime any["slime"] = true;
        //                    {
        //                        var innerItemStack = firstCraft.Input[0];
        //                        var innerCraft = UnelementizedCraftings.FindAll(x => x.Output.Item.Name == innerItemStack.Item.Name);
        //                        if (innerCraft.Count == 1)
        //                        {
        //                            //probably a [item -> block -> item] loop.
        //                            if (innerCraft[0].Input.All(x => x.Item.Name == uncheckedItem.Name))
        //                            {
        //                                //this is certainly a ibi loop. and this is the inner item.
        //                                Item innerItem = new Item();
        //                                innerItem.Name = innerItemStack.Item.Name;
        //                                innerItem.Elemental = true;

        //                                Item outerItem = new Item();
        //                                outerItem.Name = uncheckedItem.Name;
        //                                outerItem.Elemental = false;
        //                                if (checkedItems.Find(x => x.Name == innerItem.Name) == null)
        //                                {
        //                                    //adds the innerItem if not found.
        //                                    Console.WriteLine($"Elemental Item Found > {innerItem.Name}");
        //                                    checkedItems.Add(innerItem);
        //                                }
        //                                if (checkedItems.Find(x => x.Name == outerItem.Name) == null)
        //                                {
        //                                    checkedItems.Add(outerItem);

        //                                }



        //                            }
        //                            else
        //                            {
        //                                //not an ibi loop. but its craftable.
        //                                Item newItem = new Item();
        //                                newItem.Name = uncheckedItem.Name;
        //                                newItem.Elemental = false;
        //                                if (checkedItems.Find(x => x.Name == newItem.Name) == null)
        //                                {
        //                                    checkedItems.Add(newItem);
        //                                }
        //                            }

        //                        }
        //                        else
        //                        {
        //                            //has more than one craft so it couldn't be an ibi loop without being an gem/oreType.
        //                            Item newItem = new Item();
        //                            newItem.Name = uncheckedItem.Name;
        //                            newItem.Elemental = false;
        //                            if (checkedItems.Find(x => x.Name == newItem.Name) == null)
        //                            {
        //                                checkedItems.Add(newItem);
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //            else //other item checking.
        //            {
        //                checkedItem.Name = uncheckedItem.Name;
        //                checkedItem.Elemental = false;
        //                if (checkedItems.Find(x => x.Name.Contains(uncheckedItem.Name)) == null)
        //                { checkedItems.Add(checkedItem); }
        //            }

        //        }
        //        else if (allCraft.Count == 1)
        //        {
        //            var firstCraft = allCraft[0];


        //            var cleanedBlockName = "";

        //            if (uncheckedItem.Name.Contains("_block"))
        //            {
        //                List<string> list = uncheckedItem.Name.Split('_').ToList();
        //                list.Remove(list.Last());
        //                list.ForEach(x => cleanedBlockName += $"{x}_");
        //                cleanedBlockName.Remove(cleanedBlockName.Count() - 1);

        //            }
        //            if (!string.IsNullOrWhiteSpace(cleanedBlockName))
        //            {
        //                checkedItem.Name = uncheckedItem.Name;
        //                checkedItem.Elemental = false;
        //                if (checkedItems.Find(x => x.Name.Contains(uncheckedItem.Name)) == null)
        //                { checkedItems.Add(checkedItem); }


        //            }
        //            foreach (var craft in allCraft)
        //            {
        //                List<ItemStack> innerInputItems = craft.Input;
        //                foreach (ItemStack itemstack in innerInputItems)
        //                {
        //                    var innerLookup = UnelementizedCraftings.FindAll(x => x.Output.Item.Name == itemstack.Item.Name);
        //                    if (innerLookup.Count == 0)
        //                    {
        //                        checkedItem.Name = itemstack.Item.Name;
                               
        //                        checkedItem.Elemental = true;
        //                        if (checkedItems.Find(x => x.Name == itemstack.Item.Name) == null)
        //                        {
        //                            Console.WriteLine($"Elemental Item Found >  {checkedItem.Name}");
        //                            checkedItems.Add(checkedItem);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (innerLookup.Count == 1 && uncheckedItem.Name.Contains("_block"))
        //                        {
        //                            checkedItem.Name = itemstack.Item.Name;

        //                            checkedItem.Elemental = true;
        //                            if (checkedItems.Find(x => x.Name == itemstack.Item.Name) == null)
        //                            {
        //                                Console.WriteLine($"Elemental Item Found >  {checkedItem.Name}");
        //                                checkedItems.Add(checkedItem);
        //                            }

        //                        }
                                
        //                    }
        //                }


        //                checkedItem.Name = uncheckedItem.Name;
        //                checkedItem.Elemental = false;
        //                if (checkedItems.Find(x => x.Name == uncheckedItem.Name) == null)
        //                { checkedItems.Add(checkedItem); }
        //            }
        //        }
        //        else
        //        {
        //            checkedItem.Name = uncheckedItem.Name;
        //            checkedItem.Elemental = false;
        //            if (checkedItems.Find(x => x.Name == uncheckedItem.Name) == null)
        //            { checkedItems.Add(checkedItem); }

        //            foreach (var craft in allCraft)
        //            {
        //                List<ItemStack> innerInputItems = craft.Input;
        //                foreach (ItemStack itemstack in innerInputItems)
        //                {
        //                    var innerLookup = UnelementizedCraftings.FindAll(x => x.Output.Item.Name == itemstack.Item.Name);
        //                    if (innerLookup.Count == 0)
        //                    {
        //                        checkedItem.Name = itemstack.Item.Name;
        //                        checkedItem.Elemental = true;
        //                        if (checkedItems.Find(x => x.Name == itemstack.Item.Name) == null)
        //                        { checkedItems.Add(checkedItem); }
        //                    }
        //                    else
        //                    {
        //                        checkedItem.Name = itemstack.Item.Name;
        //                        checkedItem.Elemental = false;
        //                        if (checkedItems.Find(x => x.Name == itemstack.Item.Name) == null)
        //                        { checkedItems.Add(checkedItem); }
        //                    }
        //                }

        //            }
        //            checkedItem.Name = uncheckedItem.Name;
        //            checkedItem.Elemental = false;
        //            if (checkedItems.Find(x => x.Name == uncheckedItem.Name) == null)
        //            { checkedItems.Add(checkedItem); }
        //        }
        //    }
    }
}
