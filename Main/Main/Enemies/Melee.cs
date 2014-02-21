﻿using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Main
{
    class Melee : Enemy
    {
        private float playerDistanceX;
        private float playerDistanceY;
        private Vector2 patrolPositon;
        private int patrolDistance;
        private bool hasJumped = false;

        public Melee(int positonX, int positionY)
        {
            this.position.X = positonX;
            this.position.Y = positionY;
            this.patrolPositon.X = positonX;
            this.patrolPositon.Y = positionY;
            this.patrolDistance = 50;
            this.velocity.X = 1f;
            this.velocity.Y = 1f;
        }
        public override void Update(GameTime gameTime, int playerX, int playerY)
        {
            
            position += velocity;
            
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, 50,50);
            if(position.X > patrolPositon.X)
            {
                if((int)(position.X - patrolPositon.X) > patrolDistance )
                {
                    velocity.X = -1f;
                }
            }
            if(position.X < patrolPositon.X)
            {
                if(Math.Abs((int)(position.X - patrolPositon.X)) > patrolDistance)
                {
                    velocity.X = 1f;
                }
            }

          playerDistanceX = playerX - position.X;
            playerDistanceY = playerY - position.Y;
          
            int detectionDistanceX = 250;
            int detectionDistanceY = 200;

            if (playerDistanceX >= -detectionDistanceX && playerDistanceX <= detectionDistanceX
                && playerDistanceY >= -detectionDistanceY && playerDistanceY <= detectionDistanceY)
            {
                if (playerDistanceX < 0)
                {
                    velocity.X = -1f;
                    patrolPositon.X -= 1;
                }
                else if (playerDistanceX > 0)
                {
                    velocity.X = 1f;
                    patrolPositon.X += 1;
                }
            }
            if (hasJumped == true)
            {
                position.Y -= 4f;
                velocity.Y = -8f;
                hasJumped = false;
            }

            if (velocity.Y < 12)
            {
                velocity.Y += 0.4f;
            }
        }


        public override void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>("knight1");
        }

        public override void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
          
            if (rectangle.TouchTopOf(newRectangle))
            {
                velocity.Y = 0f;
                
            }
            if (rectangle.TouchLeftOff(newRectangle))
            {
                hasJumped = true;
                velocity.X = -1f;
                patrolPositon.X -= 10;
            }
            if (rectangle.TouchRightOff(newRectangle))
            {
                hasJumped = true;
                velocity.X = 1f;
                patrolPositon.X += 10;
            }
            if(rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.X > xOffset - rectangle.Width)
            {
                position.X = xOffset - rectangle.Width;
            }
            if (position.Y < 0)
            {
                velocity.Y = 1f;
            }
            if (position.Y > yOffset - rectangle.Height)
            {
                position.Y = yOffset - rectangle.Height;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {       
            spriteBatch.Draw(texture, rectangle, Color.White);
            /*if (velocity.X > 0)
            {
                spriteBatch.Draw(texture, rectangle, null, Color.White, 0f, new Vector2(50,50), SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, rectangle, null, Color.White, 0f, new Vector2(50, 50), SpriteEffects.FlipHorizontally, 0f);
            }*/
        }
    }
}
