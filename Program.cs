using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tester.src.Aggregator.Tasks;
using Tester.src.Common.Result;
using Tester.src.Common.Tasks;
using Tester.src.Microservices.Disk;
using Tester.src.Microservices.Listener;
using Tester.src.Microservices.Listener.Service;
using Tester.src.Microservices.Memory;

namespace Tester.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string serverIp = ConfigurationManager.AppSettings["serverIp"];
                int serverPort = int.Parse(ConfigurationManager.AppSettings["serverPort"]);

                TcpClient connectionSocket = new SocketService(IPAddress.Parse(serverIp), serverPort).EstablishConnection();

                ListenerTask listenerTask = new ListenerTask(connectionSocket);
                DiskTask diskTask = new DiskTask("sqlservr");
                MemoryTask memoryTask = new MemoryTask("sqlservr");

                EventWaitHandle waitHandle = new AutoResetEvent(false);

                TaskPool taskPool = new TaskPoolBuilder()
                    .SubmitCompositeTask(listenerTask)
                    .SubmitCompositeTask(diskTask).Observe(listenerTask)
                    //.SubmitCompositeTask(memoryTask).Observe(listenerTask)
                    .OnReturn(delegate (List<IDataSet> dataSets, State state)
                    {
                        lock (dataSets)
                        {
                            if (state == State.FAILED)
                            {
                                Console.WriteLine("Query failed");
                                return;
                            }
                            else if (state == State.DEAD)
                            {
                                waitHandle.Set();
                                return;
                            }


                            string jsonReport = new TimeDataReportBuilder(GetTimeDataSets(dataSets)).GenerateJsonReport();

                            if (jsonReport.Length > 0)
                            {
                                jsonReport += "}";
                            }

                            string jsonData = "{\"data\":[{},{" + jsonReport + "}]}";
                            NetworkStream stream = connectionSocket.GetStream();
                            stream.Write(Encoding.ASCII.GetBytes("lets get this thing started|" + jsonData));

                            if (state == State.RESET)
                            {
                                Console.WriteLine("Resetting monitor...");
                            }
                        }
                    })
                    .Build();

                waitHandle.WaitOne();
                Console.WriteLine("Recieved shutdown signal! shutting down...");
                connectionSocket.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception thrown when communicating with the server: " + exception.StackTrace);
            }
        }

        private static byte[][] ChunkString(string toChunk, int maxBytesInASCII)
        {
            int maxComponents = (Encoding.ASCII.GetByteCount(toChunk) / maxBytesInASCII);

            if (maxBytesInASCII % toChunk.Length != 0)
            {
                maxComponents++;
            }

            byte[][] chunked = new byte[maxComponents][];

            byte[] toChunkBytes = Encoding.ASCII.GetBytes(toChunk);

            int position = 0;
            int componentCount = 0;
            while (componentCount < maxComponents)
            {
                byte[] component = new byte[maxBytesInASCII];

                int byteCount = 0;
                while (byteCount < maxBytesInASCII)
                {
                    if (position >= toChunkBytes.Length)
                        break;

                    component[byteCount] = toChunkBytes[position];
                    byteCount++;
                    position++;
                }

                chunked[componentCount] = component;
                componentCount++;
            }

            return chunked;
        }

        private static byte[] AddOrder(int order, byte[] data)
        {
            return Encoding.ASCII.GetBytes(order + "?" + Encoding.ASCII.GetString(data));
        }

        private static List<ITimeDataSet> GetTimeDataSets(List<IDataSet> dataSets)
        {
            List<ITimeDataSet> timeDataSets = new List<ITimeDataSet>();

            foreach (IDataSet dataSet in dataSets)
            {
                if (dataSet is ITimeDataSet timeDataSet)
                {
                    timeDataSets.Add(timeDataSet);
                }
            }

            return timeDataSets;
        }
    }
}
