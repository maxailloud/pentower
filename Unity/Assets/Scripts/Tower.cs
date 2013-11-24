using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
	public float gold;
	public float hitPoints;
	public float incomePerCycle;
	public int maxUnitPerQueue = 1;
	public int maxAliveUnits = 20;
	
	public List<Queue<Unit>> laneQueues;
	public int currentSpawningLaneIndex = 0;
	
	internal int aliveUnits;

	void Awake ()
	{
		GameConfig config = GameSingleton.Instance.config;

		this.gold = config.defaultGold;

		this.laneQueues = new List<Queue<Unit>> (config.maxLaneCount);
		for (var i = 0; i < config.maxLaneCount; ++i) {
			this.laneQueues.Add (new Queue<Unit> ());
		}
	}
	
	public void EnqueueUnit (int laneIndex, Unit unit)
	{
		laneIndex = Mathf.Clamp (laneIndex, 0, this.laneQueues.Count);

		if (this.laneQueues [laneIndex].Count < this.maxUnitPerQueue) {
			this.laneQueues [laneIndex].Enqueue (unit);
		}
	}

	public void DequeueUnit (int laneIndex)
	{
		laneIndex = Mathf.Clamp (laneIndex, 0, this.laneQueues.Count);
		
		if (this.laneQueues [laneIndex].Count > 0) {
			this.laneQueues [laneIndex].Dequeue ();
		}
	}

	public void MoveLaneIndex ()
	{
		this.currentSpawningLaneIndex = (this.currentSpawningLaneIndex + 1) % this.laneQueues.Count;
	}

	public void UnitKilled(Unit unit)
	{
		// For now just ndecrement counter
		this.aliveUnits = Mathf.Clamp (this.aliveUnits - 1, 0, this.maxAliveUnits);
	}
	
	public void UnitSpawned(Unit unit)
	{
		// For now just increment counter
		this.aliveUnits = Mathf.Clamp (this.aliveUnits + 1, 0, this.maxAliveUnits);
	}
}

