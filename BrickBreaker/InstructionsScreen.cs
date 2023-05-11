using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker
{
    public partial class InstructionsScreen : UserControl
    {
        public InstructionsScreen()
        {
            InitializeComponent();

            
            exitButton.Font = Form1.myFont;
            Form1.size = 24;
            Form1.FontChange();
            instructionsLabel.Font = Form1.myFont;


            instructionsLabel.Text = $" 1. Use the left and right arrow keys to move across the screen \n 2. Press space to start your ball, Once its in motion it will not stop! \n 3. destroy all the brick to beat the level or die trying, dont worry you have three lives \n 4. Watch out for falling powers they will help!!! or possibilly harm";

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            MenuScreen gs = new MenuScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }
    }
}
