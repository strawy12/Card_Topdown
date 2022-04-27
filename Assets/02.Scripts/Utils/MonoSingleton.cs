using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T inst;
    private static bool shuttingDown = false;
    private static object locker = new object();

    public static T Inst
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance " + typeof(T) + " already destroyed. Returning null.");
            }

            lock (locker)
            {
                if (inst == null)
                {
                    inst = FindObjectOfType<T>();
                    if (inst == null)
                    {
                        inst = new GameObject(typeof(T).ToString()).AddComponent<T>();
                    }
                }
                return inst;
            }
        }
    }
    private void OnDestroy()
    {
        shuttingDown = true;
    }

    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }
}
