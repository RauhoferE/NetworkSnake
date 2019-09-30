//-----------------------------------------------------------------------
// <copyright file="MessageContainer.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="MessageContainer"/> class.
    /// </summary>
    [Serializable]
    public class MessageContainer
    {
        /// <summary>
        /// Represents the message.
        /// </summary>
        private string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageContainer"/> class.
        /// </summary>
        /// <param name="message"> The message. </param>
        public MessageContainer(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value> A normal string. </value>
        public string Message
        {
            get
            {
                return this.message;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Error value cant be null.");
                }

                this.message = value;
            }
        }
    }
}