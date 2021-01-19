namespace Aipa.Vista
{
    partial class Ventana_manual
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_manual));
            this.Barratitulo = new System.Windows.Forms.Panel();
            this.boton_restaurar = new System.Windows.Forms.PictureBox();
            this.boton_maximizar = new System.Windows.Forms.PictureBox();
            this.boton_minimizar = new System.Windows.Forms.PictureBox();
            this.boton_cerrar = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Barratitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boton_restaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_maximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_cerrar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Barratitulo
            // 
            this.Barratitulo.BackColor = System.Drawing.SystemColors.WindowText;
            this.Barratitulo.Controls.Add(this.boton_restaurar);
            this.Barratitulo.Controls.Add(this.boton_maximizar);
            this.Barratitulo.Controls.Add(this.boton_minimizar);
            this.Barratitulo.Controls.Add(this.boton_cerrar);
            this.Barratitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.Barratitulo.Location = new System.Drawing.Point(0, 0);
            this.Barratitulo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Barratitulo.Name = "Barratitulo";
            this.Barratitulo.Size = new System.Drawing.Size(800, 39);
            this.Barratitulo.TabIndex = 2;
            this.Barratitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Barratitulo_MouseMove);
            // 
            // boton_restaurar
            // 
            this.boton_restaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_restaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boton_restaurar.Image = ((System.Drawing.Image)(resources.GetObject("boton_restaurar.Image")));
            this.boton_restaurar.Location = new System.Drawing.Point(737, 9);
            this.boton_restaurar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_restaurar.Name = "boton_restaurar";
            this.boton_restaurar.Size = new System.Drawing.Size(25, 25);
            this.boton_restaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boton_restaurar.TabIndex = 6;
            this.boton_restaurar.TabStop = false;
            this.boton_restaurar.Visible = false;
            this.boton_restaurar.Click += new System.EventHandler(this.boton_restaurar_Click_1);
            // 
            // boton_maximizar
            // 
            this.boton_maximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_maximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boton_maximizar.Image = ((System.Drawing.Image)(resources.GetObject("boton_maximizar.Image")));
            this.boton_maximizar.Location = new System.Drawing.Point(737, 9);
            this.boton_maximizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_maximizar.Name = "boton_maximizar";
            this.boton_maximizar.Size = new System.Drawing.Size(25, 23);
            this.boton_maximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boton_maximizar.TabIndex = 5;
            this.boton_maximizar.TabStop = false;
            this.boton_maximizar.Click += new System.EventHandler(this.boton_maximizar_Click_1);
            // 
            // boton_minimizar
            // 
            this.boton_minimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_minimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boton_minimizar.Image = ((System.Drawing.Image)(resources.GetObject("boton_minimizar.Image")));
            this.boton_minimizar.Location = new System.Drawing.Point(706, 9);
            this.boton_minimizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_minimizar.Name = "boton_minimizar";
            this.boton_minimizar.Size = new System.Drawing.Size(25, 23);
            this.boton_minimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boton_minimizar.TabIndex = 4;
            this.boton_minimizar.TabStop = false;
            // 
            // boton_cerrar
            // 
            this.boton_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_cerrar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.boton_cerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.boton_cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boton_cerrar.Image = ((System.Drawing.Image)(resources.GetObject("boton_cerrar.Image")));
            this.boton_cerrar.Location = new System.Drawing.Point(768, 9);
            this.boton_cerrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_cerrar.Name = "boton_cerrar";
            this.boton_cerrar.Size = new System.Drawing.Size(25, 23);
            this.boton_cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boton_cerrar.TabIndex = 2;
            this.boton_cerrar.TabStop = false;
            this.boton_cerrar.Click += new System.EventHandler(this.boton_cerrar_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 574);
            this.panel1.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(800, 574);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Ventana_manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 613);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Barratitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Ventana_manual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ventana_manual";
            this.Barratitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.boton_restaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_maximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_cerrar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Barratitulo;
        private System.Windows.Forms.PictureBox boton_restaurar;
        private System.Windows.Forms.PictureBox boton_maximizar;
        private System.Windows.Forms.PictureBox boton_minimizar;
        private System.Windows.Forms.PictureBox boton_cerrar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
    }
}