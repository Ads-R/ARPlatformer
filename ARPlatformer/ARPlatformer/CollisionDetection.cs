using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;



/*Credits to Oyyou for the Tile Collision, Gravity and Jump Response solutions */
namespace ARPlatformer
{
    public static class CollisionDetection
    {
        /// <summary>
        /// Checks for collision between player and door, returns a boolean
        /// </summary>
        /// <param name="player">Rectangle property of player</param>
        /// <param name="door">Rectangle property of door</param>
        /// <returns>boolean</returns>
        public static bool DoorCollision(this Rectangle player, Rectangle door)
        {
            return player.Intersects(door);
        }

        /// <summary>
        /// Checks for collision between player and coin, returns a boolean
        /// </summary>
        /// <param name="player">Rectangle property of player</param>
        /// <param name="coin">Rectangle property of coin</param>
        /// <returns>boolean</returns>
        public static bool CoinCollision(this Rectangle player, Rectangle coin)
        {
            return player.Intersects(coin);
        }

        /// <summary>
        /// Checks for collision between player and top side of the tile, 
        /// </summary>
        /// <param name="player">Rectangle property of player</param>
        /// <param name="tile">Rectangle property of tile</param>
        /// <returns>boolean</returns>
        public static bool TopCollision(this Rectangle player, Rectangle tile)
        {
            return (player.Bottom >= tile.Top - 1 &&
                player.Bottom <= tile.Top + (tile.Height / 2) &&
                player.Right >= tile.Left + (tile.Width /5) &&
                player.Left <= tile.Right - (tile.Width /5));
        }

        /// <summary>
        /// Checks for collision between player and bottom side of the tile, 
        /// </summary>
        /// <param name="player">Rectangle property of player</param>
        /// <param name="tile">Rectangle property of tile</param>
        /// <returns>boolean</returns>
        public static bool BottomCollision(this Rectangle player, Rectangle tile)
        {
            return (player.Top <= tile.Bottom + (tile.Height/5) &&
                player.Top >= tile.Bottom  -1 &&
                player.Right >= tile.Left + (tile.Width / 5) &&
                player.Left <= tile.Right - (tile.Width / 2));
        }

        /// <summary>
        /// Checks for collision between player and left side of the tile, 
        /// </summary>
        /// <param name="player">Rectangle property of player</param>
        /// <param name="tile">Rectangle property of tile</param>
        /// <returns>boolean</returns>
        public static bool LeftCollision(this Rectangle player, Rectangle tile)
        {
            return (player.Right <= tile.Right &&
                player.Right >= tile.Left - 5 &&
                player.Top <= tile.Bottom - (tile.Width / 4) &&
                player.Bottom >= tile.Top + (tile.Width / 4));
        }

        /// <summary>
        /// Checks for collision between player and right side of the tile, 
        /// </summary>
        /// <param name="player">Rectangle property of player</param>
        /// <param name="tile">Rectangle property of tile</param>
        /// <returns>boolean</returns>
        public static bool RightCollision(this Rectangle player, Rectangle tile)
        {
            return (player.Left >= tile.Left &&
                player.Left <= tile.Right + 5 &&
                player.Top <= tile.Bottom - (tile.Width / 4) &&
                player.Bottom >= tile.Top + (tile.Width / 4));
        }


    }
}
