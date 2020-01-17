using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Tester.src.Common.Tasks;
using Tester.src.Common.Tasks.CompositeTasks;
using Tester.src.Common.Tasks.CompositeTasks.Observer;
using Tester.src.Microservices.Listener.Result;

namespace Tester.src.Microservices.Listener
{
    class ListenerTask : AbstractObservableTask<ListenerDataSet, long>
    {
        private TcpClient connectionSocket;
        private volatile int timeElapsed = 0;

        public ListenerTask(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }

        public override CompositeTaskType GetTaskType()
        {
            return CompositeTaskType.SYNCHRONOUS;
        }

        public override void Run()
        {
            UpdaterTask updaterTask = updaterTask = new UpdaterTask(() =>
            {
                UpdateSharedElement(timeElapsed);

                int delay = 100;

                timeElapsed += delay;
                Task.Delay(delay).Wait();
            });
            updaterTask.StartPaused();

            NetworkStream stream = connectionSocket.GetStream();
            //stream.Write(Encoding.ASCII.GetBytes("ready"));

            string testName = "Could not find test name!";

            byte[] dataBuffer = new byte[1024];
            while (connectionSocket.Connected)
            {
                int bytesReceived = stream.Read(dataBuffer);
                string message = Encoding.ASCII.GetString(dataBuffer, 0, bytesReceived);

                if (message.Contains("start"))
                {
                    string[] splitMessage = message.Split(';');
                    testName = splitMessage[1];

                    Console.WriteLine("Starting monitor for " + testName + "...");
                    NotifyObservers(State.ALIVE);
                    updaterTask.UnPause();
                }
                else if (message.Contains("reset"))
                {
                    updaterTask.Pause();
                    timeElapsed = 0;

                    if (message.Contains("failed"))
                    {
                        ReportBack(new ListenerDataSet(testName), State.FAILED);
                    }
                    else
                    {
                        ReportBack(new ListenerDataSet(testName), State.RESET);
                    }
                }
                else if (message.Contains("stop"))
                {
                    updaterTask.Cancel();
                    Console.WriteLine("Stopped at test " + testName);

                    if (message.Contains("failed"))
                    {
                        ReportBack(new ListenerDataSet(testName), State.FAILED);
                        ReportBack(new ListenerDataSet(testName), State.DEAD);
                    }
                    else
                    {
                        ReportBack(new ListenerDataSet(testName), State.DEAD);
                    }

                    break;
                }
            }
        }
    }
}
