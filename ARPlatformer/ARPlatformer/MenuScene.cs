using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ARPlatformer
{
    public class MenuScene : GameScene
    {
        private SpriteFont standardFont;
        private Texture2D title;
        private Texture2D background;
        private Color standardColor = Color.Black;
        private Color hoverColor = Color.Blue;

        private List<string> menuItems;
        public int selectedIndex;

        private Vector2 titlePosition;
        private Vector2 position;

        private KeyboardState oldState;

        private SoundEffect rollOver;
        private SoundEffectInstance rollOverInstance;


        public MenuScene(Game game, List<string> menuItems) : base(game)
        {
            this.background = parent.Content.Load<Texture2D>("Images/Background_Main_Menu");
            this.title = parent.Content.Load<Texture2D>("Images/Title");
            this.standardFont = parent.Content.Load<SpriteFont>("Fonts/StandardFont");
            this.menuItems = menuItems;
            titlePosition = new Vector2(parent.Stage.X / 2 - title.Width/2, parent.Stage.Y / 4);
            position = new Vector2(parent.Stage.X / 2 - standardFont.Texture.Width/2, parent.Stage.Y / 2);
            this.rollOver = parent.Content.Load<SoundEffect>("Sounds/Rollover");
            this.rollOverInstance = rollOver.CreateInstance();

        }

        /// <summary>
        /// Draws the title, background and iterates through a list of strings which contains the names of scenes
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 temporaryPos = position;
            parent.Sprite.Begin();
            Rectangle srcRectangleBackground = new Rectangle(0, 0, background.Width, background.Height);
            Rectangle srcRectangleTitle = new Rectangle(0,0, title.Width, title.Height);
            parent.Sprite.Draw(background, srcRectangleBackground, Color.White);
            parent.Sprite.Draw(title, titlePosition, srcRectangleTitle, Color.White);

            for (int i =0; i<menuItems.Count; i++)
            {
                if(selectedIndex == i)
                {
                    parent.Sprite.DrawString(standardFont, menuItems[i], temporaryPos, hoverColor);
                    temporaryPos.Y += standardFont.LineSpacing;
                }
                else
                {
                    parent.Sprite.DrawString(standardFont, menuItems[i], temporaryPos, standardColor);
                    temporaryPos.Y += standardFont.LineSpacing;
                }
            }
            parent.Sprite.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks for inputs and updates the string colors to highlight the currently selected option
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {


            KeyboardState ks = Keyboard.GetState();
            if(oldState.IsKeyUp(Keys.Down) && ks.IsKeyDown(Keys.Down))
            {
                rollOverInstance.Play();
                selectedIndex = MathHelper.Clamp(selectedIndex + 1, 0, menuItems.Count -1);
            }
            if (oldState.IsKeyDown(Keys.Up) && ks.IsKeyUp(Keys.Up))
            {
                rollOverInstance.Play();
                selectedIndex = MathHelper.Clamp(selectedIndex - 1, 0, menuItems.Count - 1);
            }
            if (oldState.IsKeyUp(Keys.Enter) && ks.IsKeyDown(Keys.Enter))
            {
                rollOverInstance.Play();
                parent.Notify(this, menuItems[selectedIndex]);
            }
            oldState = ks;
            base.Update(gameTime);
        }
    }
}
