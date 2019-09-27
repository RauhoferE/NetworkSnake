using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NetworkLibrary
{
    public static class NetworkSerealizer
    {
        /// <summary>
        /// The header for every message.
        /// </summary>
        public static readonly string Header = "7T6Ru2kd";

        public static byte[] SerealizeMessage(MessageContainer e)
        {
            MessageTypePrintString messageTypePrintString = new MessageTypePrintString();
            byte[] headerEnc = Encoding.UTF8.GetBytes(Header);
            List<byte> message = new List<byte>();

            byte[] hostEnc = Encoding.UTF8.GetBytes("null");

            try
            {
                using (var ms = new MemoryStream())
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    writer.Serialize(ms, e);
                    message = ms.ToArray().ToList();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error couldnt serealize the message.");
            }

            List<byte> finalMessage = new List<byte>();

            finalMessage.AddRange(headerEnc);

            byte[] messageLength = new byte[100];

            for (int i = 0; i < BitConverter.GetBytes(hostEnc.Length + message.Count + 2).Length; i++)
            {
                messageLength[i] = BitConverter.GetBytes(hostEnc.Length + message.Count + 2)[i];
            }

            finalMessage.AddRange(messageLength);

            finalMessage.Add((byte)messageTypePrintString.Id);

            finalMessage.Add((byte)hostEnc.Length);

            finalMessage.AddRange(hostEnc);

            finalMessage.AddRange(message);

            finalMessage.Add(CalculateCheckSum(finalMessage.ToArray()));

            return finalMessage.ToArray();
        }

        public static byte[] SerealizeErrorMessage(MessageContainer e)
        {
            MessageTypePrintErrorMessage messageTypePrintError = new MessageTypePrintErrorMessage();
            byte[] headerEnc = Encoding.UTF8.GetBytes(Header);
            List<byte> message = new List<byte>();
            byte[] hostEnc = Encoding.UTF8.GetBytes("null");

            try
            {
                using (var ms = new MemoryStream())
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    writer.Serialize(ms, e);
                    message = ms.ToArray().ToList();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error couldnt serealize the message.");
            }

            List<byte> finalMessage = new List<byte>();

            finalMessage.AddRange(headerEnc);

            byte[] messageLength = new byte[100];

            for (int i = 0; i < BitConverter.GetBytes(hostEnc.Length + message.Count + 2).Length; i++)
            {
                messageLength[i] = BitConverter.GetBytes(hostEnc.Length + message.Count + 2)[i];
            }

            finalMessage.AddRange(messageLength);

            finalMessage.Add((byte)messageTypePrintError.Id);

            finalMessage.Add((byte)hostEnc.Length);

            finalMessage.AddRange(hostEnc);

            finalMessage.AddRange(message);

            finalMessage.Add(CalculateCheckSum(finalMessage.ToArray()));

            return finalMessage.ToArray();
        }

        public static byte[] SerealizeGameInfo(GameInformationContainer e)
        {
            MessageTypePrintInformation messageTypePrintError = new MessageTypePrintInformation();
            byte[] headerEnc = Encoding.UTF8.GetBytes(Header);
            List<byte> message = new List<byte>();
            byte[] hostEnc = Encoding.UTF8.GetBytes("null");

            try
            {
                using (var ms = new MemoryStream())
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    writer.Serialize(ms, e);
                    message = ms.ToArray().ToList();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error couldnt serealize the message.");
            }

            List<byte> finalMessage = new List<byte>();

            finalMessage.AddRange(headerEnc);

            byte[] messageLength = new byte[100];

            for (int i = 0; i < BitConverter.GetBytes(hostEnc.Length + message.Count + 2).Length; i++)
            {
                messageLength[i] = BitConverter.GetBytes(hostEnc.Length + message.Count + 2)[i];
            }

            finalMessage.AddRange(messageLength);

            finalMessage.Add((byte)messageTypePrintError.Id);

            finalMessage.Add((byte)hostEnc.Length);

            finalMessage.AddRange(hostEnc);

            finalMessage.AddRange(message);

            finalMessage.Add(CalculateCheckSum(finalMessage.ToArray()));

            return finalMessage.ToArray();
        }

        public static byte[] SerealizeField(FieldPrintContainer e)
        {
            MessageTypePrintField messageTypePrintError = new MessageTypePrintField();
            byte[] headerEnc = Encoding.UTF8.GetBytes(Header);
            List<byte> message = new List<byte>();
            byte[] hostEnc = Encoding.UTF8.GetBytes("null");

            try
            {
                using (var ms = new MemoryStream())
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    writer.Serialize(ms, e);
                    message = ms.ToArray().ToList();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error couldnt serealize the message.");
            }

            List<byte> finalMessage = new List<byte>();

            finalMessage.AddRange(headerEnc);

            byte[] messageLength = new byte[100];

            for (int i = 0; i < BitConverter.GetBytes(hostEnc.Length + message.Count + 2).Length; i++)
            {
                messageLength[i] = BitConverter.GetBytes(hostEnc.Length + message.Count + 2)[i];
            }

            finalMessage.AddRange(messageLength);

            finalMessage.Add((byte)messageTypePrintError.Id);

            finalMessage.Add((byte)hostEnc.Length);

            finalMessage.AddRange(hostEnc);

            finalMessage.AddRange(message);

            finalMessage.Add(CalculateCheckSum(finalMessage.ToArray()));

            return finalMessage.ToArray();
        }

        public static byte[] SerealizeGameObjects(ObjectListContainer e)
        {
            MessageTypePrintObject messageTypePrintError = new MessageTypePrintObject();
            byte[] headerEnc = Encoding.UTF8.GetBytes(Header);
            List<byte> message = new List<byte>();
            byte[] hostEnc = Encoding.UTF8.GetBytes("null");

            try
            {
                using (var ms = new MemoryStream())
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    writer.Serialize(ms, e);
                    message = ms.ToArray().ToList();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error couldnt serealize the message.");
            }

            List<byte> finalMessage = new List<byte>();

            finalMessage.AddRange(headerEnc);

            byte[] messageLength = new byte[100];

            for (int i = 0; i < BitConverter.GetBytes(hostEnc.Length + message.Count + 2).Length; i++)
            {
                messageLength[i] = BitConverter.GetBytes(hostEnc.Length + message.Count + 2)[i];
            }

            finalMessage.AddRange(messageLength);

            finalMessage.Add((byte)messageTypePrintError.Id);

            finalMessage.Add((byte)hostEnc.Length);

            finalMessage.AddRange(hostEnc);

            finalMessage.AddRange(message);

            finalMessage.Add(CalculateCheckSum(finalMessage.ToArray()));

            return finalMessage.ToArray();
        }

        public static byte[] SerealizeMoveSnake(MoveSnakeContainer e)
        {
            MessageTypeMoveSnake messageTypePrintError = new MessageTypeMoveSnake();
            byte[] headerEnc = Encoding.UTF8.GetBytes(Header);
            List<byte> message = new List<byte>();
            byte[] hostEnc = Encoding.UTF8.GetBytes("null");

            try
            {
                using (var ms = new MemoryStream())
                {
                    BinaryFormatter writer = new BinaryFormatter();
                    writer.Serialize(ms, e);
                    message = ms.ToArray().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error couldnt serealize the message." + ex);
            }

            List<byte> finalMessage = new List<byte>();

            finalMessage.AddRange(headerEnc);

            byte[] messageLength = new byte[100];

            for (int i = 0; i < BitConverter.GetBytes(hostEnc.Length + message.Count + 2).Length; i++)
            {
                messageLength[i] = BitConverter.GetBytes(hostEnc.Length + message.Count + 2)[i];
            }

            finalMessage.AddRange(messageLength);

            finalMessage.Add((byte)messageTypePrintError.Id);

            finalMessage.Add((byte)hostEnc.Length);

            finalMessage.AddRange(hostEnc);

            finalMessage.AddRange(message);

            finalMessage.Add(CalculateCheckSum(finalMessage.ToArray()));

            return finalMessage.ToArray();
        }

        public static byte[] SerealizePing()
        {
            MessageTypePing ping = new MessageTypePing();

            byte[] headerEnc = Encoding.UTF8.GetBytes(Header);
            List<byte> message = new List<byte>();

            byte[] messageLength = new byte[100];

            for (int i = 0; i < BitConverter.GetBytes(1).Length; i++)
            {
                messageLength[i] = BitConverter.GetBytes(1)[i];
            }

            message.AddRange(headerEnc);
            message.AddRange(messageLength);

            message.Add((byte)ping.Id);

            message.Add(CalculateCheckSum(message.ToArray()));

            return message.ToArray();
        }

        /// <summary>
        /// This method gets the calculated checksum.
        /// </summary>
        /// <param name="array"> The message array. </param>
        /// <returns> It returns a byte. </returns>
        public static byte CalculateCheckSum(byte[] array)
        {
            int firstChecksum = 1;

            int secondChecksum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                firstChecksum = (firstChecksum + array[i]) % 255;
                secondChecksum = (secondChecksum + firstChecksum) % 255;
            }

            return (byte)((secondChecksum << 8) | firstChecksum);
        }
    }
}