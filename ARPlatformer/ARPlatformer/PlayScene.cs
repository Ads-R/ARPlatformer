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
    public class PlayScene : GameScene
    {
        Texture2D background;
        Player player;
        Tile tile;
        Coin coin;
        Door door;
        private KeyboardState oldState;
        List<Tile> tileList = new List<Tile>();
        List<Coin> coinList = new List<Coin>();
        private SpriteFont standardFont;

        /// <summary>
        /// 2d array to iterate and instantiate later for play scene drawable components
        /// 0 for empty space
        /// 1 is for tiles
        /// 2 is for coins
        /// </summary>
        int[,] tileMap = new int[12, 18] {
       /*1*/ {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
       /*2*/ {0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0},
       /*3*/ {2,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0},
       /*4*/ {0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,1,1},
       /*5*/ {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
       /*6*/ {0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
       /*7*/ {0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
       /*8*/ {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,2,2},
       /*9*/ {0,0,0,0,0,1,1,0,0,0,2,0,1,0,0,0,0,0},
       /*0*/ {0,0,0,0,1,0,0,0,0,2,0,1,0,0,2,0,1,0},
       /*1*/ {2,0,0,1,0,0,0,0,2,0,1,0,0,2,0,1,0,0},
       /*2*/ {0,1,1,0,0,0,0,0,1,1,0,0,0,0,1,1,0,0}
            };

        const int tileWidth = 64;
        const int tileHeight = 64;

        public PlayScene(Game game) : base(game)
        {
            background = parent.Content.Load<Texture2D>("Images/Background");
            this.standardFont = parent.Content.Load<SpriteFont>("Fonts/StandardFont");

            door = new Door(parent, "Images/Door", new Vector2(parent.Stage.X - 100, 90));
            this.Components.Add(door);

            player = new Player(parent, "Images/Player", parent.Stage, "Sounds/Run", "Sounds/Jump");
            this.Components.Add(player);

            //iterate through the 2d array and instantiate tiles and coins
            for(int y=0; y<tileMap.GetLength(0); y++)
            {
                for(int x=0; x<tileMap.GetLength(1); x++)
                {
                    if (tileMap[y, x] == 1)
                    {
                        tile = new Tile(parent, "Images/Tile", parent.Stage, new Vector2(tileWidth * x, tileHeight * y));
                        this.Components.Add(tile);
                        tileList.Add(tile);
                    }
                    if(tileMap[y, x] == 2)
                    {
                        coin = new Coin(parent, "Images/Coin", new Vector2(tileWidth * x, tileHeight * y));
                        this.Components.Add(coin);
                        coinList.Add(coin);
                    }
                }
            }
        }
        /// <summary>
        /// Draws the background and score
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            parent.Sprite.Begin();
            parent.Sprite.Draw(background, new Rectangle(0,0, parent.Graphics.PreferredBackBufferWidth, parent.Graphics.PreferredBackBufferHeight), Color.White);
            parent.Sprite.DrawString(standardFont, "Score: "+player.Score, new Vector2(0,0), Color.Red);
            parent.Sprite.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks for collision both tile, coin and door, if colliding with door and enter key is pressed, game ends
        /// If player falls game also ends
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            foreach (Tile tile in tileList)
            {
                player.Collision(tile.Rectangle);
            }

            foreach(Coin coin in coinList)
            {
                player.CoinGain(coin.Rectangle, coin);
            }

            if(player.Position.Y> parent.Stage.Y+250)
            {
                parent.Notify(this, "Score");
            }

            bool end = player.DoorGrab(door.Rectangle);

            if(ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter) && end)
            {
                parent.Notify(this, "Score");
            }
            oldState = ks;
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// Returns player score
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return player.Score;
        }
    }
}
