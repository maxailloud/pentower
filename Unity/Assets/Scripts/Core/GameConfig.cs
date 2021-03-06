using UnityEngine;
using System.Collections;

[AddComponentMenu("Pentower/Core/Game Config")]
/// <summary>
/// This class holds the current configuration of the game, including player preferences.
/// </summary>
public class GameConfig : MonoBehaviour
{
	#region Layer Masks

	public LayerMask tankLayerMask;
	public LayerMask wallsLayerMask;

	#endregion // Layer Masks

	public float defaultGold = 100.0f;
	public float defaultIncomeCycleDelay = 5.0f;
	public GUISkin defaultSkin;

	public int maxLaneCount = 4;
}

