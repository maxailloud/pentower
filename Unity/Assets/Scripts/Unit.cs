using UnityEngine;
using System.Collections;

public enum UnitState
{
	// Idle
	Idle,
	// Unit is moving
	Movement,
	// Unit is attacking another unit
	AttackUnit,
	// Unit is attacking a tower
	AttackTower,
	// Unit is dying (play animation), cannot do anything
	Dying,
	// Unit is dead, waiting for being removed from the scene
	// This is the last state.
	Dead,
}

public class Unit : MonoBehaviour
{
	#region Gold
	public float creationCost = 1;
	public float incomeBase = 0;
	#endregion // Gold
	
	public float hitPoints = 1;
	public float maxHitPoints = 3;

	public Tower tower;

	[HideInInspector]
	public Unit targetUnit = null;
	public Tower targetTower = null;

	/// Current state of the unit
	public UnitState currentState;

	void OnDestroy()
	{
		StopAllCoroutines();
		this.tower = null;
	}

	// Use this for initialization
	void Start ()
	{
		if (tower != null)
		{
			tower.gold -= this.creationCost;
			tower.incomePerCycle += this.incomeBase;
		}
		StartCoroutine(FSM());
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
		while (this.currentState == UnitState.Idle)
		{
			// nothing for now
			yield return null;
		}
	}

	IEnumerator MovementState()
	{
		while (this.currentState == UnitState.Movement)
		{
			// nothing for now
			yield return null;
		}
	}

	IEnumerator AttackUnitState()
	{
		while (this.currentState == UnitState.AttackUnit)
		{
			this.Attack();
			yield return null;
		}
	}

	IEnumerator AttackTowerState()
	{
		while (this.currentState == UnitState.AttackTower)
		{
			this.Attack();
			yield return null;
		}
	}
	IEnumerator DyingState()
	{
		while (this.currentState == UnitState.Dying)
		{
			// nothing for now
			yield return null;
		}
	}

	IEnumerator DeadState()
	{
		while (this.currentState == UnitState.Dead)
		{
			// nothing for now
			yield return null;
		}
	}
	#endregion // Internal States
	#endregion // State Machine

	public void Die()
	{
		// TODO animation
		if (tower != null)
		{
			this.tower.UnitKilled(this);
		}
	}

	public void StartAttackUnit (Unit targetUnit)
	{
		//Debug.Log("Start Fire to minions motherfucker!");
		this.targetUnit = targetUnit;
		this.currentState = UnitState.AttackUnit;
	}

	public void StartAttackTower (Tower targetTower)
	{
		//Debug.Log("Start Destroying tower motherfucker!");
		this.targetTower = targetTower;
		this.currentState = UnitState.AttackTower;
	}

	void Attack ()
	{
		if (null != this.targetUnit) {
			Debug.Log("Attack unit");
			this.targetUnit.hitPoints -= 1;
		} else if (null != this.targetTower) {

		}
	}

	public void StopAttack ()
	{
		this.targetUnit = null;
		this.targetTower = null;
	}

	public void StopMovement()
	{
		this.gameObject.GetComponent<LocomotionController>().enabled = false;
		this.gameObject.rigidbody.velocity = Vector3.zero;
		this.gameObject.rigidbody.angularVelocity = Vector3.zero;
		this.currentState = UnitState.Idle;
	}

	public void StartMovement()
	{
		this.gameObject.GetComponent<LocomotionController>().enabled = true;
		this.currentState = UnitState.Movement;
	}
}

