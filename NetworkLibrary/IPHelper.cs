//-----------------------------------------------------------------------
// <copyright file="IPHelper.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace NetworkLibrary
{
    using System.Net;

    /// <summary>
    /// The <see cref="IPHelper"/> class.
    /// </summary>
    public static class IPHelper
    {
        /// <summary>
        /// This method checks if the string is an IP address.
        /// </summary>
        /// <param name="s"> The string. </param>
        /// <returns> It returns true if the strings is an address. </returns>
        public static bool IsIPAdress(string s)
        {
            IPAddress adress;

            if (IPAddress.TryParse(s, out adress))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method converts the string to an IP address.
        /// </summary>
        /// <param name="s"> The string. </param>
        /// <returns> It returns an IP endpoint. </returns>
        public static IPEndPoint GetIPAdress(string s)
        {
            return new IPEndPoint(IPAddress.Parse(s), 80); 
        }
    }
}