//-----------------------------------------------------------------------
// <copyright file="FieldMessageEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="FieldMessageEventArgs"/> class.
    /// </summary>
    public class FieldMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldMessageEventArgs"/> class.
        /// </summary>
        /// <param name="fieldPrintContainer"> The <see cref="FieldPrintContainer"/>. </param>
        public FieldMessageEventArgs(FieldPrintContainer fieldPrintContainer)
        {
            this.FieldPrintContainer = fieldPrintContainer;
        }

        /// <summary>
        /// Gets the <see cref="FieldPrintContainer"/>.
        /// </summary>
        /// <value> A normal <see cref="FieldPrintContainer"/> object. </value>
        public FieldPrintContainer FieldPrintContainer
        {
            get;
        }
    }
}