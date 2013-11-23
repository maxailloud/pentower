using UnityEngine;
using System.Collections;

public class TankCollision : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		Tower tower = other.gameObject.GetComponent<Tower> ();
		if(null != tower && this.gameObject.GetComponent<Unit>().tower != tower)
		{
			Destroy (this.gameObject);
		}
	}
}

