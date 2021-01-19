namespace Aipa.Vista
{
    partial class Ventana_juego
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_juego));
            this.panel_contenedor = new System.Windows.Forms.Panel();
            this.panel_derecho = new System.Windows.Forms.Panel();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.panel_izquierdo = new System.Windows.Forms.Panel();
            this.resetear = new System.Windows.Forms.Button();
            this.franja_negra3 = new System.Windows.Forms.PictureBox();
            this.emoticon_guia = new System.Windows.Forms.PictureBox();
            this.boton_activar_consejos = new System.Windows.Forms.Button();
            this.label_estado = new System.Windows.Forms.Label();
            this.label_2 = new System.Windows.Forms.Label();
            this.franja_negra2 = new System.Windows.Forms.PictureBox();
            this.label_recomendacion = new System.Windows.Forms.Label();
            this.label_jugador = new System.Windows.Forms.Label();
            this.label_1 = new System.Windows.Forms.Label();
            this.franja_negra4 = new System.Windows.Forms.PictureBox();
            this.franja_negra1 = new System.Windows.Forms.PictureBox();
            this.emoticon_manual = new System.Windows.Forms.PictureBox();
            this.boton_manual_usuario = new System.Windows.Forms.Button();
            this.Barratitulo = new System.Windows.Forms.Panel();
            this.boton_restaurar = new System.Windows.Forms.PictureBox();
            this.boton_maximizar = new System.Windows.Forms.PictureBox();
            this.boton_minimizar = new System.Windows.Forms.PictureBox();
            this.boton_cerrar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_contenedor.SuspendLayout();
            this.panel_derecho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.panel_izquierdo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.franja_negra3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoticon_guia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.franja_negra2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.franja_negra4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.franja_negra1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoticon_manual)).BeginInit();
            this.Barratitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boton_restaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_maximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_cerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_contenedor
            // 
            this.panel_contenedor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel_contenedor.Controls.Add(this.panel_derecho);
            this.panel_contenedor.Controls.Add(this.panel_izquierdo);
            this.panel_contenedor.Controls.Add(this.Barratitulo);
            this.panel_contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_contenedor.Location = new System.Drawing.Point(0, 0);
            this.panel_contenedor.Name = "panel_contenedor";
            this.panel_contenedor.Size = new System.Drawing.Size(1298, 1102);
            this.panel_contenedor.TabIndex = 0;
            // 
            // panel_derecho
            // 
            this.panel_derecho.Controls.Add(this.Canvas);
            this.panel_derecho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_derecho.Location = new System.Drawing.Point(384, 39);
            this.panel_derecho.Name = "panel_derecho";
            this.panel_derecho.Size = new System.Drawing.Size(914, 1063);
            this.panel_derecho.TabIndex = 3;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Image = ((System.Drawing.Image)(resources.GetObject("Canvas.Image")));
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Margin = new System.Windows.Forms.Padding(4);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(914, 1063);
            this.Canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Canvas.TabIndex = 1;
            this.Canvas.TabStop = false;
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Ventana_juego_Canvas_MouseUp);
            // 
            // panel_izquierdo
            // 
            this.panel_izquierdo.BackColor = System.Drawing.Color.LightGray;
            this.panel_izquierdo.Controls.Add(this.label1);
            this.panel_izquierdo.Controls.Add(this.resetear);
            this.panel_izquierdo.Controls.Add(this.franja_negra3);
            this.panel_izquierdo.Controls.Add(this.emoticon_guia);
            this.panel_izquierdo.Controls.Add(this.boton_activar_consejos);
            this.panel_izquierdo.Controls.Add(this.label_estado);
            this.panel_izquierdo.Controls.Add(this.label_2);
            this.panel_izquierdo.Controls.Add(this.franja_negra2);
            this.panel_izquierdo.Controls.Add(this.label_recomendacion);
            this.panel_izquierdo.Controls.Add(this.label_jugador);
            this.panel_izquierdo.Controls.Add(this.label_1);
            this.panel_izquierdo.Controls.Add(this.franja_negra4);
            this.panel_izquierdo.Controls.Add(this.franja_negra1);
            this.panel_izquierdo.Controls.Add(this.emoticon_manual);
            this.panel_izquierdo.Controls.Add(this.boton_manual_usuario);
            this.panel_izquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_izquierdo.Location = new System.Drawing.Point(0, 39);
            this.panel_izquierdo.Name = "panel_izquierdo";
            this.panel_izquierdo.Size = new System.Drawing.Size(384, 1063);
            this.panel_izquierdo.TabIndex = 2;
            // 
            // resetear
            // 
            this.resetear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.resetear.FlatAppearance.BorderSize = 0;
            this.resetear.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.resetear.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.resetear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetear.Font = new System.Drawing.Font("Monotype Corsiva", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetear.ForeColor = System.Drawing.SystemColors.WindowText;
            this.resetear.Location = new System.Drawing.Point(0, 537);
            this.resetear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resetear.Name = "resetear";
            this.resetear.Size = new System.Drawing.Size(384, 38);
            this.resetear.TabIndex = 36;
            this.resetear.Text = "Resetear";
            this.resetear.UseVisualStyleBackColor = false;
            this.resetear.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // franja_negra3
            // 
            this.franja_negra3.BackColor = System.Drawing.SystemColors.WindowText;
            this.franja_negra3.Location = new System.Drawing.Point(0, 227);
            this.franja_negra3.Name = "franja_negra3";
            this.franja_negra3.Size = new System.Drawing.Size(8, 38);
            this.franja_negra3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.franja_negra3.TabIndex = 35;
            this.franja_negra3.TabStop = false;
            // 
            // emoticon_guia
            // 
            this.emoticon_guia.Image = ((System.Drawing.Image)(resources.GetObject("emoticon_guia.Image")));
            this.emoticon_guia.Location = new System.Drawing.Point(8, 227);
            this.emoticon_guia.Name = "emoticon_guia";
            this.emoticon_guia.Size = new System.Drawing.Size(48, 38);
            this.emoticon_guia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.emoticon_guia.TabIndex = 34;
            this.emoticon_guia.TabStop = false;
            // 
            // boton_activar_consejos
            // 
            this.boton_activar_consejos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.boton_activar_consejos.FlatAppearance.BorderSize = 0;
            this.boton_activar_consejos.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.boton_activar_consejos.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.boton_activar_consejos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boton_activar_consejos.Font = new System.Drawing.Font("Monotype Corsiva", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boton_activar_consejos.ForeColor = System.Drawing.SystemColors.WindowText;
            this.boton_activar_consejos.Location = new System.Drawing.Point(0, 227);
            this.boton_activar_consejos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_activar_consejos.Name = "boton_activar_consejos";
            this.boton_activar_consejos.Size = new System.Drawing.Size(384, 38);
            this.boton_activar_consejos.TabIndex = 33;
            this.boton_activar_consejos.Text = "Consejos";
            this.boton_activar_consejos.UseVisualStyleBackColor = false;
            this.boton_activar_consejos.Click += new System.EventHandler(this.boton_activar_consejos_Click);
            // 
            // label_estado
            // 
            this.label_estado.AutoSize = true;
            this.label_estado.Font = new System.Drawing.Font("Monotype Corsiva", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_estado.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label_estado.Location = new System.Drawing.Point(153, 174);
            this.label_estado.Name = "label_estado";
            this.label_estado.Size = new System.Drawing.Size(62, 29);
            this.label_estado.TabIndex = 32;
            this.label_estado.Text = "Jaque";
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Font = new System.Drawing.Font("Monotype Corsiva", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_2.ForeColor = System.Drawing.Color.Sienna;
            this.label_2.Location = new System.Drawing.Point(80, 132);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(216, 29);
            this.label_2.TabIndex = 31;
            this.label_2.Text = "Estado de la partida:";
            this.label_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // franja_negra2
            // 
            this.franja_negra2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.franja_negra2.Location = new System.Drawing.Point(0, 136);
            this.franja_negra2.Name = "franja_negra2";
            this.franja_negra2.Size = new System.Drawing.Size(384, 25);
            this.franja_negra2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.franja_negra2.TabIndex = 30;
            this.franja_negra2.TabStop = false;
            // 
            // label_recomendacion
            // 
            this.label_recomendacion.AutoSize = true;
            this.label_recomendacion.BackColor = System.Drawing.Color.LightGray;
            this.label_recomendacion.Font = new System.Drawing.Font("Monotype Corsiva", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_recomendacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label_recomendacion.Location = new System.Drawing.Point(3, 280);
            this.label_recomendacion.Name = "label_recomendacion";
            this.label_recomendacion.Size = new System.Drawing.Size(324, 174);
            this.label_recomendacion.TabIndex = 28;
            this.label_recomendacion.Text = "Se recomiendan los siguientes \r\nmovimientos:\r\n* Mover la pieza X en la posición A" +
    " \r\nhacia la posición B.\r\n* Mover la pieza X en la posición A\r\nhacia la posición " +
    "B.";
            this.label_recomendacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_jugador
            // 
            this.label_jugador.AutoSize = true;
            this.label_jugador.Font = new System.Drawing.Font("Monotype Corsiva", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_jugador.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label_jugador.Location = new System.Drawing.Point(144, 92);
            this.label_jugador.Name = "label_jugador";
            this.label_jugador.Size = new System.Drawing.Size(82, 29);
            this.label_jugador.TabIndex = 26;
            this.label_jugador.Text = "Jugador";
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Font = new System.Drawing.Font("Monotype Corsiva", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_1.ForeColor = System.Drawing.Color.Sienna;
            this.label_1.Location = new System.Drawing.Point(144, 51);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(105, 29);
            this.label_1.TabIndex = 25;
            this.label_1.Text = "Turno de:";
            this.label_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // franja_negra4
            // 
            this.franja_negra4.BackColor = System.Drawing.SystemColors.WindowText;
            this.franja_negra4.Location = new System.Drawing.Point(0, 467);
            this.franja_negra4.Name = "franja_negra4";
            this.franja_negra4.Size = new System.Drawing.Size(8, 38);
            this.franja_negra4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.franja_negra4.TabIndex = 18;
            this.franja_negra4.TabStop = false;
            // 
            // franja_negra1
            // 
            this.franja_negra1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.franja_negra1.Location = new System.Drawing.Point(0, 51);
            this.franja_negra1.Name = "franja_negra1";
            this.franja_negra1.Size = new System.Drawing.Size(384, 25);
            this.franja_negra1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.franja_negra1.TabIndex = 14;
            this.franja_negra1.TabStop = false;
            // 
            // emoticon_manual
            // 
            this.emoticon_manual.Image = ((System.Drawing.Image)(resources.GetObject("emoticon_manual.Image")));
            this.emoticon_manual.Location = new System.Drawing.Point(8, 467);
            this.emoticon_manual.Name = "emoticon_manual";
            this.emoticon_manual.Size = new System.Drawing.Size(48, 38);
            this.emoticon_manual.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.emoticon_manual.TabIndex = 13;
            this.emoticon_manual.TabStop = false;
            // 
            // boton_manual_usuario
            // 
            this.boton_manual_usuario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.boton_manual_usuario.FlatAppearance.BorderSize = 0;
            this.boton_manual_usuario.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.boton_manual_usuario.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.boton_manual_usuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boton_manual_usuario.Font = new System.Drawing.Font("Monotype Corsiva", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boton_manual_usuario.ForeColor = System.Drawing.SystemColors.WindowText;
            this.boton_manual_usuario.Location = new System.Drawing.Point(38, 467);
            this.boton_manual_usuario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_manual_usuario.Name = "boton_manual_usuario";
            this.boton_manual_usuario.Size = new System.Drawing.Size(346, 38);
            this.boton_manual_usuario.TabIndex = 4;
            this.boton_manual_usuario.Text = "Manual de usuario";
            this.boton_manual_usuario.UseVisualStyleBackColor = false;
            this.boton_manual_usuario.Click += new System.EventHandler(this.Boton_manual_usuario_click);
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
            this.Barratitulo.Size = new System.Drawing.Size(1298, 39);
            this.Barratitulo.TabIndex = 1;
            this.Barratitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Barratitulo_MouseMove);
            // 
            // boton_restaurar
            // 
            this.boton_restaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_restaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boton_restaurar.Image = ((System.Drawing.Image)(resources.GetObject("boton_restaurar.Image")));
            this.boton_restaurar.Location = new System.Drawing.Point(1235, 7);
            this.boton_restaurar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_restaurar.Name = "boton_restaurar";
            this.boton_restaurar.Size = new System.Drawing.Size(25, 25);
            this.boton_restaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boton_restaurar.TabIndex = 6;
            this.boton_restaurar.TabStop = false;
            this.boton_restaurar.Visible = false;
            this.boton_restaurar.Click += new System.EventHandler(this.Boton_restaurar_Click);
            // 
            // boton_maximizar
            // 
            this.boton_maximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_maximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boton_maximizar.Image = ((System.Drawing.Image)(resources.GetObject("boton_maximizar.Image")));
            this.boton_maximizar.Location = new System.Drawing.Point(1235, 9);
            this.boton_maximizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_maximizar.Name = "boton_maximizar";
            this.boton_maximizar.Size = new System.Drawing.Size(25, 23);
            this.boton_maximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boton_maximizar.TabIndex = 5;
            this.boton_maximizar.TabStop = false;
            this.boton_maximizar.Click += new System.EventHandler(this.Boton_maximizar_Click);
            // 
            // boton_minimizar
            // 
            this.boton_minimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_minimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boton_minimizar.Image = ((System.Drawing.Image)(resources.GetObject("boton_minimizar.Image")));
            this.boton_minimizar.Location = new System.Drawing.Point(1204, 9);
            this.boton_minimizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_minimizar.Name = "boton_minimizar";
            this.boton_minimizar.Size = new System.Drawing.Size(25, 23);
            this.boton_minimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boton_minimizar.TabIndex = 4;
            this.boton_minimizar.TabStop = false;
            this.boton_minimizar.Click += new System.EventHandler(this.Boton_minimizar_Click_1);
            // 
            // boton_cerrar
            // 
            this.boton_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boton_cerrar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.boton_cerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.boton_cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boton_cerrar.Image = ((System.Drawing.Image)(resources.GetObject("boton_cerrar.Image")));
            this.boton_cerrar.Location = new System.Drawing.Point(1266, 9);
            this.boton_cerrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.boton_cerrar.Name = "boton_cerrar";
            this.boton_cerrar.Size = new System.Drawing.Size(25, 23);
            this.boton_cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boton_cerrar.TabIndex = 2;
            this.boton_cerrar.TabStop = false;
            this.boton_cerrar.Click += new System.EventHandler(this.Boton_cerrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Sienna;
            this.label1.Location = new System.Drawing.Point(131, 610);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 29);
            this.label1.TabIndex = 37;
            this.label1.Text = "Cargando..";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // Ventana_juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 1102);
            this.Controls.Add(this.panel_contenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(230, 0);
            this.MinimumSize = new System.Drawing.Size(325, 500);
            this.Name = "Ventana_juego";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel_contenedor.ResumeLayout(false);
            this.panel_derecho.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.panel_izquierdo.ResumeLayout(false);
            this.panel_izquierdo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.franja_negra3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoticon_guia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.franja_negra2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.franja_negra4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.franja_negra1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoticon_manual)).EndInit();
            this.Barratitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.boton_restaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_maximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boton_cerrar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_contenedor;
        private System.Windows.Forms.Panel Barratitulo;
        private System.Windows.Forms.PictureBox boton_cerrar;
        private System.Windows.Forms.Panel panel_izquierdo;
        private System.Windows.Forms.Panel panel_derecho;
        private System.Windows.Forms.PictureBox boton_minimizar;
        private System.Windows.Forms.PictureBox boton_restaurar;
        private System.Windows.Forms.PictureBox boton_maximizar;
        private System.Windows.Forms.Button boton_manual_usuario;
        private System.Windows.Forms.PictureBox emoticon_manual;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.PictureBox franja_negra1;
        private System.Windows.Forms.PictureBox franja_negra4;
        private System.Windows.Forms.Label label_recomendacion;
        private System.Windows.Forms.Label label_jugador;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.PictureBox franja_negra3;
        private System.Windows.Forms.PictureBox emoticon_guia;
        private System.Windows.Forms.Button boton_activar_consejos;
        private System.Windows.Forms.Label label_estado;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.PictureBox franja_negra2;
        private System.Windows.Forms.Button resetear;
        private System.Windows.Forms.Label label1;
    }
}