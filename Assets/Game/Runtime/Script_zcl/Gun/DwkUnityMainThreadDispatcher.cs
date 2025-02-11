
using System.Collections.Generic;

namespace UnA
{
    public class DwkUnityMainThreadDispatcher : UnitySingleton<DwkUnityMainThreadDispatcher>
    {
        private readonly Queue<System.Action> actions = new Queue<System.Action>();

        public void Init() { }

        public void Enqueue(System.Action action)
        {
            lock (actions)
            {
                actions.Enqueue(action);
            }
        }

        public void Update()
        {
            while (actions.Count > 0)
            {
                actions.Dequeue().Invoke();
            }
        }
    }
}



