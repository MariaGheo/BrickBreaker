using System;
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

            if (type > 0 && type <= GameScreen.colours.Count);
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
                
                if (type < 99) //good powerups
                {
                    if (type == 1) //tnt green
                    {
                        Rectangle tntRec = new Rectangle(GameScreen.ball.x, GameScreen.ball.y, GameScreen.ball.size, GameScreen.ball.size);
                        
                        foreach(Block b in GameScreen.blocks)
                        {
                            Rectangle blockRec = new Rectangle(b.x, b.y, 10, 2);

                            if (tntRec.IntersectsWith(blockRec))
                            {
                                b.hp--;
                            }
                            
                        }
                        
                    }
                    else if (type == 2) //luck/more powerups blue
                    {
                        GameScreen.luckChance = 2;
                        await Task.Delay(10000);
                        GameScreen.luckChance = 0;
                    }
                    
                    else if (type == 3) //strength/double damage red
                    {
                        GameScreen.damage = 2;
                        await Task.Delay(10000);
                        GameScreen.damage = 1;
                    }
                    else if (type == 4) //health potion/ +1 heart orange
                    {
                        GameScreen.lives++;
                    }
                    else if (type == 5) //Slowfall for ball purple
                    {
                        GameScreen.ball.ySpeed = GameScreen.prevYSpeed - 2;
                    }
                    else if (type == 6) //totem of undying (might be hard) yellow
                    {

                    }

                    if (type < 0 || type == 99)
                    {
                        if(type == -1) //speed/fast ball pink
                        {
                            GameScreen.ball.xSpeed = GameScreen.prevXSpeed + 2;
                        }
                        else if(type == -2)//slowness/slow paddle cyan
                        {
                            GameScreen.paddle.speed = GameScreen.paddlePrevSpeed - 2;
                        }
                        else if(type == -3) //harming potion marroon
                        {
                            GameScreen.lives--;
                        }
                        else if(type == -4) //invisibility ball lavender
                        {
                            GameScreen.invisible = true;
                            await Task.Delay(5000);
                            GameScreen.invisible = false;

                        }
                        else if(type == -5) //mining fatigue/no damage gray
                        {
                            GameScreen.damage = 0;
                            await Task.Delay(10000);
                            GameScreen.damage = 1;
                        }
                    }
                }
            type = 0;
            }
        }

    }
}
