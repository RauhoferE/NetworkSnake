using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NetworkLibrary
{
    public static class NetworkDeSerealizer
    {
        public static MessageContainer DeSerealizedMessage(byte[] e)
        {
            MessageContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    MessageContainer deSerealizedContainer = (MessageContainer)writer.Deserialize(s);
                    container = deSerealizedContainer;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            return container;
        }

        public static FieldPrintContainer DeSerealizedFieldMessage(byte[] e)
        {
            FieldPrintContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    FieldPrintContainer deSerealizedContainer = (FieldPrintContainer)writer.Deserialize(s);
                    container = deSerealizedContainer;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            return container;
        }

        public static GameInformationContainer DeSerealizedGameInfo(byte[] e)
        {
            GameInformationContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    GameInformationContainer deSerealizedContainer = (GameInformationContainer)writer.Deserialize(s);
                    container = deSerealizedContainer;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            return container;
        }

        public static ObjectListContainer DeSerealizedObjectList(byte[] e)
        {
            ObjectListContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    ObjectListContainer deSerealizedContainer = (ObjectListContainer)writer.Deserialize(s);
                    container = deSerealizedContainer;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            return container;
        }

        public static MoveSnakeContainer DeSerealizeSnakeMovement(byte[] e)
        {
            MoveSnakeContainer container;

            try
            {
                using (var s = new MemoryStream(e))
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    MoveSnakeContainer deSerealizedContainer = (MoveSnakeContainer)writer.Deserialize(s);
                    container = deSerealizedContainer;
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