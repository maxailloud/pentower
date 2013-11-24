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
			Unit otherUnit = other.transform.parent.parent.parent.gameObject.GetComponent<Unit>();
			tower = otherUnit.tower;
			if (unit.tower != tower) {
				unit.StopMovement();
				unit.StartAttackUnit(otherUnit);
			}
			else {
				if(other.gameObject.tag == "RearBumper"){
					//Debug.Log("Friendly front");
					if(otherUnit.currentState != UnitState.Movement) {
						unit.StopMovement();
					} else {
						unit.ChangeSpeed(other.transform.parent.parent.parent.gameObject.GetComponent<LocomotionController>().initialVelocity);
					}
				}
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
