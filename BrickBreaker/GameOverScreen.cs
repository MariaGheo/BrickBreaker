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
    public partial class GameOverScreen : UserControl
    {
        public GameOverScreen()
        {// Make background the last game screen background with a red filter?
            InitializeComponent();

          //  if (GameScreen.lives )
            
          
            subtitleLabel.Font = Form1.myFont;
            scoreLabel.Font = Form1.myFont;
            scoreValue.Font = Form1.myFont;
            exitButton .Font = Form1.myFont;


            Form1.size = 30;
        
            Form1.FontChange();

            titleLabel.Font = Form1.myFont;

            // Lets use "total score" to add to the xml highscores, we will need a high score screen
          //  scoreValue.Text = ($"{score}");
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
