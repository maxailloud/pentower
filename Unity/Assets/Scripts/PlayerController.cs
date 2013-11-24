using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Player player;

	[HideInInspector]
	public int
		selectedLaneIndex;

	[HideInInspector]
	public int
		selectedUnitIndex;

	void Awake ()
	{
		if (this.player == null) {
			this.player = GetComponent<Player> ();
		}
	}

	void OnDestroy ()
	{
		this.player = null;
	}

	// Use this for initialization
	void Start ()
	{
		if (this.player != null) {
			// Register player to game context
			GameSingleton.Instance.context.currentPlayerController = this;
		}
	}

	public void SelectLaneIndex (int laneIndex)
	{
		this.selectedLaneIndex = Mathf.Clamp (laneIndex, 0, GameSingleton.Instance.config.maxLaneCount - 1);
	}
	
	public void SelectUnitIndex (int unitIndex)
	{
		AssetHolder holder = GameSingleton.Instance.assetHolder;
		this.selectedUnitIndex = Mathf.Clamp (unitIndex, 0, holder.tankPrefabs.Length - 1);

		this.player.tower.EnqueueUnit (this.selectedLaneIndex, holder.tankPrefabs [this.selectedUnitIndex]);
	}
}

