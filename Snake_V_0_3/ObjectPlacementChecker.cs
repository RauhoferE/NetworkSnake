using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class ObjectPlacementChecker
    {
        private Random rnd;

        public ObjectPlacementChecker()
        {
            this.rnd = new Random();
        }
        public event System.EventHandler<StaticObjectEventArgs> OnPlacementFound;

        public void CheckPlacement(PlayingField field, List<GameObjects> gameObjects, StaticObjects powerupToPlace)
        {
            bool isPlacedCorrect = false;

            while (!isPlacedCorrect)
            {
                var xPos = this.rnd.Next(0, field.Width - 2);
                var yPos = this.rnd.Next(0, field.Length - 2);

                foreach (var segment in gameObjects)
                {
                    if (segment.Pos.X == xPos && segment.Pos.Y == yPos)
                    {
                        break;
                    }
                    else if (gameObjects.LastOrDefault() == segment)
                    {
                        isPlacedCorrect = true;
                        powerupToPlace.Pos = new Position(xPos, yPos);
                        break;
                    }
                }
            }

            this.FireOnPlacementFound(new StaticObjectEventArgs(powerupToPlace));
        }

        protected virtual void FireOnPlacementFound(StaticObjectEventArgs e)
        {
            OnPlacementFound?.Invoke(this, e);
        }
    }
}