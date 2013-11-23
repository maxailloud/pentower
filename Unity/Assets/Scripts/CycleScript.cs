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
		Tower[] towers = GameObject.FindObjectsOfType(typeof(Tower)) as Tower[];

		foreach(var tower in towers)
		{
			tower.gold += tower.incomePerCycle;
		}
	}
}
