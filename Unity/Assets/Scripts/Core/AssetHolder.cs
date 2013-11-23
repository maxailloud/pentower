using UnityEngine;
using System.Collections;

internal enum SFX
{
	None = -1,
	// TODO
}

[AddComponentMenu("Pentower/Core/AssetHolder")]
public class AssetHolder : MonoBehaviour
{
	#region Tanks
	public Unit tankPrefab;
	#endregion // Tanks

	#region Sfx & Bgm
	public AudioClip menuMusic;
	public AudioClip inGameMusic;
	public AudioClip[] SFXs;
	#endregion // SFX & BGM
}

