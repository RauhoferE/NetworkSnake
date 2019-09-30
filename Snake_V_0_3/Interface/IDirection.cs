//-----------------------------------------------------------------------
// <copyright file="IDirection.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace Snake_V_0_3
{
    /// <summary>
    /// Gets the <see cref="IDirection"/> interface.
    /// </summary>
    public interface IDirection
    {
        /// <summary>
        /// Gets the id of the direction.
        /// </summary>
        /// <value> A normal integer. </value>
        int ID { get; }

        /// <summary>
        /// Gets the description of the direction.
        /// </summary>
        /// <value> A normal string. </value>
        string Name { get; }
    }
}