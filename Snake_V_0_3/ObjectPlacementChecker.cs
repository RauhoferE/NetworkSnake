//-----------------------------------------------------------------------
// <copyright file="ObjectPlacementChecker.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="ObjectPlacementChecker"/> class.
    /// </summary>
    public class ObjectPlacementChecker
    {
        /// <summary>
        /// A normal random.
        /// </summary>
        private Random rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPlacementChecker"/> class.
        /// </summary>
        public ObjectPlacementChecker()
        {
            this.rnd = new Random();
        }

        /// <summary>
        /// This event should fires when the placement has been found.
        /// </summary>
        public event EventHandler<StaticObjectEventArgs> OnPlacementFound;

        /// <summary>
        /// This method checks the placement.
        /// </summary>
        /// <param name="field"> The <see cref="PlayingField"/>. </param>
        /// <param name="gameObjects"> The list of <see cref="GameObjects"/>. </param>
        /// <param name="powerupToPlace"> The <see cref="StaticObjects"/>. </param>
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

        /// <summary>
        /// This method fires the <see cref="OnPlacementFound"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        protected virtual void FireOnPlacementFound(StaticObjectEventArgs e)
        {
            this.OnPlacementFound?.Invoke(this, e);
        }
    }
}