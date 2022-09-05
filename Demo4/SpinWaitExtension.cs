using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo4
{
    public static class SpinWaitExtension
    {
        public static async Task<bool> SpinUntilAsync(this SpinWait spinWait, Func<bool> condition, int millisecondsTimeout)
        {
            bool isCreateSuccess = false;
            await Task.Run(() =>
            {
                isCreateSuccess = SpinWait.SpinUntil(condition, millisecondsTimeout);
            });
            return isCreateSuccess;
        }
    }
}
