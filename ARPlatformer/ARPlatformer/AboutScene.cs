using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ARPlatformer
{
    public class AboutScene : GameScene
    {
        //declare variables
        private KeyboardState oldState;
        private Texture2D bg;
        private SpriteFont subjectFont;
        private SpriteFont headingFont;
        private SpriteFont titleFont;
        private SpriteFont smallFont;
        private string title;

        //declare and initialize list of strings
        private List<string> heading = new List<string> { "Name: ", "Course: ", "Date Published: " };
        private List<string> subject = new List<string> { "Adriel Roque", "PROG2370 - Game Programming with Data Structures", "December 12, 2020" };
        public AboutScene(Game game) : base(game)
        {
            this.bg = parent.Content.Load<Texture2D>("Images/Background_About");
            this.titleFont = parent.Content.Load<SpriteFont>("Fonts/TitleFont");
            this.subjectFont = parent.Content.Load<SpriteFont>("Fonts/SubjectFont");
            this.headingFont = parent.Content.Load<SpriteFont>("Fonts/HeadingFont");
            this.smallFont = parent.Content.Load<SpriteFont>("Fonts/SmallFont");
            this.title = "Finals Project in Game Programming";
        }

        /// <summary>
        /// Draw a background and iterate on the list of text and draw
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Rectangle srcRectangleBackground = new Rectangle(0, 0, bg.Width, bg.Height);
            Vector2 orgPos = new Vector2(50, 200);
            Vector2 pos = orgPos;
            parent.Sprite.Begin();

            parent.Sprite.Draw(bg, srcRectangleBackground, Color.White);

            parent.Sprite.DrawString(titleFont, title, new Vector2(50, 100), Color.Black);

            for(int x=0; x<heading.Count; x++)
            {
                parent.Sprite.DrawString(headingFont, heading[x], pos, Color.Black);
                pos.X += heading[x].Length * 18;
                parent.Sprite.DrawString(subjectFont, subject[x], pos, Color.Black);
                pos.Y += headingFont.LineSpacing;
                pos.X = orgPos.X;
            }

            parent.Sprite.DrawString(smallFont, "Press Escape key to go to Main Menu", new Vector2(pos.X, pos.Y+100), Color.Red);
            parent.Sprite.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks for an escape key input and returns to the main menu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
            {
                parent.Notify(this, "return");
            }
            oldState = ks;
            base.Update(gameTime);
        }
    }
}
