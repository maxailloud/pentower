using UnityEngine;
using System.Collections;

public class EnterPoint : MonoBehaviour
{
	public Tower target;
	
	void OnDestroy()
	{
		this.target = null;
	}

	void OnTriggerEnter (Collider other)
	{
		Unit unit = other.transform.parent.parent.parent.GetComponent<Unit> ();
		if (null != unit) {
			unit.transform.rotation = Quaternion.LookRotation (target.transform.position - unit.transform.position, Vector3.up);
		}
		
	}
}

