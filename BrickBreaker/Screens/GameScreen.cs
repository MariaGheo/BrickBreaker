/*  Created by: 
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

        #endregion
       // SolidBrush darkRedBrush = new SolidBrush(Color.DarkRed);

        List<PictureBox> livesList = new List<PictureBox>();  
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
            livesList.Add(livesBox1);
            livesList.Add(livesBox2);
            livesList.Add(livesBox3);

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

            //ball launch - Kian
            ball.canMove = false;

            ////#region Creates blocks for generic level. Need to replace with code that loads levels.

            //TODO - replace all the code in this region eventually with code that loads levels from xml files

            blocks.Clear();
            int x = 10;

            while (blocks.Count < 13)
            {
                x += 87;
                Block b1 = new Block(x, 10, 1, Color.White);
                blocks.Add(b1);
            }

            //#endregion

            //XMLReader code: we will use this once the first level has been built
            //XmlReader reader = XmlReader.Create("Resources/brickTest.xml");

            //while (reader.Read())
            //{
            //    int x, y, hp;
            //    Color color = Color.DarkRed;
            //    string test;

            //    reader.ReadToFollowing("X");
            //    test = reader.ReadString();

            //    if(test == "")
            //    {
            //        break;
            //    }

            //    x = Convert.ToInt32(test);

            //    reader.ReadToNextSibling("Y");
            //    y = Convert.ToInt32(reader.ReadString());

            //    reader.ReadToNextSibling("HP");
            //    hp = Convert.ToInt32(reader.ReadString());

            //    blocks.Add(new Block(x, y, hp, color));
            //}

            //reader.Close();

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

            //test
            cam();

            Nathaniel();


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
                Refresh();
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


        public void Nathaniel()
        {
          foreach(PictureBox l in livesList)
            {
                l.Image = Properties.Resources.minecraftHeart;
            }


        }

    }
}
