using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class CollisionManager
    {
        public event System.EventHandler<CollisionEventArgs> OnPowerUpCollided;
        public event System.EventHandler OnObsatcleCollided;

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

        protected virtual void FireOnPowerUpCollided(CollisionEventArgs e)
        {
            OnPowerUpCollided?.Invoke(this, e);
        }

        protected virtual void FireOnObsatcleCollided()
        {
            OnObsatcleCollided?.Invoke(this, EventArgs.Empty);
        }
    }
}