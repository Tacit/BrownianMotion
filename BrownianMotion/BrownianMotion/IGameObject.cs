using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrownianMotion
{
    public interface IGameObject
    {
        Vector2 Velocity {get; set;}
        Vector2 Position { get; set; }
        Rectangle Bounds { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void OnColision(Vector2 vector);

        void OnColision(Vector2 vector, IGameObject object2);
    }
}
