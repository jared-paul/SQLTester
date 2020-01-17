using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.src.Microservices.Listener
{
    class UpdaterTask
    {
        private CancellationTokenSource tokenSource;
        private CancellationToken token;
        private Action action;
        EventWaitHandle waitHandle = new ManualResetEvent(false);

        public UpdaterTask(Action action)
        {
            this.tokenSource = new CancellationTokenSource();
            this.token = tokenSource.Token;
            this.action = action;
        }

        public void StartPaused()
        {
            Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    waitHandle.WaitOne();

                    action();
                }
            }, token);
        }

        public void Pause()
        {
            waitHandle.Reset();
        }

        public void UnPause()
        {
            waitHandle.Set();
        }

        public void Cancel()
        {
            this.tokenSource.Cancel();
        }
    }
}
