using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class HUD : MonoBehaviour
{
	private Player m_player;

	void Awake()
	{
		this.m_player = GetComponent<Player>();
	}

	void OnGUI()
	{

	}
}

