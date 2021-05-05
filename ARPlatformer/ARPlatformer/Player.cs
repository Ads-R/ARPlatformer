using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



/*Credits to Oyyou for the Tile Collision, Gravity and Jump Response solutions */
namespace ARPlatformer
{
    public class Player : DrawableGameComponent
    {
        Game1 parent;
        private Texture2D tex;
        private Vector2 speed;
        private Vector2 position;
        public Vector2 Position { get { return position; } }
        private Vector2 stage;
        private Rectangle rectangle;
        private int width;
        private int height;
        private int score;
        public int Score { get { return score; } }
        private int row = 2;
        private int col = 8;
        private int currentX;
        private int currentY;
        private bool hasJumped;

        //slowdown animation
        private int timeSince = 0;
        private int fps = 100;
        
        //sound effect
        private SoundEffect runSound;
        private SoundEffect jumpSound;
        private SoundEffect coinEffect;
        //sound effect instance
        private SoundEffectInstance runInstance;
        private SoundEffectInstance jumpInstance;
        private SoundEffectInstance coinEffectInstance;
        public Player(Game game, string imageName, Vector2 stage, string runName, string jumpName) : base(game)
        {
            this.parent = (Game1)game;
            this.tex = parent.Content.Load<Texture2D>(imageName);
            this.position = new Vector2(50, 600);
            this.stage = stage;
            this.width = tex.Width / col;
            this.height = tex.Height / row;
            currentX = 0;
            currentY = 0;
            score = 0;

            runSound = parent.Content.Load<SoundEffect>(runName);
            runInstance = runSound.CreateInstance();
            jumpSound = parent.Content.Load<SoundEffect>(jumpName);
            jumpInstance = jumpSound.CreateInstance();
            coinEffect = parent.Content.Load<SoundEffect>("Sounds/CoinEffect");
            coinEffectInstance = coinEffect.CreateInstance();
            hasJumped = false;
        }

        /// <summary>
        /// Draws the player object from the sprite sheet
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Rectangle sourceRectangle = new Rectangle(currentX, currentY, width, height);
            parent.Sprite.Begin();
            parent.Sprite.Draw(tex,position,sourceRectangle ,Color.White);
            parent.Sprite.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks for player input, position, collision and updates animation
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //update position based on speed
            position += speed;
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            KeyBoardInput(gameTime);

            //player gravity
            if (speed.Y < 10)
            {
                speed.Y += 0.4f;
            }

            base.Update(gameTime);
        }


        private void KeyBoardInput(GameTime gameTime)
        {
            timeSince += gameTime.ElapsedGameTime.Milliseconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //move right
                speed.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                //check if soundeffect is stil playing
                if (runInstance.State != SoundState.Playing)
                {
                    runInstance.Play();
                }
                if(currentX >= tex.Width)
                {
                    currentX = 0;
                }
                //player animation
                if (timeSince > fps)
                {
                    timeSince -= fps;
                    currentX += width;
                    currentY = 0;
                    timeSince = 0;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //move left
                speed.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                //check if soundeffect is stil playing
                if (runInstance.State != SoundState.Playing)
                {
                    runInstance.Play();
                }
                if (currentX >= tex.Width)
                {
                    currentX = 0;
                }
                //player animation
                if (timeSince > fps)
                {
                    timeSince -= fps;
                    currentX += width;
                    currentY = height;
                    timeSince = 0;
                }
            }
            else
            {
                speed.X = 0f;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped==false)
            {
                if (jumpInstance.State != SoundState.Playing)
                {
                    jumpInstance.Play();
                }
                //player moves up if has not jumped yet, and slowly goes down
                position.Y -= 6f;
                speed.Y = -9f;
                hasJumped = true;
            }
        }

        /// <summary>
        /// Checks for player collision with tiles and makes appropriate response, which bounces the player off if it detects collision from bottom
        /// and blocks if colliding from top right and left to avoid overlaying of graphics.
        /// Uses the static class CollisionDetection
        /// </summary>
        /// <param name="tileRectangle">Rectangle property of tile</param>
        public void Collision(Rectangle tileRectangle)
        {
            if (rectangle.TopCollision(tileRectangle))
            {
                rectangle.Y = tileRectangle.Y - rectangle.Height;
                speed.Y = 0f;
                hasJumped = false;
            }
            if (rectangle.LeftCollision(tileRectangle))
            {
                position.X = tileRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.RightCollision(tileRectangle))
            {
                position.X = tileRectangle.X + tileRectangle.Width + 2;
            }
            if (rectangle.BottomCollision(tileRectangle))
                speed.Y = 1f;

            //Limit movement to left side end of the screen
            if (position.X < 0) position.X = 0;
            //Limit movement to right side end of the screen
            if (position.X+ this.width >= stage.X) position.X = stage.X - this.width;
            //Limit movement to top of the screen
            if (position.Y < 0) speed.Y = 1f;
        }

        /// <summary>
        /// Checks for collision between player and coin, if collision is true, adds score and removes the coin
        /// Uses the static class CollisionDetection
        /// </summary>
        /// <param name="coinRectangle">The rectangle property of coin</param>
        /// <param name="coin">The instance of the coin</param>
        public void CoinGain(Rectangle coinRectangle, Coin coin)
        {
            if (rectangle.CoinCollision(coinRectangle))
            {
                if (coin.Enabled)
                {
                    coinEffectInstance.Play();
                    score += 10;
                }
                coin.DeleteCoin();
            }
        }

        /// <summary>
        /// Checks for collision between player and door
        /// </summary>
        /// <param name="door">Rectangle property of door</param>
        /// <returns>boolean</returns>
        public bool DoorGrab(Rectangle door)
        {
            if (rectangle.DoorCollision(door))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
