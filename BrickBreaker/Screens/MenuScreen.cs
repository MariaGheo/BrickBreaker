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
        public static string test;


        public MenuScreen()
        {
            InitializeComponent();

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
      

        }
        // THIS IS ALL FOR ROTTATING THE LABEL AND CUTTENTLY IS NOT WORKINBG

        private void subtitleLabel_Paint(object sender, PaintEventArgs e)
        {
           // //messageSet();
           // Font myfont = new Font("Arial", 34);
           // //Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
           // //e.Graphics.TranslateTransform(30, 20);
           // //e.Graphics.RotateTransform(45);
           // //e.Graphics.DrawString(test, myfont, mybrush, 50, 50);

           // e.Graphics.TranslateTransform(100, 100);
     
           //e.Graphics.RotateTransform(90);

           // e.Graphics.DrawString("minecraft", Font, Brushes.Black, 50, 50);

        }

        private void testLabel_Click(object sender, EventArgs e)
        {

        }

        private void testLabel_Paint(object sender, PaintEventArgs e)
        {
            //messageSet();
            //Font myfont = new Font("Arial", 34);
            //Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
            //e.Graphics.TranslateTransform(30, 20);
            //e.Graphics.RotateTransform(45);
            //e.Graphics.DrawString("hello", myfont, mybrush, 50, 50);
        }
    }
}
