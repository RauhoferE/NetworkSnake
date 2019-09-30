//-----------------------------------------------------------------------
// <copyright file="MoveSnakeUp.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="MoveSnakeUp"/> class.
    /// </summary>
    [Serializable]
    public class MoveSnakeUp : IInputType
    {
        /// <summary>
        /// Gets the id of the movement.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Id
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the description of the message.
        /// </summary>
        /// <value> A normal string. </value>
        public string Description
        {
            get
            {
                return "Move Up";
            }
        }
    }
}