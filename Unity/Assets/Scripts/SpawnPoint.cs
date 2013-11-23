using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
	public Tower target;

	void Awake ()
	{
		if (this.target != null)
			this.transform.rotation = Quaternion.LookRotation (-target.transform.forward, Vector3.up);
	}
}

