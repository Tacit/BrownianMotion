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

namespace BrownianMotion
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BrownianMotionGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D test;
        List<IGameObject> objects;
        Random r = new Random();

        public BrownianMotionGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
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
            // TODO: Add your initialization logic here
            base.Initialize();
            objects = new List<IGameObject>();
            for (int i = 0; i < 10; i++)
            {
                objects.Add(new Molecula(new Vector2(r.Next(0, graphics.PreferredBackBufferHeight),
                                                        r.Next(0, graphics.PreferredBackBufferWidth)), test, r));
            }

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = Content.Load<Texture2D>("molecula");
            // TODO: use this.Content to load your game content here
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
            foreach (var o in objects)
            {
                CheckCollisionWithBorder(o);
                foreach (var o1 in objects)
                {
                    if (o != o1)
                    {
                        CheckCollision(o, o1);
                    }
                }
                o.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach (var o in objects)
                o.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void CheckCollision(IGameObject object1, IGameObject object2)
        {
            Vector2 vector = new Vector2(1f, 1f);

            if (object1.Position.X > object2.Position.X && 
                object1.Position.X < object2.Position.X + object2.Size.X &&
                object1.Position.Y > object2.Position.Y && 
                object1.Position.Y < object2.Position.Y + object2.Size.Y)
            {
                vector.X = -1f;
                object1.OnColision(vector, object2);
            }

            
        }

        protected void CheckCollisionWithBorder(IGameObject object1)
        {
            Vector2 vector = new Vector2(1f, 1f);
            if (object1.Position.X - object1.Size.X / 2 < 0 ||
                object1.Position.X + object1.Size.X > graphics.PreferredBackBufferWidth)
                vector.X = -1f;
            if (object1.Position.Y - (object1.Size.Y / 2) < 0 ||
                object1.Position.Y + (object1.Size.Y) > graphics.PreferredBackBufferHeight)
                vector.Y = -1f;

            object1.OnColision(vector);
        }
    }
}
