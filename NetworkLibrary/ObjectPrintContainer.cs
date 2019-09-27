using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NetworkLibrary
{
    [Serializable]
    public class ObjectPrintContainer
    {
        private char objectChar;
        private Position posInField;

        public ObjectPrintContainer(char objectChar, Position pos, ConsoleColor color)
        {
            this.ObjectChar = objectChar;
            this.PosInField = pos;
            this.Color = color;
        }

        public char ObjectChar
        {
            get
            {
                return this.objectChar;
            }
            private set
            {
                this.objectChar = value;
            }
        }

        public Position PosInField
        {
            get
            {
                return this.posInField;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Error value cant be null.");
                }

                this.posInField = value;
            }
        }

        public ConsoleColor Color
        {
            get;
            private set;
        }
    }
}