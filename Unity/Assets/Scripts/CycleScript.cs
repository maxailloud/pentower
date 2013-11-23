using UnityEngine;
using System.Collections;

public class CycleScript : MonoBehaviour
{
	public float incomeCycleDelay;

	void Awake()
	{
		GameConfig config = GameSingleton.Instance.config;
		this.incomeCycleDelay = config.defaultIncomeCycleDelay;
	}

	IEnumerator Start () {
		// Forever
		for(;;)
		{
			yield return new WaitForSeconds(this.incomeCycleDelay);
			DoIncomeCycle();
		}
	}

	private void DoIncomeCycle()
	{
		// Get players
		Player[] players = GameObject.FindObjectsOfType(typeof(Player)) as Player[];

		foreach(var player in players)
		{
			player.gold += player.incomePerCycle;
		}
	}
}
