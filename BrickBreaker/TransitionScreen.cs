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
    public partial class TransitionScreen : UserControl
    {
        int check;

        List <string> tranistionMessages = new List <string> ();    

        public TransitionScreen()
        {
            InitializeComponent();
            messageSet();
      
           
            subtitleLabel.Font = Form1.myFont;
            playButton.Font = Form1.myFont;
            exitButton.Font = Form1.myFont;

            Form1.size = 20;
            Form1.FontChange();

            messageLabel.Font = Form1.myFont;

            check = 0;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            check++;
            messageLabel.Text = "Are you sure you want to exit?";

            if (check > 1)

            {
                MenuScreen gs = new MenuScreen();
                Form form = this.FindForm();

                form.Controls.Add(gs);
                form.Controls.Remove(this);

                gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);


            }

        }

        public void messageSet()
        {
            tranistionMessages.Add("Level 1: Taking Inventory"); //Overworld plains

            tranistionMessages.Add("Level 2: Getting an Upgrade");

            tranistionMessages.Add("Level 3: Hot Topic");

            tranistionMessages.Add("Level 4: Time to mine!"); // In the caves mining

            tranistionMessages.Add("Level 5: Acquire Hardware");

            tranistionMessages.Add("Level 6: Into the Nether "); // Go to the neather

            tranistionMessages.Add("Level 7: Into fire"); 

            tranistionMessages.Add("Level 8: Hot tourist destination"); // Go to the forest

            tranistionMessages.Add("Level 9: Beam Me Up");

            tranistionMessages.Add("Level 10: Smelt Everything!"); 

            tranistionMessages.Add("Level 11: The End?"); // the end?




            messageLabel.Text = $"{tranistionMessages[Form1.level - 1]}\n";
         
        }
    }
}
