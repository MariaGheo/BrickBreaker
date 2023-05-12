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
using System.Runtime.CompilerServices;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global 

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown, spaceDown;
        public static bool invisible;
        public static bool explode;
       
        public static int score;

        // Game values
        public static int lives;

        // Paddle and Ball objects
        public static Paddle paddle;
        public static Ball ball;
        public static int luckChance;
        public static int prevYSpeed;
        public static int prevXSpeed;
        public static int paddlePrevSpeed;
        public static int damage = 1;

        // list of all blocks for current level
        public static List<Block> blocks = new List<Block>();
        List<Powerup> powerups = new List<Powerup>();

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Red);
        public static SolidBrush invisBrush = new SolidBrush(Color.Transparent);

        public static List<PictureBox> livesList = new List<PictureBox>();
        public static List<Color> colours = new List<Color> { Color.Red, Color.Green, Color.Orange, Color.Pink, Color.Cyan, Color.Blue, Color.Gray, Color.Purple, Color.White, Color.SlateGray };

        // We will have a list of rotating images, Each time we change level we can pull a new image
        List<Image> backgroundImages = new List<Image>();

        Image plainsBackground = Properties.Resources.goodBackground;



        #endregion

        public GameScreen()
        {
            InitializeComponent();
            Form1.size = 16;
            Form1.FontChange();
            OnStart();
        }

        public void cam()
        {
            scoreOutput.Text = $"Score: {score}";
        }

        public void OnStart()
        {
            scoreOutput.Font = Form1.myFont;
            NathanielOnStart();

            // Reset score
            score = 0;

            //set life counter
            lives = 2;

            //set level tracker
            //level = 1;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 8;
            paddlePrevSpeed = paddleSpeed;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.White);

            // setup starting ball values
            int ballX = this.Width / 2 - 10;
            int ballY = this.Height - paddle.height - 80;

            // Creates a new ball
            int xSpeed = 6;
            int ySpeed = 6;
            prevYSpeed = ySpeed;
            prevXSpeed = xSpeed;
            int ballSize = 20;
            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);

            KianOnStart();

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

            if(lives > 3) //Cap lives at 3
            {
                lives = 3;
            }

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
                //livesList.RemoveAt(lives);
                lives--;

                // Moves the ball back to origin
                ball.x = ((paddle.x - (ball.size / 2)) + (paddle.width / 2));
                ball.y = (this.Height - paddle.height) - 85;
                ball.canMove = false; //stop ball each time it resets position

                if (lives == -1)
                {
                    gameTimer.Enabled = false;
                    NathanielGameOver();
                }
            }

            // Check for collision of ball with paddle, (incl. paddle movement)
            ball.PaddleCollision(paddle);

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {

                    if (b.hp <= 0)
                    {
                        blocks.Remove(b);
                        score += b.points;
                        Kian(b);
                    break;
                    }

                    if (blocks.Count == 0)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                    }
                if (ball.BlockCollision(b))
                {

                    b.hp -= damage;
                    if(explode == true)
                    {
                        Rectangle tntRec = new Rectangle(ball.x - 50, ball.y - 50, 100, 100);

                        foreach (Block block in blocks)
                        {
                            Rectangle blockRec = new Rectangle(block.x, block.y, block.width, block.height);

                            if (tntRec.IntersectsWith(blockRec))
                            {
                                block.hp--;
                            }

                        }
                        explode = false;
                       
                    }
                    break;
                }
            }

            KianPowerupEngine();

            //redraw the screen
            Refresh();
        }
        public void SetScore()
        {

            string highScore = score.ToString();
            int intScore = Convert.ToInt32(highScore);

            HighScore newScore = new HighScore("Player", intScore);
            MenuScreen.highScores.Add(newScore);

            XmlWriter writer = XmlWriter.Create("HighScoreXML.xml", null);
            writer.WriteStartElement("HighScore");

            foreach (HighScore s in MenuScreen.highScores)
            {
                writer.WriteElementString("Name", s.name);
                writer.WriteElementString("Score", s.score.ToString());
            }
            writer.WriteEndElement();
            writer.Close();



        }
        public void OnEnd()
        {

            SetScore();

            Form1.level++;

            // Goes to the game over screen
            Form form = this.FindForm();
            TransitionScreen ps = new TransitionScreen();

            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }

        public void NathanielGameOver()
        {
            Form1.totalScore = score;

            Form form = this.FindForm();
            GameOverScreen ps = new GameOverScreen();

            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(backgroundImages[Form1.level - 1], 0, 0);

            // Draws paddle
            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(paddleBrush, paddle.x, paddle.y, paddle.width, paddle.height);

            // Draws blocks
            foreach (Block b in blocks)
            {
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);

                if (b.image != null)
                {
                    e.Graphics.DrawImage(b.image, b.x, b.y, 80, 30);
                }
            }

            // Draws ball
            if (invisible == true)
            {
                e.Graphics.FillEllipse(invisBrush, ball.x, ball.y, ball.size, ball.size);
            }
            else
            {
                e.Graphics.FillEllipse(ballBrush, ball.x, ball.y, ball.size, ball.size);
            }

            // Draws PowerUps
            foreach (Powerup p in powerups)
            {
                e.Graphics.FillRectangle(p.powerUpBrush, p.x, p.y, 12, 25);
            }
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
            if (spaceDown) //launch ball
            {
                ball.canMove = true;
            }

        }

        public void Kian(Block b)
        {
            Random randGen = new Random();
            int chance = randGen.Next(1, 6);
            if (chance <= 1 + luckChance)
            {
                chance = randGen.Next(1, colours.Count + 1);
                Powerup newPowerup = new Powerup(b.x, b.y, chance);
                powerups.Add(newPowerup);
            }

        }

        public void KianPowerupEngine()
        {
            for (int i = 0; i < powerups.Count; i++)
            {
                powerups[i].Move(powerups[i].y, powerups[i].height);
                powerups[i].PowerupCollision(paddle);

                if (powerups[i].type == 0)
                {
                    powerups.Remove(powerups[i]);

                }
            }
        }

    

        public void MariaOnStart()
        {
            XmlReader reader = XmlReader.Create($"Resources/level{Form1.level}.xml");

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
                        image = Properties.Resources.dirtBlock;
                        break;
                    case 2:
                        image = Properties.Resources.stoneBlock;
                        break;
                    case 3:
                        image = Properties.Resources.netherrackBlock;
                        break;
                    case 4:
                        image = Properties.Resources.ironBlock;
                        break;
                    case 5:
                        image = Properties.Resources.diamondBlock;
                        break;
                    case 10:
                        image = Properties.Resources.obsidianBlock;
                        break;
                }

                blocks.Add(new Block(x, y, hp, image));
            }

            reader.Close();

            backgroundImages.Add(plainsBackground);
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
        }
    }
}
