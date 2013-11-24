using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MouseOrbit))]
public class GameCameraScript : MonoBehaviour
{
	public Transform[] targets;
	public float[] distances;
	
	public float[] yMinLimits;
	public float[] yMaxLimits;

	private MouseOrbit m_orbit;

	// Use this for initialization
	void Start ()
	{
		m_orbit = GetComponent<MouseOrbit>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		int targetIndex = -1;

		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			targetIndex = 0;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			targetIndex = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			targetIndex = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			targetIndex = 3;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			targetIndex = 4;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad5))
		{
			targetIndex = 5;
		}

		SwitchTarget(targetIndex);
	}

	void SwitchTarget(int targetIndex)
	{
		if (targetIndex < 0 || targetIndex > this.targets.Length)
		{
			return;
		}

		this.m_orbit.target = this.targets[targetIndex];
		this.m_orbit.distance = this.distances[targetIndex];
		this.m_orbit.yMinLimit = this.yMinLimits[targetIndex];
		this.m_orbit.yMaxLimit = this.yMaxLimits[targetIndex];
	}
}

