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
            Form1.size = 16;

            Form1.FontChange();
            Form1.level = 1;

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
            TransitionScreen gs = new TransitionScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }

        public void messageSet()
        {
            messageList.Add("Now in Colour!");
            messageList.Add("Watch out for AI");
            messageList.Add("Java > Bedrock");
            messageList.Add("If your reading this its too late");
            messageList.Add(":)");
            messageList.Add("Creeper gonna creep");
            messageList.Add("New and improved!");
            messageList.Add("Take back the night!");
            messageList.Add("Dinnerbone?");
            messageList.Add("I Hardly Know Her!");



            Random rnd = new Random();
            int random = rnd.Next(0, 10);

            subtitleLabel.Text = $"{messageList[random]}\n";
        }

        private void MenuScreen_Load(object sender, EventArgs e)
        {
            messageSet();

        }

        private void instructionsButton_Click(object sender, EventArgs e)
        {
            InstructionsScreen ts = new InstructionsScreen();
            Form form = this.FindForm();

            form.Controls.Add(ts);
            form.Controls.Remove(this);

            ts.Location = new Point((form.Width - ts.Width) / 2, (form.Height - ts.Height) / 2);

            // this needs to be fixed ?
        }
    }
}
