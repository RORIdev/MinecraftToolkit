namespace CraftingHelper
{
    partial class frmCraftingHelper
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCraftingHelper));
            this.label1 = new System.Windows.Forms.Label();
            this.tbItems = new System.Windows.Forms.TextBox();
            this.tbInput = new System.Windows.Forms.RichTextBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRecipes = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Digite os items [Ex : 90xCobble]";
            // 
            // tbItems
            // 
            this.tbItems.Enabled = false;
            this.tbItems.Location = new System.Drawing.Point(15, 75);
            this.tbItems.Multiline = true;
            this.tbItems.Name = "tbItems";
            this.tbItems.Size = new System.Drawing.Size(241, 301);
            this.tbItems.TabIndex = 2;
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(15, 28);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(241, 26);
            this.tbInput.TabIndex = 3;
            this.tbInput.Text = "";
            this.tbInput.Click += new System.EventHandler(this.tbInput_Click);
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(96, 395);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(160, 23);
            this.btnCalcular.TabIndex = 4;
            this.btnCalcular.Text = "Calcular !";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(371, 75);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(251, 301);
            this.tbOutput.TabIndex = 5;
            this.tbOutput.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Output";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(15, 395);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRecipes
            // 
            this.btnRecipes.Location = new System.Drawing.Point(493, 426);
            this.btnRecipes.Name = "btnRecipes";
            this.btnRecipes.Size = new System.Drawing.Size(117, 23);
            this.btnRecipes.TabIndex = 8;
            this.btnRecipes.Text = "Added Recipes";
            this.btnRecipes.UseVisualStyleBackColor = true;
            this.btnRecipes.Click += new System.EventHandler(this.btnRecipes_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 379);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(368, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "You can add your own recipe.json if they dont accept different items per key.";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(447, 395);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "W.I.P. RecipeAdder";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmCraftingHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 461);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRecipes);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.tbItems);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCraftingHelper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crafting Helper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbItems;
        private System.Windows.Forms.RichTextBox tbInput;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.RichTextBox tbOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRecipes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}

