//-----------------------------------------------------------------------
// <copyright file="MessageTypeMoveSnake.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace NetworkLibrary
{
    /// <summary>
    /// The <see cref="MessageTypeMoveSnake"/> class.
    /// </summary>
    public class MessageTypeMoveSnake : IMessageType
    {
        /// <summary>
        /// Gets the id of the message.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Id
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the description of the message.
        /// </summary>
        /// <value> A normal string. </value>
        public string Descreption
        {
            get
            {
                return "A message type for movement of the snake.";
            }
        }
    }
}