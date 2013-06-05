using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SummerProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameState gameState = GameState.Playing;

        //Level variables
        LevelDrawer levelDrawer;
        Player player;

        //comp vars
        int s_width;
        int s_height;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //resize window
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1024;

            s_width = graphics.PreferredBackBufferWidth;
            s_height = graphics.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            levelDrawer = new LevelDrawer(new Vector2(s_width, s_height));
            player = new Player(new Vector2(s_width, s_height));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D level = Content.Load<Texture2D>("levels/1");
            levelDrawer.addLevel(level);

            //adds the ground tex and playertex
            levelDrawer.GroundTexture = Content.Load<Texture2D>("ground");
            player.setTex(Content.Load<Texture2D>("player"));

            //starts the drawer
            levelDrawer.StartDrawer();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameState.Playing:
                    playingUpdate();
                    break;
            }

            base.Update(gameTime);
        }
        protected void playingUpdate()
        {
            //All game logic goes here
            //test to see if screen can move any farther in any dir
            bool[] levelCantMove = new bool[4];
            levelCantMove[(int)Directions.Right] = levelDrawer.offset.X >= 0;
            levelCantMove[(int)Directions.Left] = levelDrawer.offset.X <= -(levelDrawer.currLevelSize.X-s_width);
            levelCantMove[(int)Directions.Down] = levelDrawer.offset.Y <= -(levelDrawer.currLevelSize.Y - s_height);
            levelCantMove[(int)Directions.Up] = levelDrawer.offset.Y >= 0;
            //should the player be offset instead of the level
            bool[] playerPriority = new bool[4];
            playerPriority[(int)Directions.Up] = Math.Floor(player.position.Y) > Math.Floor(s_height / 2d);
            playerPriority[(int)Directions.Down] = Math.Floor(player.position.Y) < Math.Floor(s_height / 2d);
            playerPriority[(int)Directions.Left] = Math.Floor(player.position.X) > Math.Floor(s_width / 8d);
            playerPriority[(int)Directions.Right] = Math.Floor(player.position.X) < Math.Floor(s_width / 8d);
            //can the player move in each direction?
            bool[] playerCanMove = player.testDirections();
            //crazy col detection

            //resets player movement
            player.ySpeed = 0;
            player.xSpeed = 0;

            //Check keyboard input
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Right))
            {

                if (!levelCantMove[(int)Directions.Left] && !playerPriority[(int)Directions.Right])
                    levelDrawer.offset.X -= 1.5f;
                else if (playerCanMove[(int)Directions.Right])
                    player.xSpeed = 1.5f;
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {

                if (!levelCantMove[(int)Directions.Right] && !playerPriority[(int)Directions.Left])
                    levelDrawer.offset.X += 1.5f;
                else if (playerCanMove[(int)Directions.Left])
                    player.xSpeed = -1.5f;
            }
            if (keyState.IsKeyDown(Keys.Up))
            {

                if (!levelCantMove[(int)Directions.Up] && !playerPriority[(int)Directions.Up])
                    levelDrawer.offset.Y += 1.5f;
                else if (playerCanMove[(int)Directions.Up])
                    player.ySpeed = -1.5f;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                
                if (!levelCantMove[(int)Directions.Down] && !playerPriority[(int)Directions.Down])
                    levelDrawer.offset.Y -= 1.5f;
                else if (playerCanMove[(int)Directions.Down])
                    player.ySpeed = 1.5f;
            }

            //then do in-class updates after keypresses
            player.Update();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            // TODO: Add your drawing code here
            switch (gameState)
            {
                case GameState.Playing:
                    playingDraw();
                    break;
            }

            //end batch
            spriteBatch.End();
            base.Draw(gameTime);
        }
        protected void playingDraw()
        {
            levelDrawer.drawLevel(spriteBatch);
            player.Draw(spriteBatch);
        }
    }
}
