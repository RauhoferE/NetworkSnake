//-----------------------------------------------------------------------
// <copyright file="NetworkDeSerealizer.cs" company="FH Wiener Neustadt">
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
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// The <see cref="NetworkDeSerealizer"/> class.
    /// </summary>
    public static class NetworkDeSerealizer
    {
        /// <summary>
        /// This method converts the byte stream to an <see cref="MessageContainer"/>.
        /// </summary>
        /// <param name="e"> The byte stream. </param>
        /// <returns> It returns an instance of the <see cref="MessageContainer"/>. </returns>
        public static MessageContainer DeSerealizedMessage(byte[] e)
        {
            MessageContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    MessageContainer deserealizedContainer = (MessageContainer)writer.Deserialize(s);
                    container = deserealizedContainer;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            return container;
        }

        /// <summary>
        /// This method converts the byte stream to an <see cref="FieldPrintContainer"/>.
        /// </summary>
        /// <param name="e"> The byte stream. </param>
        /// <returns> It returns an instance of the <see cref="FieldPrintContainer"/>. </returns>
        public static FieldPrintContainer DeSerealizedFieldMessage(byte[] e)
        {
            FieldPrintContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    FieldPrintContainer deserealizedContainer = (FieldPrintContainer)writer.Deserialize(s);
                    container = deserealizedContainer;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            return container;
        }

        /// <summary>
        /// This method converts the byte stream to an <see cref="GameInformationContainer"/>.
        /// </summary>
        /// <param name="e"> The byte stream. </param>
        /// <returns> It returns an instance of the <see cref="GameInformationContainer"/>. </returns>
        public static GameInformationContainer DeSerealizedGameInfo(byte[] e)
        {
            GameInformationContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    GameInformationContainer deserealizedContainer = (GameInformationContainer)writer.Deserialize(s);
                    container = deserealizedContainer;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            return container;
        }

        /// <summary>
        /// This method converts the byte stream to an <see cref="ObjectListContainer"/>.
        /// </summary>
        /// <param name="e"> The byte stream. </param>
        /// <returns> It returns an instance of the <see cref="ObjectListContainer"/>. </returns>
        public static ObjectListContainer DeSerealizedObjectList(byte[] e)
        {
            ObjectListContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    ObjectListContainer deserealizedContainer = (ObjectListContainer)writer.Deserialize(s);
                    container = deserealizedContainer;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            return container;
        }

        /// <summary>
        /// This method converts the byte stream to an <see cref="MoveSnakeContainer"/>.
        /// </summary>
        /// <param name="e"> The byte stream. </param>
        /// <returns> It returns an instance of the <see cref="MoveSnakeContainer"/>. </returns>
        public static MoveSnakeContainer DeSerealizeSnakeMovement(byte[] e)
        {
            MoveSnakeContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    MoveSnakeContainer deserealizedContainer = (MoveSnakeContainer)writer.Deserialize(s);
                    container = deserealizedContainer;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            return container;
        }
    }
}