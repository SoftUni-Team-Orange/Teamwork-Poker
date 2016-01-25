namespace Poker
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	public partial class AddChips : Form
    {
        public int currentChips;
        public AddChips()
        {
            FontFamily fontFamily = new FontFamily("Arial");
            InitializeComponent();
            ControlBox = false;
            label1.BorderStyle = BorderStyle.FixedSingle;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (int.Parse(chip_txt.Text) > 100000000)
            {
                MessageBox.Show("The maximium chips you can add is 100000000");
                return;
            }

            if (!int.TryParse(chip_txt.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
            }
            else if (int.TryParse(chip_txt.Text, out parsedValue) && int.Parse(chip_txt.Text) <= 100000000)
            {
                currentChips = int.Parse(chip_txt.Text);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var message = "Are you sure?";
            var title = "Quit";
            var result = MessageBox.Show(
            message, title, 
            MessageBoxButtons.YesNo, 
            MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    Application.Exit();
                    break;
            }
        }

        private void AddChips_Load(object sender, EventArgs e)
        {

        }
    }
}
