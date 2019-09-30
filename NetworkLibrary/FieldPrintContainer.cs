//-----------------------------------------------------------------------
// <copyright file="FieldPrintContainer.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace NetworkLibrary
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="FieldPrintContainer"/> class.
    /// </summary>
    [Serializable]
    public class FieldPrintContainer
    {
        /// <summary>
        /// Represents the width of the field.
        /// </summary>
        private int width;

        /// <summary>
        /// Represents the height of the field.
        /// </summary>
        private int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldPrintContainer"/> class.
        /// </summary>
        /// <param name="width"> The width of the field. </param>
        /// <param name="height"> The height of the field. </param>
        /// <param name="symbol"> The symbol of the field. </param>
        public FieldPrintContainer(int width, int height, char symbol)
        {
            this.Width = width;
            this.Height = height;
            this.Symbol = symbol;
        }

        /// <summary>
        /// Gets the width of the field.
        /// </summary>
        /// <value> A normal integer. </value>
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

        /// <summary>
        /// Gets the height of the field.
        /// </summary>
        /// <value> A normal integer. </value>
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

        /// <summary>
        /// Gets the symbol of the field.
        /// </summary>
        /// <value> A normal character. </value>
        public char Symbol
        {
            get;
        }
    }
}