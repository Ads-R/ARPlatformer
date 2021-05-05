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
    public class InstructionsScene : GameScene
    {
        // declare variables
        private KeyboardState oldState;
        private Texture2D bg;
        private SpriteFont headingFont;
        private SpriteFont subjectFont;
        private SpriteFont standardFont;
        private SpriteFont smallFont;
        Vector2 position;

        private List<string> control = new List<string> 
        {"Press the Left Arrow to run towards the left direction",
        "Press the Right Arrow to run towards the right direction",
        "Press the Space Bar to jump",
        "Press the Enter Key when you reached the exit door to finish the stage"
        };
        public InstructionsScene(Game game) : base(game)
        {
            this.bg = parent.Content.Load<Texture2D>("Images/Background_Information");
            this.subjectFont = parent.Content.Load<SpriteFont>("Fonts/SubjectFont");
            this.headingFont = parent.Content.Load<SpriteFont>("Fonts/HeadingFont");
            this.standardFont = parent.Content.Load<SpriteFont>("Fonts/StandardFont");
            this.smallFont = parent.Content.Load<SpriteFont>("Fonts/SmallFont");
            this.position = new Vector2(50, 100);
        }

        /// <summary>
        /// Draw the contents of Instruction scene which includes the game description and control instructions
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Rectangle srcRectangleBackground = new Rectangle(0, 0, bg.Width, bg.Height);
            parent.Sprite.Begin();
            parent.Sprite.Draw(bg, srcRectangleBackground, Color.White);

            parent.Sprite.DrawString(headingFont, "Description", position, Color.Green);

            parent.Sprite.DrawString(subjectFont, "ARPlatformer is a platformer game. The goal of the game " +
                "is to collect as much coins as possible and reach the exit door. \n " +
                "The user is also to avoid falling between the open spaces on the most bottom part of the tiles else the current \n" +
                "game fails. The game employs collision for the tiles and coins and gravity for jumping and falling"
                , new Vector2(position.X ,position.Y+50), Color.Black);

            parent.Sprite.DrawString(headingFont, "Controls", new Vector2(position.X, position.Y+200), Color.Green);

            float y_coords = position.Y + 250;
            for (int x=0; x<control.Count; x++)
            {
                parent.Sprite.DrawString(subjectFont, control[x], new Vector2(position.X, y_coords), Color.Black);
                y_coords += subjectFont.LineSpacing+25;
            }

            parent.Sprite.DrawString(smallFont, "Press Escape key to go to Main Menu", new Vector2(position.X, y_coords+50), Color.Red);

            parent.Sprite.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks for escape key input and returns to main menu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if(ks.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
            {
                parent.Notify(this, "return");
            }
            oldState = ks;
            base.Update(gameTime);
        }
    }
}
