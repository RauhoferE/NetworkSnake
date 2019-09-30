//-----------------------------------------------------------------------
// <copyright file="CollisionManager.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace Snake_V_0_3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The <see cref="CollisionManager"/> class.
    /// </summary>
    public class CollisionManager
    {
        /// <summary>
        /// This event fires when the snake collides with a power up.
        /// </summary>
        public event System.EventHandler<CollisionEventArgs> OnPowerUpCollided;

        /// <summary>
        /// This event fires when the snake collides with an obstacle.
        /// </summary>
        public event System.EventHandler OnObsatcleCollided;

        /// <summary>
        /// This method checks the collision.
        /// </summary>
        /// <param name="field"> The <see cref="PlayingField"/>. </param>
        /// <param name="powerups"> The list of power ups. </param>
        /// <param name="snake"> The current snake. </param>
        public void CheckCollision(PlayingField field, List<StaticObjects> powerups, List<SnakeSegment> snake)
        {
            foreach (var element in snake)
            {
                if (element.Pos.X < 0 || element.Pos.X == field.Width - 1 || element.Pos.Y < 0 || element.Pos.Y == field.Length - 1)
                {
                    this.FireOnObsatcleCollided();
                    break;
                }

                foreach (var powerup in powerups)
                {
                    if (powerup.Pos.X == element.Pos.X && powerup.Pos.Y == element.Pos.Y)
                    {
                        this.FireOnPowerUpCollided(new CollisionEventArgs(element, powerup));
                    }
                }

                if (snake.FirstOrDefault() != null && element == snake.FirstOrDefault())
                {
                    for (int i = 1; i < snake.Count; i++)
                    {
                        if (snake[i].Pos.X == element.Pos.X && snake[i].Pos.Y == element.Pos.Y)
                        {
                            this.FireOnObsatcleCollided();
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnPowerUpCollided"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="CollisionEventArgs"/>. </param>
        protected virtual void FireOnPowerUpCollided(CollisionEventArgs e)
        {
            this.OnPowerUpCollided?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnObsatcleCollided"/> event.
        /// </summary>
        protected virtual void FireOnObsatcleCollided()
        {
            this.OnObsatcleCollided?.Invoke(this, EventArgs.Empty);
        }
    }
}