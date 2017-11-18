﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BrownianMotion
{
    public class Molecula : IGameObject
    {
        Texture2D texture;

        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Molecula(Vector2 pos, Texture2D texture, Random r)
        {
            Size = new Vector2(16f, 16f);
            float x = (float)(r.Next(-100, 100)) / 100;
            float y = (float)(r.Next(-100, 100)) / 100;
            Velocity = new Vector2(x, y);
            Position = pos;
            this.texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, 
                new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height), 
                null, 
                Color.White);
        }

        public void OnColision(Vector2 vector)
        {
            this.Velocity = new Vector2(Velocity.X * vector.X, Velocity.Y * vector.Y);
        }


        public void OnColision(Vector2 vector, IGameObject object2)
        {
            this.Velocity = new Vector2((Velocity.X + object2.Velocity.X/2) * -1, 
                Velocity.Y);
            //this.Velocity = new Vector2(Velocity.X * -1, Velocity.Y * -1);
        }
    }
}
