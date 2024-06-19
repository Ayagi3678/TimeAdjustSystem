using System.Linq;
using UnityEngine;

namespace TimeAdjustSystem.Internal
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    internal sealed class UpdateDispatcher : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            instance = new GameObject(nameof(UpdateDispatcher)).AddComponent<UpdateDispatcher>();
            DontDestroyOnLoad(instance);
        }

        static UpdateDispatcher instance;

        readonly UpdateRunner updateRunner = new(ex => Debug.LogException(ex));

        public static void Register(TimeRequest item)
        {
            instance.updateRunner.Add(item);
        }

        void LateUpdate()
        {
            if (updateRunner.Items.Length == 0)
            {
                Time.timeScale = 1;
                return;
            }
            var current = updateRunner.Items[0];
            for (var index = 1; index < updateRunner.Items.Length; index++)
            {
                var item = updateRunner.Items[index];
                if (item == null) continue;
                if (current.Priority > item.Priority) continue;
                current = item;
            }

            if (current == null)
            {
                Time.timeScale = 1;
                return;
            }
            Time.timeScale = current.TimeScale;
            updateRunner.Run();
        }
    }
}