using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ARPlatformer
{
    public class GameScene : DrawableGameComponent
    {
        protected Game1 parent;
        public List<GameComponent> Components { get; set; }
        public GameScene(Game game) : base(game)
        {
            parent = (Game1)game;
            Components = new List<GameComponent>();
            HideScene();
        }

        /// <summary>
        /// Draws the drawable components from active scenes
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent component = null;
            foreach (GameComponent item in Components)
            {
                if(item is DrawableGameComponent)
                {
                    component = (DrawableGameComponent)item;
                    if (component.Visible)
                    {
                        component.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Enables scene
        /// </summary>
        public virtual void ShowScene()
        {
            SetSceneState(true);
        }

        /// <summary>
        /// Hides scene
        /// </summary>
        public virtual void HideScene()
        {
            SetSceneState(false);
        }

        /// <summary>
        /// sets the enable and visible property of a scene
        /// </summary>
        /// <param name="state"></param>
        private void SetSceneState(bool state)
        {
            this.Enabled = state;
            this.Visible = state;

            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent component = (DrawableGameComponent)item;
                    component.Visible = state;
                }
            }
        }
    }
}
