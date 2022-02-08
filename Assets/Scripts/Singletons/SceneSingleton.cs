using UnityEngine;

namespace BGGames.Core
{
    /// <summary>
    /// This class implements the Singleton pattern.
    /// Instance is only available in the call scene.
    /// </summary>
    /// <typeparam name="T">Inheritance class.</typeparam>
    public class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
    {
        public static T Instance;

        public virtual void Awake()
        {
            if (Instance == null)
                Instance = (T)this;
            else
                Destroy(gameObject);
        }


        public virtual void Update()
        {

        }

        public virtual void Start()
        {

        }
    }


}