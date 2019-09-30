//-----------------------------------------------------------------------
// <copyright file="DateTimeReturner.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeServer
{
    using System;

    /// <summary>
    /// The <see cref="DateTimeReturner"/> class.
    /// </summary>
    public static class DateTimeReturner
    {
        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <returns> Returns the current time as string. </returns>
        public static string ReturnCurrentTime()
        {
            return DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        }
    }
}