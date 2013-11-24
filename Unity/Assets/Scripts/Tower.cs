using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TowerState
{
	// Idle
	Idle,
	// Attacking an enemy unit
	AttackUnit,
	// Unit is dying (play animation), cannot do anything
	Destroying,
	// Unit is dead, waiting for being removed from the scene
	// This is the last state.
	Destroyed,
}

public class Tower : MonoBehaviour
{
	public float gold;
	public float hitPoints = 5;
	public float incomePerCycle;
	public int maxUnitPerQueue = 1;
	public int maxAliveUnits = 20;
	
	public List<Queue<Unit>> laneQueues;
	public int currentSpawningLaneIndex = 0;

	public GameObject towerVisual;

	[HideInInspector]
	public int aliveUnits;
	public TowerState currentState = TowerState.Idle;

	void Awake ()
	{
		GameConfig config = GameSingleton.Instance.config;

		this.gold = config.defaultGold;

		this.laneQueues = new List<Queue<Unit>> (config.maxLaneCount);
		for (var i = 0; i < config.maxLaneCount; ++i) {
			this.laneQueues.Add (new Queue<Unit> ());
		}
	}

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(FSM());
		this.hitPoints = 10;
	}

	#region State Machine
	/// <summary>
	/// State Machine entry point, acts as a state scheduler
	/// </summary>
	/// <returns></returns>
	IEnumerator FSM()
	{
		/// Execute the current coroutine (State)
		while (true)
		{
			yield return StartCoroutine(currentState.ToString() + "State");
		}
	}

	#region Internal States
	IEnumerator IdleState()
	{
		while (this.currentState == TowerState.Idle)
		{
			// nothing for now
			yield return null;
		}
	}
	
	IEnumerator AttackUnitState()
	{
		while (this.currentState == TowerState.AttackUnit)
		{
			// nothing for now
			yield return null;
		}
	}
	
	IEnumerator DestroyingState()
	{
		while (this.currentState == TowerState.Destroying)
		{
			Destroy(this.gameObject.GetComponent<SpawnScript>());
			this.currentState = TowerState.Destroyed;
			yield return null;
		}
	}
	
	IEnumerator DestroyedState()
	{
		while (this.currentState == TowerState.Destroyed)
		{
			yield return null;
		}
	}
	#endregion // Internal States
	#endregion // State Machine

	
	public void EnqueueUnit (int laneIndex, Unit unit)
	{
		laneIndex = Mathf.Clamp (laneIndex, 0, this.laneQueues.Count - 1);

		if (this.laneQueues [laneIndex].Count < this.maxUnitPerQueue) {
			this.laneQueues [laneIndex].Enqueue (unit);
		}
	}

	public void DequeueUnit (int laneIndex)
	{
		laneIndex = Mathf.Clamp (laneIndex, 0, this.laneQueues.Count - 1);
		
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
		this.aliveUnits = Mathf.Clamp (this.aliveUnits - 1, 0, this.maxAliveUnits - 1);
	}
	
	public void UnitSpawned(Unit unit)
	{
		// For now just increment counter
		this.aliveUnits = Mathf.Clamp (this.aliveUnits + 1, 0, this.maxAliveUnits - 1);
	}

	public void LoseHitPoints (int hitPoints) 
	{
		this.hitPoints -= hitPoints;
		if (this.hitPoints <= 0) {
			this.hitPoints = 0;
			this.currentState = TowerState.Destroying;
		}
	}
}

