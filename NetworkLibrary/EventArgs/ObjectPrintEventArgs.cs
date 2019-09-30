//-----------------------------------------------------------------------
// <copyright file="ObjectPrintEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="ObjectPrintEventArgs"/> class.
    /// </summary>
    public class ObjectPrintEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPrintEventArgs"/> class.
        /// </summary>
        /// <param name="container"> The <see cref="ObjectListContainer"/>. </param>
        public ObjectPrintEventArgs(ObjectListContainer container)
        {
            this.ObjectPrintContainer = container;
        }

        /// <summary>
        /// Gets the <see cref="ObjectListContainer"/>.
        /// </summary>
        /// <value> A <see cref="ObjectListContainer"/> object. </value>
        public ObjectListContainer ObjectPrintContainer
        {
            get;
        }
    }
}