using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SummerProject
{
    class LevelDrawer
    {
        List<Texture2D> levels = new List<Texture2D>();

        //specifies the width and height of tiles
        int tileSize = 20;

        //array to deal with PNG level
        Color[] currLevel;
        int[,] currLevel2D;
        
        //Texture vars for drawing
        Texture2D groundTexture;
        public Texture2D GroundTexture { get { return groundTexture; } set { groundTexture = value; } }

        public LevelDrawer()
        {

        }
        private void loadLevel(Texture2D map)
        {
            currLevel = new Color[map.Width * map.Height];
            map.GetData<Color>(currLevel);

            currLevel2D = new int[map.Width, map.Height];
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                {
                    Color color = currLevel[x + y * map.Width];

                    if (color.R == 0 && color.B == 0 && color.G == 0)
                    {
                        //it's black and a wall
                        currLevel2D[x, y] = (int)ObjectEnum.Wall;
                    }
                    else
                    {
                        //it's blank
                        currLevel2D[x, y] = (int)ObjectEnum.Blank;
                    }
                }
        }
        public void StartDrawer()
        {
            loadLevel(levels[0]);
        }
        public void drawLevel(SpriteBatch batch)
        {
            //scales the tex to 10px width and height
            float scale = tileSize / 256f;

            for (int x = 0; x < currLevel2D.GetLength(0); x++)
                for (int y = 0; y < currLevel2D.GetLength(1); y++)
                    switch (currLevel2D[x,y])
                    {
                        case (int)ObjectEnum.Wall:
                            batch.Draw(groundTexture, new Vector2(x * tileSize, y * tileSize), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
                            break;
                        default:
                            break;
                    }
            
            
        }
        public void addLevel(Texture2D level)
        {
            levels.Add(level);
            //Debug.Write(levels);
        }
    }
}
