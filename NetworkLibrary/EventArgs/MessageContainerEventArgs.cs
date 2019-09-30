//-----------------------------------------------------------------------
// <copyright file="MessageContainerEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="MessageContainerEventArgs"/> class.
    /// </summary>
    public class MessageContainerEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageContainerEventArgs"/> class.
        /// </summary>
        /// <param name="messageContainer"> The <see cref="MessageContainer"/>. </param>
        public MessageContainerEventArgs(MessageContainer messageContainer)
        {
            this.MessageContainer = messageContainer;
        }
        
        /// <summary>
        /// Gets the <see cref="MessageContainer"/>.
        /// </summary>
        /// <value> A <see cref="MessageContainer"/> object. </value>
        public MessageContainer MessageContainer
        {
            get;
        }
    }
}