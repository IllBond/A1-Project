using System.Collections;
using System.Collections.Generic;
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
