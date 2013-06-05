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
        //specifies the offset of the drawn level
        public Vector2 offset = new Vector2(0, 0);

        //array to deal with PNG level
        Color[] currLevel;
        int[,] currLevel2D;
        
        //Texture vars for drawing
        Texture2D groundTexture;
        public Texture2D GroundTexture { get { return groundTexture; } set { groundTexture = value; } }

        //vars for dealing with the currently drawn level
        public Vector2 currLevelSize = new Vector2(0, 0);
        Vector2 screenSize;
        public LevelDrawer(Vector2 screen)
        {
            screenSize = screen;
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
            currLevelSize.X = map.Width * tileSize;
            currLevelSize.Y = map.Height * tileSize;
        }
        public void StartDrawer()
        {
            loadLevel(levels[0]);
        }
        public void drawLevel(SpriteBatch batch)
        {
            //scales the tex to 10px width and height
            float scale = tileSize / 256f;

            for (int x = (int)Math.Abs(Math.Ceiling(offset.X/tileSize)); x < (Math.Abs(offset.X) + screenSize.X)/tileSize; x++)
                for (int y = (int)Math.Abs(Math.Ceiling(offset.Y / tileSize)); y < (Math.Abs(offset.Y) + screenSize.Y)/tileSize; y++)
                    switch (currLevel2D[x,y])
                    {
                        case (int)ObjectEnum.Wall:
                            Vector2 pos = new Vector2(x * tileSize, y * tileSize);
                            batch.Draw(groundTexture, pos + offset, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
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
