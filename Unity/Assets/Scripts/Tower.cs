using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
	public float gold;
	public float hitPoints;
	public float incomePerCycle;

	void Awake()
	{
		this.gold = GameSingleton.Instance.config.defaultGold;	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

