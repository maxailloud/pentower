using UnityEngine;
using System.Collections;

public enum MenuState
{
	None = -1,
	MainMenu,
	Game,
	Pause,
	Credits,
}

[AddComponentMenu("Pentower/Menu/Menu")]
public class Menu : MonoBehaviour
{
	public MenuState state = MenuState.None;

	#region Cached References
	private AssetHolder m_assetHolder;
	private GameContext m_context;
	#endregion // Cached References

	void Awake()
	{
	}

	void Start()
	{
		// Get the asset holder and cache the reference
		this.m_assetHolder = GameSingleton.Instance.assetHolder;
		// Get the game context and cache the reference
		this.m_context = GameSingleton.Instance.context;
	}

	void OnDestroy()
	{
		this.m_assetHolder = null;
		this.m_context= null;
	}

	void Update()
	{
		switch (this.state)
		{
		case MenuState.None:
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				this.state = MenuState.Pause;
				if (this.m_context.gameMode == GameMode.Solo)
				{
					Time.timeScale = 0.0f;
				}
			}
			break;

		case MenuState.Pause:
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				this.state = MenuState.None;
				if (this.m_context.gameMode == GameMode.Solo)
				{
					Time.timeScale = 1.0f;
				}
			}
			break;
		}
	}

	#region GUI
	void OnGUI()
	{
		switch (this.state)
		{
		case MenuState.MainMenu:
			Menu_Main();
			break;

		case MenuState.Game:
			// Nothing to do
			break;

		case MenuState.Pause:
			switch (this.m_context.gameMode)
			{
			case GameMode.Multi:
				Menu_Pause_Multi();
				break;

			default:
			case GameMode.Solo:
				Menu_Pause_Solo();
				break;
			}
			break;

		case MenuState.Credits:
			Menu_Credits();
			break;

		default:
		case MenuState.None:
			break;
		}
	}

	private void Menu_Main()
	{
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		if (GUILayout.Button("Jouer"))
		{
			Application.LoadLevel("ArenaScene");
		}
		if (GUILayout.Button("Crédits"))
		{
			Application.LoadLevel("CreditsScene");
		}

		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
	}

	private void Menu_Pause_Multi()
	{
		// Nothing for now
	}

	private void Menu_Pause_Solo()
	{
		if (GUILayout.Button("Resume"))
		{
			Time.timeScale = 1.0f;
			this.state = MenuState.None;
		}
		if (GUILayout.Button("Restart"))
		{
			Application.LoadLevel(Application.loadedLevel);
			this.state = MenuState.None;
		}
		if (GUILayout.Button("Main Menu"))
		{
			Application.LoadLevel("MenuScene");
			this.state = MenuState.None;
		}
	}

	private void Menu_Credits()
	{
		// TODO
	}
	#endregion // GUI
}

