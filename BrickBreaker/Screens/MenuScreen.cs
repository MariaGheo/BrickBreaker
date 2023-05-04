using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker
{
    public partial class MenuScreen : UserControl
    {
        List<string> messageList = new List<string>();

        public MenuScreen()
        {
            InitializeComponent();

            playButton.Font = Form1.myFont;
            hardButton.Font = Form1.myFont;
            exitButton.Font = Form1.myFont;
            instructionsButton.Font = Form1.myFont;
            highscoreButton.Font = Form1.myFont;
            subtitleLabel.Font = Form1.myFont;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Goes to the game screen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }

        public void messageSet()
        {
            messageList.Add("Now in Colour!");
            messageList.Add("watch out for AI");
            messageList.Add(" Java > Bedrock");
            messageList.Add("If your reading this its too late");
            messageList.Add("Bradyens an opp");
            messageList.Add("Creeper gonna creep");

            Random rnd = new Random();
            int random = rnd.Next(0, 6);

            subtitleLabel.Text = $"{messageList[random]}\n";
        }

        private void MenuScreen_Load(object sender, EventArgs e)
        {
            messageSet();

        }

        private void instructionsButton_Click(object sender, EventArgs e)
        {

            TransitionScreen ts = new TransitionScreen();
            Form form = this.FindForm();

            form.Controls.Add(ts);
            form.Controls.Remove(this);

            ts.Location = new Point((form.Width - ts.Width) / 2, (form.Height - ts.Height) / 2);

        }
    }
}
