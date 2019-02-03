using CraftingHelper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
            if (!item.Exists) {


            }
            RegisteredItems = JsonConvert.DeserializeObject<List<Item>>(item.OpenText().ReadToEnd());
            RegisteredRecipes = JsonConvert.DeserializeObject<List<CraftingRecipe>>(crafts.OpenText().ReadToEnd());
        }



        ItemStack parse(String input) {
            string semDashR = input.Split('\r')[0];
            string semDashN = semDashR.Split('\n')[0];
            string name = semDashN.Split('x')[1];
            int quantity = int.Parse(semDashN.Split('x')[0]);
            Item i = RegisteredItems.Find(x => x.Name.Contains(name.ToLower()));
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
            
            while (FilaDeCraftings.Count > 0) {
                var item = FilaDeCraftings.Dequeue();
                var craft = RegisteredRecipes.Find(x => x.Output.Item.Name == item.Item.Name);
                if (craft != null)
                {
                    foreach (ItemStack its in craft.Input)
                    {
                        if (RegisteredRecipes.Find(x => x.Output.Item == its.Item) != null)
                        {
                            var nItem = new ItemStack { Item = its.Item, Quantity=item.Quantity};
                            FilaDeCraftings.Enqueue(nItem);
                        }
                        else {
                            if (ListaDeMateriais.Find(x => x.Item == its.Item) != null)
                            {
                                var itemnalista = ListaDeMateriais.Find(x => x.Item == its.Item);
                                var lquantidade = itemnalista.Quantity;
                                var qquantidade = its.Quantity * item.Quantity;
                                var tquantidade = lquantidade + qquantidade;
                                var nItem = new ItemStack { Item = its.Item, Quantity = tquantidade };
                                ListaDeMateriais.Remove(itemnalista);
                                ListaDeMateriais.Add(nItem);
                            }
                            else
                            {
                                var qquantidade = its.Quantity * item.Quantity;
                                var nItem = new ItemStack { Item = its.Item, Quantity = qquantidade };
                                ListaDeMateriais.Add(nItem);
                            }

                        }

                    }
                }
                else {
                    ListaDeMateriais.Add(item);
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
    }
}
