using UnityEngine;
using System.Collections;

internal enum SFX
{
	None = -1,
	Laser = 0,
	// TODO
}

[AddComponentMenu("Pentower/Core/AssetHolder")]
public class AssetHolder : MonoBehaviour
{
	#region Tanks
	public Unit[] tankPrefabs;
	#endregion // Tanks

	#region Sfx & Bgm
	public AudioClip menuMusic;
	public AudioClip inGameMusic;
	public AudioClip[] SFXs;
	#endregion // SFX & BGM

	#region Rendering
	public Camera debugCamera;
	public Camera gameCamera;
	public Camera hudCamera;
	public Material[] skyboxes;
	#endregion // Rendering

	public GameObject bigExplosion;
}

