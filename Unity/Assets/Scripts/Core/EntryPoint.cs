using UnityEngine;
using System.Collections;

public enum SceneMode
{
	/// <summary>
	/// No scene mode.
	/// </summary>
	None = 0,
	/// <summary>
	/// Title scene with a splash screen.
	/// </summary>
	Title = 1,
	/// <summary>
	/// Menu scene with game mode selection.
	/// </summary>
	Menu = 3,
	/// <summary>
	/// Main game scene.
	/// </summary>
	Arena = 4,
	/// <summary>
	/// Intermediary scene where resources are loaded.
	/// </summary>
	Loading = 5,
	Credits = 6,
}

[AddComponentMenu("Pentower/Core/Entry Point")]
public class EntryPoint : MonoBehaviour
{
	public SceneMode sceneMode = SceneMode.None;

	void Awake ()
	{
		// Get the game singleton to make sure it is initialized here in the first scene
		var singleton = GameSingleton.Instance;
	}
	
	IEnumerator Start ()
	{
		// Get the game singleton
		var singleton = GameSingleton.Instance;

		switch (this.sceneMode) {
		case SceneMode.Title:
			// Show splash screen
			yield return new WaitForSeconds (2.0f);
			// Go to the menu
			Application.LoadLevel ("MenuScene");
			break;

		case SceneMode.Menu:
			// Enable the menu
			singleton.menu.state = MenuState.MainMenu;
			break;

		case SceneMode.Arena:
			this.gameObject.AddComponent<CycleScript> ();
			// Disable the menu
			singleton.menu.state = MenuState.None;
			// Add HUD
			Instantiate (singleton.assetHolder.hudCamera);
			singleton.context.PlayAmbiance(singleton.assetHolder.inGameMusic);
			break;

		case SceneMode.Loading:
			break;
		default:
			break;
		}
	}

	void OnLevelWasLoaded ()
	{
		Random.seed = Mathf.RoundToInt (Time.realtimeSinceStartup);
	}

	void OnGUI ()
	{
		// TODO
	}
	
	void OnDestroy ()
	{
		// Add cleanup here...
	}
}
