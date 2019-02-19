using CraftingHelper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CraftingHelper
{
    public partial class frmCraftingHelper : Form
    {
        List<Item> RegisteredItems = new List<Item> ();
        FileInfo item = new FileInfo(Directory.GetCurrentDirectory() + @"\Items.json");
        FileInfo crafts = new FileInfo(Directory.GetCurrentDirectory() + @"\Craft.json");
        List<CraftingRecipe> RegisteredRecipes = new List<CraftingRecipe>();
        List<ItemStack> ListaDeMateriais = new List<ItemStack>();
        Queue<ItemStack> FilaDeCraftings = new Queue<ItemStack>();
        public frmCraftingHelper()
        {
            InitializeComponent();
            RegisteredItems = JsonConvert.DeserializeObject<List<Item>>(item.OpenText().ReadToEnd());
            RegisteredRecipes = JsonConvert.DeserializeObject<List<CraftingRecipe>>(crafts.OpenText().ReadToEnd());
        }



        ItemStack parse(String input) {
            string semDashR = input.Split('\r')[0];
            string semDashN = semDashR.Split('\n')[0];
            string name = semDashN.Split('x')[1];
            int quantity = int.Parse(semDashN.Split('x')[0]);
            Item i = RegisteredItems.Find(x => x.Name == name);
            var _is = new ItemStack { Item = i, Quantity = quantity }; ;
            FilaDeCraftings.Enqueue(_is);
            return _is;

        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbInput.Text)&&tbInput.Text.Last() == '\n')
            {
                ItemStack i = parse(tbInput.Text);
                if (i.Item != null)
                {
                    tbItems.Text += $"{i.Quantity} x | {i.Item.Name}\r\n";
                    tbInput.Text = "";
                }
                else {
                    tbInput.Text = "Item não encotrado tente novamente.";
                }

            }
        }

        private void tbInput_Click(object sender, EventArgs e)
        {
            if (tbInput.Text == "Item não encotrado tente novamente.") {
                tbInput.Text = "";
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {

            while (FilaDeCraftings.Count > 0)
            {
                var itemStack = FilaDeCraftings.Dequeue();
                var itemOutput = itemStack.Item;
                if (!itemOutput.Elemental)
                {
                    var allRecipes = RegisteredRecipes.FindAll(x => x.Output.Item.Name == itemOutput.Name);
                    if (allRecipes.Count == 1)
                    {
                        List<ItemStack> iteminput = allRecipes[0].Input;
                        foreach (ItemStack item in iteminput)
                        {
                            if (item.Item.Elemental)
                            {
                                if (ListaDeMateriais.Find(x => x.Item == item.Item) == null)
                                {
                                    ListaDeMateriais.Add(new ItemStack { Item = item.Item, Quantity = (int)Math.Round(((double)itemStack.Quantity * (double)item.Quantity)) });
                                }
                                else
                                {
                                    var oldItem = ListaDeMateriais.Find(x => x.Item == item.Item);
                                    var fixQtd = oldItem.Quantity + (int)Math.Round((double)itemStack.Quantity * (double)item.Quantity);
                                    ItemStack novoItem = new ItemStack { Item = oldItem.Item, Quantity = fixQtd };
                                    ListaDeMateriais.Remove(ListaDeMateriais.Find(x => x.Item == item.Item));
                                    ListaDeMateriais.Add(novoItem);
                                }

                            }
                            else
                            {
                                var fixQtd = item.Quantity + (int)Math.Round((double)itemStack.Quantity * (double)item.Quantity);
                                FilaDeCraftings.Enqueue(new ItemStack { Item = item.Item, Quantity = fixQtd });
                            }
                        }
                    }
                    else
                    {
                        int Minimum = allRecipes.Min(x => x.Input.Count);
                        var LowestValue = allRecipes.FindAll(x => x.Input.Count == Minimum);

                        List<ItemStack> iteminput = LowestValue[0].Input;
                        foreach (ItemStack item in iteminput)
                        {
                            if (item.Item.Elemental)
                            {
                                if (ListaDeMateriais.Find(x => x.Item == item.Item) == null)
                                {
                                    ListaDeMateriais.Add(new ItemStack { Item = item.Item, Quantity = (int)Math.Round((double)itemStack.Quantity * (double)item.Quantity) });
                                }
                                else
                                {
                                    var oldItem = ListaDeMateriais.Find(x => x.Item == item.Item);
                                    var fixQtd = oldItem.Quantity + (int)Math.Round((double)itemStack.Quantity * (double)item.Quantity);
                                    ItemStack novoItem = new ItemStack { Item = oldItem.Item, Quantity = fixQtd };
                                    ListaDeMateriais.Remove(ListaDeMateriais.Find(x => x.Item == item.Item));
                                    ListaDeMateriais.Add(novoItem);
                                }

                            }
                            else
                            {
                                var fixQtd = item.Quantity + (int)Math.Round((double)itemStack.Quantity * (double)item.Quantity);
                                FilaDeCraftings.Enqueue(new ItemStack { Item = item.Item, Quantity = fixQtd });
                            }
                        }

                    }
                }
                else
                {
                    if (ListaDeMateriais.Find(x => x.Item == itemOutput) == null)
                    {
                        ListaDeMateriais.Add(itemStack);
                    }
                    else
                    {
                        var oldItem = ListaDeMateriais.Find(x => x.Item == itemOutput);
                        var fixQtd = oldItem.Quantity + (int)Math.Round((double)itemStack.Quantity);
                        ItemStack novoItem = new ItemStack { Item = oldItem.Item, Quantity = fixQtd };
                        ListaDeMateriais.Remove(ListaDeMateriais.Find(x => x.Item == itemOutput));
                        ListaDeMateriais.Add(novoItem);
                    }
                }

            }

            foreach (ItemStack its in ListaDeMateriais) {
                tbOutput.Text += $"{its.Quantity} x | {its.Item.Name}\r\n";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear() {
            FilaDeCraftings.Clear();
            ListaDeMateriais.Clear();
            tbInput.Clear();
            tbItems.Clear();
            tbOutput.Clear();
        }

        private void btnRecipes_Click(object sender, EventArgs e)
        {
            string allrecipe = "";
            RegisteredRecipes.ForEach(x => { allrecipe += $" {x.Output.Item.Name}\r\n"; });
            tbOutput.Text = allrecipe;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
