using UnityEngine;


// 实现普通的单例模式
public abstract class Singleton<T> where T : new()
{
    private static T instance;
    private static object mutex = new object();
    public static T Instance
    {
        get
        {
            if (instance != null)
                return instance;

            lock (mutex) // 保证线程安全
            {
                instance ??= new T();
            }
            return instance;
        }
    }
}

// MonoBehavior:声音，网络
// Unity单例，不考虑多线程
public class UnitySingleton<T> : MonoBehaviour
    where T : Component
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType(typeof(T)) as T;
            if (instance != null)
                return instance;

            var obj = new GameObject();
            instance = (T)obj.AddComponent(typeof(T));
            obj.hideFlags = HideFlags.DontSave;
            obj.name = typeof(T).Name;

            return instance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
