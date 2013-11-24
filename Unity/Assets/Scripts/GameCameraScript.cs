using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MouseOrbit))]
public class GameCameraScript : MonoBehaviour
{
	public float target0MinDistance;
	public float target0MaxDistance;

	public Transform[] targets;

	public float[] distances;
	
	public float[] xSpeeds;
	public float[] ySpeeds;
	
	public float[] yMinLimits;
	public float[] yMaxLimits;

	private MouseOrbit m_orbit;
	private int m_currentIndex;

	// Use this for initialization
	void Start ()
	{
		m_orbit = GetComponent<MouseOrbit>();
		this.target0MaxDistance = this.distances[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Target switch
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

		// Distance
		SetDistance();

	}

	void SetDistance()
	{
		if (m_currentIndex == 0)
		{
			float wheel = Input.GetAxis ("Mouse ScrollWheel");
			if (Mathf.Abs (wheel) > 0.025f)
			{
				this.m_orbit.distance = Mathf.Clamp (this.m_orbit.distance + wheel * 5.0f, this.target0MinDistance, this.target0MaxDistance);
			}
		}
	}

	void SwitchTarget(int targetIndex)
	{
		if (targetIndex < 0 || targetIndex > this.targets.Length)
		{
			return;
		}

		this.m_orbit.target = this.targets[targetIndex];
		this.m_currentIndex = targetIndex;

		// Each tower use the same settings
		targetIndex = Mathf.Clamp (targetIndex, 0, 1);

		this.m_orbit.distance = this.distances[targetIndex];
		this.m_orbit.xSpeed = this.xSpeeds[targetIndex];
		this.m_orbit.ySpeed = this.ySpeeds[targetIndex];
		this.m_orbit.yMinLimit = this.yMinLimits[targetIndex];
		this.m_orbit.yMaxLimit = this.yMaxLimits[targetIndex];
	}
}

