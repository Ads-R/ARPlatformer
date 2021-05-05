using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARPlatformer
{
    public class Coin : DrawableGameComponent
    {
        //declare variables
        Game1 parent;
        private Texture2D tex;
        private Vector2 position;
        private Rectangle rectangle;
        private int width;
        private int height;
        private int col = 6;
        private int row = 1;
        private int currentX;
        private int currentY;

        private int timeSince;
        private int fps = 100;

        public Rectangle Rectangle { get { return rectangle; } }

        ///initialize some variables when class is instantiated
        public Coin(Game game, string imageName, Vector2 pos) : base(game)
        {
            parent = (Game1)game;
            this.tex = parent.Content.Load<Texture2D>("Images/Coin");
            this.position = pos;
            width = tex.Width / col;
            height = tex.Height / row;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            currentX = 0;
            currentY = 0;
            timeSince = 0;
        }

        /// <summary>
        /// Draw coin class from the sprite sheet
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Rectangle sourceRectangle = new Rectangle(currentX, currentY, width, height);
            parent.Sprite.Begin();
            parent.Sprite.Draw(tex, position, sourceRectangle, Color.White);
            parent.Sprite.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Animatation for the coin object drawn
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            timeSince += gameTime.ElapsedGameTime.Milliseconds;

            if(currentX >= tex.Width)
            {
                currentX = 0;
            }
            if (timeSince > fps)
            {
                timeSince -= fps;
                currentX += width;
                timeSince = 0;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Disable the coin object drawn
        /// </summary>
        public void DeleteCoin()
        {
            this.Enabled = false;
            this.Visible = false;
        }
    }
}
