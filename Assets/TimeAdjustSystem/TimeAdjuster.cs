using Cysharp.Threading.Tasks;
using TimeAdjustSystem.Internal;

namespace TimeAdjustSystem
{
    public static class TimeAdjuster
    {
        public static void Subscribe(int priority, float timeScale, UniTask task)=>Subscribe(new TimeRequest(priority, timeScale, task));
        public static void Subscribe(TimeRequest timeRequest)
        {
            UpdateDispatcher.Register(timeRequest);
        }
    }
}