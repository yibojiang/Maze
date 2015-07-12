using UnityEngine;
using System.Collections;


public class SingletonMonoBehaviourClass<T> : MonoBehaviour where T : Object , new()
{
	private static T s_Instance = default(T);
 
    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static T instance {
        get {
            if (s_Instance == null) {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance =  FindObjectOfType(typeof(T)) as T;
            }
            
            // If it is still null, create a new instance
            if (s_Instance == null) {
                GameObject obj = new GameObject(typeof(T).ToString());
                s_Instance = obj.AddComponent(typeof(T)) as T;
				DontDestroyOnLoad(obj);
            }
            
            return s_Instance;
        }
    }
	
	public static void Release()
	{
		if(s_Instance!=null)
		{
			MonoBehaviour component = s_Instance as MonoBehaviour;
			Destroy(component.gameObject);
			s_Instance = null;
		}
	}
    
    // Ensure that the instance is destroyed when the game is stopped in the editor.
    virtual protected void OnApplicationQuit() {
        s_Instance = null;
    }
}


