using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;

    public virtual void Awake()
    {
        if (Application.isPlaying)
            DontDestroyOnLoad(gameObject);
    }
}
