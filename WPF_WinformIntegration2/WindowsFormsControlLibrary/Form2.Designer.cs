namespace WindowsFormsControlLibrary
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
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 239 , 241 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 33 , 12 );
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 12 , 9 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 33 , 12 );
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.Color.Blue;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point( 0 , 100 );
            this.panel2.Margin = new System.Windows.Forms.Padding( 0 );
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size( 284 , 162 );
            this.panel2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point( 0 , 0 );
            this.panel1.Margin = new System.Windows.Forms.Padding( 0 );
            this.panel1.MinimumSize = new System.Drawing.Size( 0 , 100 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 284 , 100 );
            this.panel1.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size( 284 , 262 );
            this.Controls.Add( this.panel2 );
            this.Controls.Add( this.panel1 );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.label1 );
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;

    }
}