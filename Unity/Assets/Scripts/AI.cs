using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
	public Player player;

	public float decisionDelay = 1.5f;
	
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
	IEnumerator Start ()
	{
		if (this.player != null) {
			// Wait one frame
			yield return null;

			for (;;) {
				DoRandomChoice ();
				yield return new WaitForSeconds (this.decisionDelay);
			}
		}
	}

	private void DoRandomChoice ()
	{
		// Choose lane
		this.selectedLaneIndex = Random.Range (0, GameSingleton.Instance.config.maxLaneCount);
		// Choose unit
		AssetHolder holder = GameSingleton.Instance.assetHolder;
		this.selectedUnitIndex = Random.Range (0, holder.tankPrefabs.Length);
		
		this.player.tower.EnqueueUnit (this.selectedLaneIndex, holder.tankPrefabs [this.selectedUnitIndex]);
	}

}

