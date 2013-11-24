using UnityEngine;
using System.Collections;

[AddComponentMenu("Pentower/Units/TankCollision")]
public class TankCollision : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		Tower tower = other.transform.parent.gameObject.GetComponent<Tower> ();
		Unit unit = this.gameObject.GetComponent<Unit> ();
		if (null == tower) {
			unit.StopMovement();
			tower = other.transform.parent.parent.parent.gameObject.GetComponent<Unit>().tower;
			if (unit.tower != tower) {
				unit.StartAttackUnit(other.transform.parent.parent.parent.gameObject.GetComponent<Unit>());
			}
		}
		else if (null != tower && unit.tower != tower) {
			unit.StopMovement();
			unit.StartAttackTower(tower);			
		}
	}

	void OnTriggerExit (Collider other)
	{
		Unit unit = this.gameObject.GetComponent<Unit> ();
		unit.StartMovement();
	}
}
