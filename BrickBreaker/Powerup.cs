using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Threading.Tasks;

namespace BrickBreaker
{
    internal class Powerup
    {

        public int height = 25;
        public int width = 12;
        public int speed = 3;

        public int x, y, type;

        public SolidBrush powerUpBrush = new SolidBrush(Color.White);
      

      

        Random randGen = new Random();
         /* Kian Powerups
          * Good Powerups 
         1 - TNT
         2 - Luck 
         3 - Strength
         4 - Health Potion
         5 - Slowfall
         6 - Totem of Undying
         Bad Powerups
         -1 - Speed
         -2 - Slowness
         -3 - Harming potion
         -4 - Invisibility
         -5 - Mining Fatigue
         
        Random
        99 - Suspicious Stew
          * */
        public Powerup(int x_, int y_, int _type)
        {
            x = x_;
            y = y_;
            type = _type;

            //if (type > 0 && type <= GameScreen.colours.Count)
            powerUpBrush.Color = GameScreen.colours[type - 1];

        }

        public void Move(int y_, int height_)
        {
            PowerupCollision(GameScreen.paddle);
            height = height_;
            y += speed;

            if (y > 13)
            {
                speed *= +1;
            }
         

        }

        public async void PowerupCollision(Paddle p)
        {
            Rectangle paddleRec = new Rectangle(p.x, p.y, p.width, p.height);
            Rectangle powerRec = new Rectangle(x, y, 12, 25);
            if (powerRec.IntersectsWith(paddleRec))
            {
                x = 10000; //Move Powerup off screen till timer is done, gives illusion of breaking
                type = 5;
               
                    if (type == 1) //tnt Red Doesn't work
                    {
                        Rectangle tntRec = new Rectangle(GameScreen.ball.x, GameScreen.ball.y, GameScreen.ball.size, GameScreen.ball.size);
                        
                        foreach(Block b in GameScreen.blocks)
                        {
                            Rectangle blockRec = new Rectangle(b.x, b.y, 50, 10);

                            if (tntRec.IntersectsWith(blockRec))
                            {
                                b.hp--;
                            }
                            
                        }
                        
                    }
                    else if (type == 2) //luck/more powerups Green
                    {
                        GameScreen.luckChance = 2;
                        await Task.Delay(10000);
                        GameScreen.luckChance = 0;
                    }
                    
                    else if (type == 3) //strength/double damage orange
                    {
                        GameScreen.damage = 2;
                        await Task.Delay(10000);
                        GameScreen.damage = 1;
                    }
                    else if (type == 4) //health potion/ +1 heart Pink
                    {
                    
                    GameScreen.livesList[GameScreen.lives + 1].Image = Properties.Resources.minecraftHeart;
                    GameScreen.lives++;
                    }
                    else if (type == 5) //Slowfall for ball Cyan VERY BROKEN
                    {
                    if(GameScreen.ball.ySpeed > 0)
                    {
                        GameScreen.ball.ySpeed = GameScreen.prevYSpeed - 2;
                        await Task.Delay(5000);
                        
                    }
                    else
                    {
                        GameScreen.ball.ySpeed = -GameScreen.prevYSpeed + 2;
                        await Task.Delay(5000);
                        
                    }
                    if(GameScreen.ball.ySpeed > 0)
                    {
                        GameScreen.ball.ySpeed = GameScreen.prevYSpeed;
                    }
                    else
                    {
                        GameScreen.ball.ySpeed = -GameScreen.prevYSpeed;
                    }
                    }
                    
                    else if (type == 6) //totem of undying (might be hard) yellow Broken
                    {
                        GameScreen.undying = true;
                        await Task.Delay(1000);
                        GameScreen.undying = false;
                        
                    }

                    
                        if(type == 7) //speed/fast ball Blue
                        {
                            GameScreen.ball.xSpeed = GameScreen.prevXSpeed + 2;
                            await Task.Delay(5000);
                            GameScreen.ball.xSpeed = GameScreen.prevXSpeed;
                        }
                        else if(type == 8)//slowness/slow paddle Gray
                        {
                            GameScreen.paddle.speed = GameScreen.paddlePrevSpeed - 2;
                            await Task.Delay(5000);
                            GameScreen.paddle.speed = GameScreen.paddlePrevSpeed;
                        }
                        else if(type == 9) //harming potion Purple
                        {
                        GameScreen.livesList[GameScreen.lives].Image = null;
                        GameScreen.lives--;
                        }
                        else if(type == 10) //invisibility ball White
                        {
                            GameScreen.invisible = true;
                            await Task.Delay(5000);
                            GameScreen.invisible = false;

                        }
                        else if(type == 11) //mining fatigue/no damage slate gray
                        {
                            GameScreen.damage = 0;
                            await Task.Delay(10000);
                            GameScreen.damage = 1;
                        }
                        
                        
                    
                
            type = 0;
            }
        }

    }
}
