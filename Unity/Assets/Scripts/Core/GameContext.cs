using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameMode
{
	Solo,
	Multi // Ignored for now
}

[AddComponentMenu("Pentower/Core/Game Context")]
[RequireComponent(typeof(NetworkView))]
public class GameContext : MonoBehaviour
{
	public GameMode gameMode = GameMode.Solo;

	#region Audio
	internal const int AUDIOSRC_AMB = 0;
	internal const int AUDIOSRC_SFX = 0;

	public AudioListener audioListener {
		get;
		private set;
	}

	private AudioSource[] m_bgmAudioSourceArray = null;
	#endregion // Audio

	#region Players
	public Player[] players;
	public Player currentPlayer;
	#endregion // Players

	private int m_nLastLevelPrefix = 0;

	void Awake ()
	{
		DontDestroyOnLoad (this);

		this.m_bgmAudioSourceArray = new AudioSource[2];
		// Add an audio source for BGM music
		this.m_bgmAudioSourceArray[AUDIOSRC_AMB] = gameObject.AddComponent<AudioSource>();
		this.m_bgmAudioSourceArray[AUDIOSRC_AMB].playOnAwake = false;
		// Add an audio source for SFXs
		this.m_bgmAudioSourceArray[AUDIOSRC_SFX] = gameObject.AddComponent<AudioSource>();
		this.m_bgmAudioSourceArray[AUDIOSRC_SFX].playOnAwake = false;

		// Get players at startup if the first scene loaded is Arena Scene
		this.players = FindObjectsOfType(typeof(Player)) as Player[];
	}

	void OnDestroy ()
	{
		this.audioListener = null;
		this.m_bgmAudioSourceArray = null;
	}

	void OnLevelWasLoaded (int level)
	{
		// Force fullscreen clearing (color AND depth)
		GL.Clear (true, true, Color.black);
		InitializeAudioListener ();
		// Get players
		this.players = FindObjectsOfType(typeof(Player)) as Player[];
	}

	#region Audio Management
	private void InitializeAudioListener ()
	{
		this.audioListener = (AudioListener)FindObjectOfType (typeof(AudioListener));
		if (this.audioListener)
		{
			AudioSource snd = GetAudioSource (AUDIOSRC_AMB);
			if (snd != null) {
				// TODO: set volume based on GameConfig
			}
			// TODO: set SFX volume
		}
	}
	
	internal AudioSource GetAudioSource (int nNumAudioSource)
	{
		if (this.m_bgmAudioSourceArray != null && this.m_bgmAudioSourceArray.Length > nNumAudioSource)
			return this.m_bgmAudioSourceArray [nNumAudioSource];
		return null;
	}
	
	// Ambiance Management
	internal void PlayAmbiance (AudioClip clip)
	{
		// No Clip
		if (clip == null) {
			return;
		}
		// Try to initialize the AudioListener
		if (!this.audioListener) {
			InitializeAudioListener ();
		}
		// No AudioListener found
		if (!this.audioListener) {
			Debug.LogError ("AudioListener not found !!!");
			return;
		}

		AudioSource snd = GetAudioSource (AUDIOSRC_AMB);
		if (snd != null && snd.clip != clip)
		{
			snd.ignoreListenerVolume = true;
			if( snd.isPlaying)
			{
				StartCoroutine (FadeAmbiance(snd, clip));
			}
			else
			{
				snd.Stop();
				snd.clip = clip;
				snd.loop = true;
				// TODO: set volume based on GameConfig
				snd.Play();
			}
		}
	}

	private IEnumerator FadeAmbiance (AudioSource snd, AudioClip clip)
	{
		float prevVolume = snd.volume;
		// FIXME: fade speed and fade min (GameConfig?)
		while( snd.volume > 0.05f )
		{
			snd.volume = Mathf.Lerp (snd.volume, 0.0f, 1.5f*Time.deltaTime);
			yield return null;
		}
		snd.Stop ();
		snd.clip = clip;
		snd.loop = true;
		snd.Play ();
		// FIXME: fade speed and fade min (GameConfig?)
		while( snd.volume < (prevVolume - 0.01f) )
		{
			snd.volume = Mathf.Lerp (snd.volume, prevVolume + 0.01f, 2.0f*Time.deltaTime);
			yield return null;
		}
		snd.volume = prevVolume;
	}

	internal void StopAmbiance ()
	{
		AudioSource snd = GetAudioSource (AUDIOSRC_AMB);
		if (snd) {
			snd.Stop ();
			snd.volume = 0;
		} else {
			Debug.Log ("audioSrc not set !!!");
		}
	}
	
	// SFX Management
	internal void PlaySFX (SFX sfx)
	{
		PlaySFX (GameSingleton.Instance.assetHolder.SFXs [(int)sfx]);
	}
	
	internal void PlaySFX (AudioClip clip)
	{
		if (clip != null) {
			AudioSource snd = GetAudioSource (AUDIOSRC_SFX);
			if (snd != null && !snd.isPlaying) {
				snd.clip = clip;
				snd.loop = false;
				snd.volume = AudioListener.volume;
				snd.Play ();
			}
		}
	}
	
	internal void PlayOneShotSFX (SFX sfx)
	{
		PlayOneShotSFX (sfx, 1.0f);
	}

	internal void PlayOneShotSFX (SFX sfx, float fPitch)
	{
		PlayOneShotSFX (sfx, 1.0f, 1.0f);
	}

	internal void PlayOneShotSFX (SFX sfx, float fPitch, float fVolume)
	{
		PlayOneShotSFX (GameSingleton.Instance.assetHolder.SFXs [(int)sfx], fPitch, fVolume);
	}

	internal void PlayOneShotSFX (AudioClip clip, float fPitch, float fVolume)
	{
		if (clip != null) {
			AudioSource snd = GetAudioSource (AUDIOSRC_SFX);
			if (snd != null) {
				snd.pitch = fPitch;
				snd.volume = fVolume;
				snd.PlayOneShot (clip);
			}
		}
	}
	#endregion // AudioManagement

	IEnumerator LoadStreamedLevel (string levelName, bool additive, bool testBeforeLoad)
	{
		if (testBeforeLoad) {
			while (!Application.CanStreamedLevelBeLoaded(levelName)) {
				yield return null;
			}
		}

		if (additive) {
#if UNITY_PRO
            AsyncOperation async = Application.LoadLevelAdditiveAsync(levelName);
            yield return async;
#else
			Application.LoadLevelAdditive (levelName);
#endif
		} else {
#if UNITY_PRO
            AsyncOperation async = Application.LoadLevelAsync(levelName);
            yield return async;
#else
			Application.LoadLevel (levelName);
#endif
		}
	}

	#region Event Handlers
	[RPC]
	IEnumerator LoadLevel (string levelName, int levelPrefix)
	{
		this.m_nLastLevelPrefix = levelPrefix;

		// There is no reason to send any more data over the network on the default channel,
		// because we are about to load the level, thus all those objects will get deleted anyway
		Network.SetSendingEnabled (0, false);

		// We need to stop receiving because the level must be loaded first.
		// Once the level is loaded, rpc's and other state update attached to objects in the level are allowed to fire
		Network.isMessageQueueRunning = false;

		// All network views loaded from a level will get a prefix into their NetworkViewID.
		// This will prevent old updates from clients leaking into a newly created scene.
		Network.SetLevelPrefix (levelPrefix);

		yield return StartCoroutine(LoadStreamedLevel(levelName, false, true));
		// Awake and Start are called on each objects, so wait one or two frames.
		yield return null;
		yield return null;

		// Allow receiving data again
		Network.isMessageQueueRunning = true;
		// Now the level has been loaded and we can start sending out data to clients
		Network.SetSendingEnabled (0, true);

		// Broadcast OnNetworkLevelLoaded event on all GameObject
		foreach (Object go in FindObjectsOfType(typeof(GameObject))) {
			(go as GameObject).SendMessage ("OnNetworkLevelLoaded", SendMessageOptions.DontRequireReceiver);
		}
	}
	#endregion // Event Handlers
}

