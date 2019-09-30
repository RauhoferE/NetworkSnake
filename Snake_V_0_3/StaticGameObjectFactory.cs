//-----------------------------------------------------------------------
// <copyright file="StaticGameObjectFactory.cs" company="FH Wiener Neustadt">
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

    /// <summary>
    /// The <see cref="StaticGameObjectFactory"/> class.
    /// </summary>
    public class StaticGameObjectFactory
    {
        /// <summary>
        /// The random.
        /// </summary>
        private Random rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticGameObjectFactory"/> class.
        /// </summary>
        /// <param name="rnd"> The random. </param>
        public StaticGameObjectFactory(Random rnd)
        {
            this.rnd = rnd;
        }

        /// <summary>
        /// This event should fire when a object has been created.
        /// </summary>
        public event EventHandler<StaticObjectEventArgs> OnObjectCreated;

        /// <summary>
        /// This method creates a power up.
        /// </summary>
        public void CreatePowerUp()
        {
            StaticObjects powerUp = null;

            switch (this.rnd.Next(0, 3))
            {
                case 0:
                    powerUp = this.ReturnApple(); 
                    break;
                case 1:
                    powerUp = this.ReturnRainbow();
                    break;
                case 2:
                    powerUp = this.ReturnSegmentDestroyer();
                    break;
                default:
                    break;
            }

            this.FireObjectCreated(new StaticObjectEventArgs(powerUp));
        }

        /// <summary>
        /// This method returns an apple.
        /// </summary>
        /// <returns> It returns an <see cref="Apple"/>. </returns>
        public Apple ReturnApple()
        {
            return new Apple(new Position());
        }

        /// <summary>
        /// This method returns an rainbow.
        /// </summary>
        /// <returns> It returns an <see cref="Rainbow"/>. </returns>
        public Rainbow ReturnRainbow()
        {
            return new Rainbow(new Position());
        }

        /// <summary>
        /// This method returns an segment destroyer.
        /// </summary>
        /// <returns> It returns an <see cref="SegmentDestroyer"/>. </returns>
        public SegmentDestroyer ReturnSegmentDestroyer()
        {
            return new SegmentDestroyer(new Position());
        }

        /// <summary>
        /// This method fires the <see cref="OnObjectCreated"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        protected virtual void FireObjectCreated(StaticObjectEventArgs e)
        {
            if (this.OnObjectCreated != null)
            {
                this.OnObjectCreated(this, e);
            }
        }
    }
}