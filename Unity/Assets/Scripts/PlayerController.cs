using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
	[HideInInspector]
	public Player player;

	void Awake()
	{
		this.player = GetComponent<Player>();
	}

	void OnDestroy()
	{
		this.player = null;
	}

	// Use this for initialization
	void Start ()
	{
		// Register player to game context
		GameSingleton.Instance.context.currentPlayer = this.player;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

