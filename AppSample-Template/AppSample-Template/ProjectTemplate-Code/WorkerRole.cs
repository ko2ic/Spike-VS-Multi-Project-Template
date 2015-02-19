using Microsoft.WindowsAzure.ServiceRuntime;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using $safeprojectname$.Dto;

namespace $safeprojectname$
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("$safeprojectname$ is running");

            try
            {
                var dto = new SampleDto().Dto;
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // 同時接続の最大数を設定します
            ServicePointManager.DefaultConnectionLimit = 12;

            // 構成の変更を処理する方法については、
            // MSDN トピック (http://go.microsoft.com/fwlink/?LinkId=166357) を参照してください。

            bool result = base.OnStart();

            Trace.TraceInformation("$safeprojectname$ has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("$safeprojectname$ is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("$safeprojectname$ has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: 次のロジックを自分で作成したロジックに置き換えてください。
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
