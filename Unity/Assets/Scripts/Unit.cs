using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
	#region Gold
	public float creationCost = 1;
	public float incomeBase = 0;
	#endregion // Gold
	
	public float hitPoints = 1;
	public float maxHitPoints = 3;

	public Tower tower;

	void OnDestroy()
	{
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
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

