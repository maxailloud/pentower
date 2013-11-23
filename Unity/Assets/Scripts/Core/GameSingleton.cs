using UnityEngine;
using System.Collections;

[AddComponentMenu("Pentower/Core/Game Singleton")]
public class GameSingleton : MonoBehaviour
{
	#region Singleton
	public static GameSingleton Instance {
		get {
			GameObject go = null;
			if (s_instance == null) {
				go = GameObject.Find ("GameSingleton");
				if (go == null)
					go = new GameObject ("GameSingleton");
				s_instance = go.AddComponent<GameSingleton> ();

				// Mark this game object as persistent between scenes.
				DontDestroyOnLoad (go);
			}
			return s_instance;
		}
	}
	private static GameSingleton s_instance = null;
	#endregion // Singleton

	#region Properties
	public AssetHolder assetHolder
	{
		get;
		private set;
	}
	public GameConfig config
	{
		get;
		private set;
	}
	public GameContext context
	{
		get;
		private set;
	}
	public Menu menu
	{
		get;
		private set;
	}
	#endregion // Properties
	
	void Awake ()
	{
		// Get the game config.
		this.config = (GameConfig) Resources.Load("GameConfig", typeof(GameConfig));
		// Get the game context.
		this.context = GetComponent<GameContext>();
		// Get the menu.
		this.menu = GetComponent<Menu>();
		// Get the asset holder
		this.assetHolder = (AssetHolder) Resources.Load ("AssetHolder", typeof(AssetHolder));
	}
	
	void Start ()
	{
		// Nothing to do.
		// Need to kept this method to ensure OnDestroy() will be called upon destruction.
	}
	
	void OnDestroy ()
	{
		// Release references
		GameSingleton.s_instance = null;

		StopAllCoroutines ();
	}
}
