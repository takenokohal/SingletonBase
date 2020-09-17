using UnityEngine;

namespace Takenokohal.Scripts
{
    public abstract class SingletonObjectBase<T> : MonoBehaviour where T : SingletonObjectBase<T>
    {
        private static T _instance;

        private static string GetName() => typeof(T).Name;

        protected virtual void InitializeMethod()
        {
        }

        public abstract class AddComponentSingleton : SingletonObjectBase<T>
        {
            public static T Instance
            {
                get
                {
                    if (_instance != null)
                        return _instance;

                    var obj = new GameObject(GetName());
                    _instance = obj.AddComponent<T>();

                    _instance.InitializeMethod();
                    DontDestroyOnLoad(_instance.gameObject);

                    return _instance;
                }
            }
        }

        public abstract class InResourcesSingleton : SingletonObjectBase<T>
        {
            public static T Instance
            {
                get
                {
                    if (_instance != null)
                        return _instance;

                    var prefab = Resources.Load<T>(GetName());
                    _instance = Instantiate(prefab);
                    _instance.name = GetName();

                    _instance.InitializeMethod();

                    DontDestroyOnLoad(_instance.gameObject);

                    return _instance;
                }
            }
        }

        public abstract class InSceneSingleton : SingletonObjectBase<T>
        {
            public static T Instance
            {
                get
                {
                    if (_instance != null)
                        return _instance;

                    var obj = FindObjectOfType<T>();
                    _instance = obj;
                    _instance.InitializeMethod();

                    return _instance;
                }
            }
        }
    }
}