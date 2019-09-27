using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class FieldPrintContainer
    {
        private int width;
        private int height;
        private char symbol;
        private List<ObjectPrintContainer> currentItems;
        private GameInformationContainer info;


        public FieldPrintContainer(int width, int height, char symbol)
        {
            this.Width = width;
            this.Height = height;
            this.Symbol = symbol;
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Error value cant be smaller or equal to zero.");
                }

                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Error value cant be smaller or equal to null.");
                }

                this.height = value;
            }
        }

        public char Symbol
        {
            get
            {
                return this.symbol;
            }
            private set
            {
                this.symbol = value;
            }
        }
    }
}