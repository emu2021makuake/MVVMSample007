using System;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMSample007.Models
{
    public class HeavyWorkModel
    {
        private CancellationTokenSource _cancelTokenSrc;

        public async Task HeavyWorkAsync(IProgress<ProgressInfo> progress)
        {
            using (_cancelTokenSrc = new CancellationTokenSource())
            {
                try
                {
                    for (int i = 0; i < 100; i++)
                    {
                        _cancelTokenSrc.Token.ThrowIfCancellationRequested();

                        await Task.Delay(50);

                        progress.Report(new ProgressInfo(i, $"{i + 1} cases processed."));
                    }

                    progress.Report(new ProgressInfo(0, "***** Done *****"));
                }
                catch (OperationCanceledException)
                {
                    progress.Report(new ProgressInfo(0, "***** Cancelled *****"));
                    return;
                }

            }
        }

        public void Cancel()
        {
            if (_cancelTokenSrc?.IsCancellationRequested == false)
            {
                _cancelTokenSrc.Cancel();
            }
            
        }
    }
}
