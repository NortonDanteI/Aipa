using System.Drawing;

namespace Aipa.Vista
{
    partial class Ventana_coronacion
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
            this.pieza_reina = new System.Windows.Forms.PictureBox();
            this.pieza_alfil = new System.Windows.Forms.PictureBox();
            this.pieza_caballo = new System.Windows.Forms.PictureBox();
            this.pieza_torre = new System.Windows.Forms.PictureBox();
            this.Barratitulo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pieza_reina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieza_alfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieza_caballo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieza_torre)).BeginInit();
            this.Barratitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pieza_reina
            // 
            this.pieza_reina.BackColor = System.Drawing.Color.LightGray;
            this.pieza_reina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pieza_reina.Location = new System.Drawing.Point(13, 54);
            this.pieza_reina.Margin = new System.Windows.Forms.Padding(4);
            this.pieza_reina.Name = "pieza_reina";
            this.pieza_reina.Size = new System.Drawing.Size(123, 113);
            this.pieza_reina.TabIndex = 0;
            this.pieza_reina.TabStop = false;
            this.pieza_reina.Click += new System.EventHandler(this.Reina_click);
            // 
            // pieza_alfil
            // 
            this.pieza_alfil.BackColor = System.Drawing.Color.LightGray;
            this.pieza_alfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pieza_alfil.Location = new System.Drawing.Point(147, 54);
            this.pieza_alfil.Margin = new System.Windows.Forms.Padding(4);
            this.pieza_alfil.Name = "pieza_alfil";
            this.pieza_alfil.Size = new System.Drawing.Size(123, 113);
            this.pieza_alfil.TabIndex = 0;
            this.pieza_alfil.TabStop = false;
            this.pieza_alfil.Click += new System.EventHandler(this.Alfil_click);
            // 
            // pieza_caballo
            // 
            this.pieza_caballo.BackColor = System.Drawing.Color.LightGray;
            this.pieza_caballo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pieza_caballo.Location = new System.Drawing.Point(278, 54);
            this.pieza_caballo.Margin = new System.Windows.Forms.Padding(4);
            this.pieza_caballo.Name = "pieza_caballo";
            this.pieza_caballo.Size = new System.Drawing.Size(123, 113);
            this.pieza_caballo.TabIndex = 0;
            this.pieza_caballo.TabStop = false;
            this.pieza_caballo.Click += new System.EventHandler(this.Caballo_click);
            // 
            // pieza_torre
            // 
            this.pieza_torre.BackColor = System.Drawing.Color.LightGray;
            this.pieza_torre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pieza_torre.Location = new System.Drawing.Point(409, 54);
            this.pieza_torre.Margin = new System.Windows.Forms.Padding(4);
            this.pieza_torre.Name = "pieza_torre";
            this.pieza_torre.Size = new System.Drawing.Size(123, 113);
            this.pieza_torre.TabIndex = 0;
            this.pieza_torre.TabStop = false;
            this.pieza_torre.Click += new System.EventHandler(this.Torre_click);
            // 
            // Barratitulo
            // 
            this.Barratitulo.BackColor = System.Drawing.SystemColors.WindowText;
            this.Barratitulo.Controls.Add(this.label1);
            this.Barratitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.Barratitulo.Location = new System.Drawing.Point(0, 0);
            this.Barratitulo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Barratitulo.Name = "Barratitulo";
            this.Barratitulo.Size = new System.Drawing.Size(546, 39);
            this.Barratitulo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(202, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seleccione pieza";
            // 
            // Ventana_coronacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(546, 189);
            this.Controls.Add(this.Barratitulo);
            this.Controls.Add(this.pieza_torre);
            this.Controls.Add(this.pieza_caballo);
            this.Controls.Add(this.pieza_alfil);
            this.Controls.Add(this.pieza_reina);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ventana_coronacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = new Point(720,590);
            this.Text = "selector";
            ((System.ComponentModel.ISupportInitialize)(this.pieza_reina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieza_alfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieza_caballo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieza_torre)).EndInit();
            this.Barratitulo.ResumeLayout(false);
            this.Barratitulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pieza_reina;
        private System.Windows.Forms.PictureBox pieza_alfil;
        private System.Windows.Forms.PictureBox pieza_caballo;
        private System.Windows.Forms.PictureBox pieza_torre;
        private System.Windows.Forms.Panel Barratitulo;
        private System.Windows.Forms.Label label1;
    }
}