using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class StaticGameObjectFactory
    {
        private System.Random rnd;

        public event System.EventHandler<StaticObjectEventArgs> OnObjectCreated;

        public StaticGameObjectFactory(Random rnd)
        {
            this.rnd = rnd;
        }

        public void CreatePowerUp()
        {
            StaticObjects powerUp = null;

            switch (rnd.Next(0, 3))
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

        public Apple ReturnApple()
        {
            return new Apple(new Position());
        }

        public Rainbow ReturnRainbow()
        {
            return new Rainbow(new Position());
        }

        public SegmentDestroyer ReturnSegmentDestroyer()
        {
            return new SegmentDestroyer(new Position());
        }

        protected virtual void FireObjectCreated(StaticObjectEventArgs e)
        {
            if (this.OnObjectCreated != null)
            {
                this.OnObjectCreated(this, e);
            }
        }
    }
}