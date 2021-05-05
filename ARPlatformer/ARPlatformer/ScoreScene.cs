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
    public class ScoreScene : GameScene
    {
        private KeyboardState oldState;
        private Color standardColor = Color.Black;
        private SpriteFont standardFont;
        private SpriteFont headingFont;
        private Vector2 position;
        private int score;
        public int selectedIndex;

        private Texture2D background;

        private KeyboardState ks;

        private SoundEffect rollOver;
        private SoundEffectInstance rollOverInstance;
        private int timeSince = 0;

        private List<string> response = new List<string> {"Try Again", "Return to Main Menu"};

        public ScoreScene(Game game, int score) : base(game)
        {
            this.background = parent.Content.Load<Texture2D>("Images/Background");
            this.standardFont = parent.Content.Load<SpriteFont>("Fonts/StandardFont");
            this.headingFont = parent.Content.Load<SpriteFont>("Fonts/HeadingFont");
            this.score = score;
            this.position = new Vector2(parent.Stage.X/2 - headingFont.Texture.Width/2-50, parent.Stage.Y/2);

            this.rollOver = parent.Content.Load<SoundEffect>("Sounds/Rollover");
            this.rollOverInstance = rollOver.CreateInstance();
        }

        /// <summary>
        /// Draws the player score and options for trying again or returning to main menu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 temporaryPos = position;
            parent.Sprite.Begin();
            parent.Sprite.Draw(background, new Rectangle(0,0, parent.Graphics.PreferredBackBufferWidth, parent.Graphics.PreferredBackBufferHeight), Color.White);
            parent.Sprite.DrawString(standardFont, "Your Score is: "+score, new Vector2(parent.Stage.X/2 - standardFont.Texture.Width/2 -50, position.Y - 100), Color.Blue);
            for (int i=0; i<response.Count; i++)
            {
                if (selectedIndex == i)
                {
                    parent.Sprite.DrawString(headingFont, response[i], temporaryPos, Color.Red);
                    temporaryPos.Y += headingFont.LineSpacing+25;
                }
                else
                {
                    parent.Sprite.DrawString(headingFont, response[i], temporaryPos, standardColor);
                    temporaryPos.Y += headingFont.LineSpacing+25;
                }
            }
            parent.Sprite.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Checks for user input and updates the highlighted string
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            timeSince += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSince> 200)
            {
                ks = Keyboard.GetState();
            }

            if (oldState.IsKeyUp(Keys.Down) && ks.IsKeyDown(Keys.Down))
            {
                rollOverInstance.Play();
                selectedIndex = MathHelper.Clamp(selectedIndex + 1, 0, response.Count - 1);
            }
            if (oldState.IsKeyDown(Keys.Up) && ks.IsKeyUp(Keys.Up))
            {
                rollOverInstance.Play();
                selectedIndex = MathHelper.Clamp(selectedIndex - 1, 0, response.Count - 1);
            }
            if (oldState.IsKeyUp(Keys.Enter) && ks.IsKeyDown(Keys.Enter))
            {
                rollOverInstance.Play();
                parent.Notify(this, response[selectedIndex]);
            }
            oldState = ks;
            base.Update(gameTime);
        }
    }
}
