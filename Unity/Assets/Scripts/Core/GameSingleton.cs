using UnityEngine;
using System.Collections;

[AddComponentMenu("Pentower/Core/Game Singleton")]
public class GameSingleton : MonoBehaviour
{
	public static GameSingleton Instance
	{
		get
		{
			GameObject go = null;
			if (s_instance == null)
			{
				go = GameObject.Find("GameSingleton");
				if (go == null)
					go = new GameObject("GameSingleton");
				s_instance = go.AddComponent<GameSingleton>();

				// Mark this game object as persistent between scenes.
				DontDestroyOnLoad(go);
			}
			return s_instance;
		}
	}
	private static GameSingleton s_instance = null;
	
	void Awake()
	{
		// TODO
	}
	
	void Start()
	{
		// Nothing to do.
		// Need to kept this method to ensure OnDestroy() will be called upon destruction.
	}
	
	void OnDestroy()
	{
		// Release references
		GameSingleton.s_instance = null;

		StopAllCoroutines();
	}
}
