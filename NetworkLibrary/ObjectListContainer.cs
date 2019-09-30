//-----------------------------------------------------------------------
// <copyright file="ObjectListContainer.cs" company="FH Wiener Neustadt">
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
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ObjectListContainer"/> class.
    /// </summary>
    [Serializable]
    public class ObjectListContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectListContainer"/> class.
        /// </summary>
        /// <param name="oldItems"> The old item list. </param>
        /// <param name="newItems"> The new item list.  </param>
        /// <param name="information"> The game info.  </param>
        public ObjectListContainer(List<ObjectPrintContainer> oldItems, List<ObjectPrintContainer> newItems, GameInformationContainer information)
        {
            this.OldItems = oldItems;
            this.NewItems = newItems;
            this.Information = information;
        }

        /// <summary>
        /// Gets the list of old items.
        /// </summary>
        /// <value> A list of <see cref="ObjectPrintContainer"/>. </value>
        public List<ObjectPrintContainer> OldItems
        {
            get;
        }

        /// <summary>
        /// Gets the list of new items.
        /// </summary>
        /// <value> A list of <see cref="ObjectPrintContainer"/>. </value>
        public List<ObjectPrintContainer> NewItems
        {
            get;
        }

        /// <summary>
        /// Gets the game info.
        /// </summary>
        /// <value> A normal <see cref="GameInformationContainer"/>. </value>
        public GameInformationContainer Information
        {
            get;
        }
    }
}