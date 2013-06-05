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
        Vector2 position = new Vector2(50,50);
        Vector2 dimensions = new Vector2(0,0);
        int tileSize = 20;
        //tex var
        Texture2D playerTexture;
        //physics vars
        float gravity;
        public float xSpeed;
        public float ySpeed;

        public Player()
        {
            for (int i = 0; i < canMove.Length; i++)
                canMove[i] = false;
        }
        public void setTex(Texture2D tex)
        {
            playerTexture = tex;
            dimensions.X = tex.Width;
            dimensions.Y = tex.Height;
        }
        public void Draw(SpriteBatch batch)
        {
            float scale = tileSize / 256f;
            batch.Draw(playerTexture, position,null, Color.Red, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
        }
        public void Update()
        {
            position.X += xSpeed;
            position.Y += ySpeed;
        }
    }
}
