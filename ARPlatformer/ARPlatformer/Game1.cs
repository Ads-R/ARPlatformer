/* 
 * Program ID: ARPlatformer
 * 
 * Final Project in PROG2370
 * 
 * Revision History:
 *        CREATED: Adriel Roque December 6, 2020
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ARPlatformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //declare variables
        GraphicsDeviceManager graphics;
        public GraphicsDeviceManager Graphics { get => graphics; }

        SpriteBatch spriteBatch;
        public SpriteBatch Sprite {get => spriteBatch;}

        Vector2 stage;
        public Vector2 Stage { get => stage; }

        private GameScene currentScene;
        private MenuScene menuScene;
        private PlayScene playScene;
        private InstructionsScene instructionsScene;
        private ScoreScene scoreScene;
        private AboutScene aboutScene;
        private List<string> menuItems = new List<string> {"Start Game", "Instructions", "About", "Exit" };



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // set mouse visibility to true
            this.IsMouseVisible = true;
            //sets the stage width and height
            graphics.PreferredBackBufferWidth = 1152;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// 
        /// Initialize game scenes
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            
            menuScene = new MenuScene(this, menuItems);
            this.Components.Add(menuScene);
            currentScene = menuScene;

            playScene = new PlayScene(this);
            this.Components.Add(playScene);

            instructionsScene = new InstructionsScene(this);
            this.Components.Add(instructionsScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            currentScene.ShowScene();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        /// <summary>
        /// sets the current active scene
        /// </summary>
        /// <param name="sender">scene calling the method</param>
        /// <param name="action">string passed from the calling scene</param>
        public void Notify(GameScene sender, string action)
        {
            currentScene.HideScene();
            if (sender is MenuScene)
            {
                switch (action)
                {
                    case "Start Game":
                        currentScene = playScene;
                        break;
                    case "Instructions":
                        currentScene = instructionsScene;
                        break;
                    case "About":
                        currentScene = aboutScene;
                        break;
                    case "Exit":
                        Exit();
                        break;
                }
            }
            else if (sender is PlayScene)
            {
                PlayScene play = (PlayScene)sender;
                scoreScene = new ScoreScene(this, play.GetScore());
                this.Components.Add(scoreScene);
                currentScene = scoreScene;
                Reset();
            }
            else if(sender is ScoreScene)
            {
                this.Components.Remove(scoreScene);
                switch (action)
                {
                    case "Try Again":
                        currentScene = playScene;
                        break;
                    case "Return to Main Menu":
                        currentScene = menuScene;
                        break;
                }
            }
            else if(sender is InstructionsScene)
            {
                currentScene = menuScene;
            }
            else if(sender is AboutScene)
            {
                currentScene = menuScene;
            }
            currentScene.ShowScene();
        }

        /// <summary>
        /// Resets playScene variables by making a new instance
        /// </summary>
        public void Reset()
        {
            this.Components.Remove(playScene);
            playScene = new PlayScene(this);
            this.Components.Add(playScene);
        }
    }
}
