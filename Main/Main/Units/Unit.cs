﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Main.Interfaces;

namespace Main.Units
{
     abstract class Unit : ILoad, IDrawable
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle rectangle;
        protected Vector2 velocity;
		
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }
            set
            {
                this.rectangle = value;
            }
        }

        public abstract void Load(ContentManager contentManager);
        public virtual void Update(GameTime gameTime) {}
        public virtual void Update(GameTime gameTime, Player player) {}
        public virtual void Draw(SpriteBatch spriteBatch){}       
    }
}
