using System.Drawing;

namespace Aquarium
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.AnT = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.загрузить3DМодельToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьaseФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnT
            // 
            this.AnT.AccumBits = ((byte)(0));
            this.AnT.AutoCheckErrors = false;
            this.AnT.AutoFinish = false;
            this.AnT.AutoMakeCurrent = true;
            this.AnT.AutoSwapBuffers = true;
            this.AnT.BackColor = System.Drawing.Color.Black;
            this.AnT.ColorBits = ((byte)(32));
            this.AnT.DepthBits = ((byte)(16));
            this.AnT.Location = new System.Drawing.Point(30, 42);
            this.AnT.Name = "AnT";
            this.AnT.Size = new System.Drawing.Size(800, 600);
            this.AnT.StencilBits = ((byte)(0));
            this.AnT.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(873, 46);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(165, 20);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Включить wire режим";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // RenderTimer
            // 
            this.RenderTimer.Interval = 30;
            this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузить3DМодельToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1480, 28);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // загрузить3DМодельToolStripMenuItem
            // 
            this.загрузить3DМодельToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьaseФайлToolStripMenuItem});
            this.загрузить3DМодельToolStripMenuItem.Name = "загрузить3DМодельToolStripMenuItem";
            this.загрузить3DМодельToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.загрузить3DМодельToolStripMenuItem.Text = "Загрузить 3D модель";
            // 
            // выбратьaseФайлToolStripMenuItem
            // 
            this.выбратьaseФайлToolStripMenuItem.Name = "выбратьaseФайлToolStripMenuItem";
            this.выбратьaseФайлToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.выбратьaseФайлToolStripMenuItem.Text = "Выбрать .ase файл";
            this.выбратьaseФайлToolStripMenuItem.Click += new System.EventHandler(this.выбратьaseФайлToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(870, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(873, 136);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(195, 20);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "Включить авто-плавание";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(873, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "label2";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(876, 253);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(95, 20);
            this.checkBox3.TabIndex = 12;
            this.checkBox3.Text = "Добавить тело вращения Лагранжа";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(876, 309);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(95, 20);
            this.checkBox4.TabIndex = 13;
            this.checkBox4.Text = "Покормить рыбок";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 720);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.AnT);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "3D визуализация";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form2_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem загрузить3DМодельToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьaseФайлToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}
