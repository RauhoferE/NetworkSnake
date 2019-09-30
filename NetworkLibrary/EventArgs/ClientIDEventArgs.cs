//-----------------------------------------------------------------------
// <copyright file="ClientIDEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="ClientIDEventArgs"/> class.
    /// </summary>
    public class ClientIDEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientIDEventArgs"/> class.
        /// </summary>
        /// <param name="clientId"> The number of the client. </param>
        public ClientIDEventArgs(int clientId)
        {
            this.ClientID = clientId;
        }

        /// <summary>
        /// Gets the client id.
        /// </summary>
        /// <value> A normal integer. </value>
        public int ClientID
        {
            get;
        }
    }
}