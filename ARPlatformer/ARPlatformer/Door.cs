using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARPlatformer
{
    public class Door : DrawableGameComponent
    {
        //declare variables
        Game1 parent;
        private Texture2D tex;
        private Vector2 position;
        private Rectangle rectangle;
        public Rectangle Rectangle { get { return rectangle; } }
        public Door(Game game, string imageName, Vector2 pos) : base(game)
        {
            this.parent = (Game1)game;
            this.tex = parent.Content.Load<Texture2D>(imageName);
            this.position = pos;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }

        /// <summary>
        /// Draw door based on position passed from playscene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Rectangle srcRectangle = new Rectangle(0,0, tex.Width,tex.Height);
            parent.Sprite.Begin();
            parent.Sprite.Draw(tex, position, srcRectangle, Color.White);
            parent.Sprite.End();
            base.Draw(gameTime);
        }
    }
}
