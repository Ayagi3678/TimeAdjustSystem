using Cysharp.Threading.Tasks;
namespace TimeAdjustSystem
{
    public sealed class TimeRequest
    {
        public readonly int Priority;
        public readonly float TimeScale;
        private readonly UniTask _task;
        public bool IsCompleted => _task.Status == UniTaskStatus.Succeeded || _task.Status == UniTaskStatus.Canceled || _task.Status == UniTaskStatus.Faulted;
        public TimeRequest(int priority, float timeScale, UniTask task)
        {
            Priority = priority;
            TimeScale = timeScale;
            _task = task;
        }
    }
}