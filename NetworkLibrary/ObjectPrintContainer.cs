//-----------------------------------------------------------------------
// <copyright file="ObjectPrintContainer.cs" company="FH Wiener Neustadt">
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

    /// <summary>
    /// The <see cref="ObjectPrintContainer"/> class.
    /// </summary>
    [Serializable]
    public class ObjectPrintContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPrintContainer"/> class.
        /// </summary>
        /// <param name="objectChar"> The object character. </param>
        /// <param name="pos"> The object position. </param>
        /// <param name="color"> The object color. </param>
        public ObjectPrintContainer(char objectChar, Position pos, ConsoleColor color)
        {
            this.ObjectChar = objectChar;
            this.PosInField = pos;
            this.Color = color;
        }

        /// <summary>
        /// Gets the object character.
        /// </summary>
        /// <value> A normal character. </value>
        public char ObjectChar
        {
            get;
        }

        /// <summary>
        /// Gets the position in the field.
        /// </summary>
        /// <value> A <see cref="Position"/> object. </value>
        public Position PosInField
        {
            get;
        }

        /// <summary>
        /// Gets the color of the object.
        /// </summary>
        /// <value> A normal <see cref="ConsoleColor"/>. </value>
        public ConsoleColor Color
        {
            get;
        }
    }
}