namespace Poker
{
	using System.ComponentModel;
	using System.Windows.Forms;

	partial class AddChips
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.label1 = new System.Windows.Forms.Label();
            this.addChips_btn = new System.Windows.Forms.Button();
            this.exit_btn = new System.Windows.Forms.Button();
            this.chip_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(48, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "You ran out of chips !";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addChips_btn
            // 
            this.addChips_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addChips_btn.Location = new System.Drawing.Point(12, 226);
            this.addChips_btn.Name = "addChips_btn";
            this.addChips_btn.Size = new System.Drawing.Size(75, 23);
            this.addChips_btn.TabIndex = 1;
            this.addChips_btn.Text = "Add Chips";
            this.addChips_btn.UseVisualStyleBackColor = true;
            this.addChips_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // exit_btn
            // 
            this.exit_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exit_btn.Location = new System.Drawing.Point(197, 226);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(75, 23);
            this.exit_btn.TabIndex = 2;
            this.exit_btn.Text = "Exit";
            this.exit_btn.UseVisualStyleBackColor = true;
            this.exit_btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // chip_txt
            // 
            this.chip_txt.Location = new System.Drawing.Point(91, 229);
            this.chip_txt.Name = "chip_txt";
            this.chip_txt.Size = new System.Drawing.Size(100, 20);
            this.chip_txt.TabIndex = 3;
            // 
            // AddChips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.chip_txt);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.addChips_btn);
            this.Controls.Add(this.label1);
            this.Name = "AddChips";
            this.Text = "You Ran Out Of Chips";
            this.Load += new System.EventHandler(this.AddChips_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion

        private Label label1;
        private Button addChips_btn;
        private Button exit_btn;
        private TextBox chip_txt;
    }
}