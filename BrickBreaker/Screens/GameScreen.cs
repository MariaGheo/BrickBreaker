/*  Created by: me
 *  Project: Brick Breaker
 *  Date: 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Xml;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown, spaceDown;

        // Game values
        int lives;
        int level;

        // Paddle and Ball objects
        Paddle paddle;
        Ball ball;

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Red);

        List<PictureBox> livesList = new List<PictureBox>();
        //List<Image> livesImage = new List<Image>();

        Image backgroundImage = Properties.Resources.goodbackground;

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void cam()
        {

        }

        public void OnStart()
        {
            NathanielOnStart();

            //set life counter
            lives = 2;

            //set level tracker
            level = 1;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 8;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.White);

            // setup starting ball values
            int ballX = this.Width / 2 - 10;
            int ballY = this.Height - paddle.height - 80;

            // Creates a new ball
            int xSpeed = 6;
            int ySpeed = 6;
            int ballSize = 20;
            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);

            KianOnStart();

            //XMLReader code: we will use this once the first level has been built
            MariaOnStart();

            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            KianGameTimer();
            //test
            cam();

            //KianMethod(); //Test Pull Request Kian

            // Move the paddle
            if (leftArrowDown && paddle.x > 0)
            {
                paddle.Move("left");
            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");
            }
            // Move ball
            ball.Move();

            // Check for collision with top and side walls
            ball.WallCollision(this);

            // Check for ball hitting bottom of screen
            if (ball.BottomCollision(this))
            {
                livesList[lives].Image = null;
                livesList.RemoveAt(lives);
                lives--;

                // Moves the ball back to origin
                ball.x = ((paddle.x - (ball.size / 2)) + (paddle.width / 2));
                ball.y = (this.Height - paddle.height) - 85;

                if (lives == -1)
                {
                    gameTimer.Enabled = false;
                    OnEnd();
                }
            }
            // Check for collision of ball with paddle, (incl. paddle movement)
            ball.PaddleCollision(paddle);

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b))
                {
                    blocks.Remove(b);

                    if (blocks.Count == 0)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                    }
                    break;
                }
            }
            //redraw the screen
            Refresh();
        }

        public void OnEnd()
        {
            // Goes to the game over screen
            Form form = this.FindForm();
            MenuScreen ps = new MenuScreen();

            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
           e.Graphics.DrawImageUnscaled(backgroundImage, 0, 0);
            // Draws paddle
            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(paddleBrush, paddle.x, paddle.y, paddle.width, paddle.height);

            // Draws blocks
            foreach (Block b in blocks)
            {
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
                
            }

            // Draws ball
            e.Graphics.FillRectangle(ballBrush, ball.x, ball.y, ball.size, ball.size);

            

           //FOR NOT USING PICTURE BOXES
            //e.Graphics.DrawImage(livesImage[0],14, 740);
            //e.Graphics.DrawImage(livesImage[1], 64, 740);
            //e.Graphics.DrawImage(livesImage[2], 114, 740);

   
        }

        public void KianOnStart()
        {
            // ball launch - Kian
            ball.canMove = false;
        }

        public void KianGameTimer()
        {
            // Move ball to paddle when it needs to stick to it
            if (ball.canMove == false)
            {
                ball.x = paddle.x + (paddle.width / 2) - (ball.size / 2);
                ball.y = paddle.y - paddle.height;
            }
            if (spaceDown == true) //launch ball
            {
                ball.canMove = true;
            }
            
        }
        
        public void Noah()
        {
            Random randGen = new Random();
            int chance = randGen.Next(1, 4);
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b) && chance == 2)
                {
                    //Powerup.SpawnUp(chance);
                }
            }
        }

        public void MariaOnStart()
        {
            XmlReader reader = XmlReader.Create($"Resources/level{level}.xml");

            while (reader.Read())
            {
                int x, y, hp;
                Image image = null;
                string test;

                reader.ReadToFollowing("X");
                test = reader.ReadString();

                if (test == "")
                {
                    break;
                }

                x = Convert.ToInt32(test);

                reader.ReadToNextSibling("Y");
                y = Convert.ToInt32(reader.ReadString());

                reader.ReadToNextSibling("HP");
                hp = Convert.ToInt32(reader.ReadString());

                switch (hp)
                {
                    case 1:
                        //image = 
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 10:
                        break;
                }

                blocks.Add(new Block(x, y, hp, image));
            }

            reader.Close();
        }

        public void NathanielOnStart()
        {
            livesList.Add(livesBox1);
            livesList.Add(livesBox2);
            livesList.Add(livesBox3);

            foreach (PictureBox l in livesList)
            {
                l.Image = Properties.Resources.minecraftHeart;
            }

            //For making the lives an image, Not in use at the minute
            //livesImage.Add(Properties.Resources.minecraftHeart);
            //livesImage.Add(Properties.Resources.minecraftHeart);
            //livesImage.Add(Properties.Resources.minecraftHeart);

        }
    }
}
