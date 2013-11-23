﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Tower))]
public class Player : MonoBehaviour {

	[HideInInspector]
	public Tower tower;

	void Awake()
	{
		this.tower = GetComponent<Tower>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}