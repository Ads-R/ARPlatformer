using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARPlatformer
{
    public class Tile : DrawableGameComponent
    {
        Game1 parent;
        private Texture2D tex;
        private Vector2 stage;
        private Vector2 position;
        private Rectangle rectangle;
        public Rectangle Rectangle { get { return rectangle; } }
        private Rectangle srcRect;

        public Tile(Game game, string imageName, Vector2 stage, Vector2 pos) : base(game)
        {
            this.parent = (Game1)game;
            this.tex = parent.Content.Load<Texture2D>(imageName);
            this.stage = stage;
            this.position = pos;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
            this.srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
        }
        /// <summary>
        /// Draw the tile from the content
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            parent.Sprite.Begin();
            parent.Sprite.Draw(tex,position,srcRect,Color.White);
            parent.Sprite.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
