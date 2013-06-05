using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SummerProject
{
    public class Player
    {
        //general control vars
        bool[] canMove = new bool[4];
        public Vector2 position = new Vector2(50,50);
        Vector2 dimensions = new Vector2(0,0);
        int tileSize = 20;
        //tex var
        Texture2D playerTexture;
        //physics vars
        float gravity;
        public float xSpeed;
        public float ySpeed;

        //general vars
        Vector2 screensize;
        float scale;
        public Player(Vector2 screen)
        {
            for (int i = 0; i < canMove.Length; i++)
                canMove[i] = false;
            screensize = screen;

            scale = tileSize / 256f;
        }
        public void setTex(Texture2D tex)
        {
            playerTexture = tex;
            dimensions.X = tex.Width;
            dimensions.Y = tex.Height;
        }
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(playerTexture, position,null, Color.Red, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
        }
        public void Update()
        {
            position.X += xSpeed;
            position.Y += ySpeed;
        }
        public bool[] testDirections()
        {
            canMove[(int)Directions.Up] = position.Y > 0;
            canMove[(int)Directions.Down] = position.Y < (screensize.Y - playerTexture.Height*scale);
            canMove[(int)Directions.Left] = position.X > 0;
            canMove[(int)Directions.Right] = position.X < (screensize.X - playerTexture.Width*scale);

            //do col detection here
            return canMove;
        }
    }
}
