//-----------------------------------------------------------------------
// <copyright file="IMessageType.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="IMessageType"/> interface.
    /// </summary>
    public interface IMessageType
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value> A normal integer. </value>
        int Id
        {
            get;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value> A normal string. </value>
        string Descreption
        {
            get;
        }
    }
}