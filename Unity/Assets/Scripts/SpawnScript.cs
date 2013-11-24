using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour
{
	public EnterPoint[] enterPoints;
	public SpawnPoint spawnPoint;
	public float spawningCoolDown = 1.0f;

	private Tower m_tower;

	void Awake ()
	{
		if (this.enterPoints == null || this.enterPoints.Length == 0) {
			this.enterPoints = GetComponentsInChildren<EnterPoint> ();
		}

		if (this.spawnPoint == null) {
			this.spawnPoint = GetComponentInChildren<SpawnPoint> ();
		}

		this.m_tower = GetComponent<Tower> ();
	}

	void OnDestroy ()
	{
		StopAllCoroutines ();

		this.enterPoints = null;
		this.spawnPoint = null;
		this.m_tower = null;
	}

	IEnumerator Start ()
	{
		for (;;) {
			yield return new WaitForSeconds (this.spawningCoolDown);
			CheckLaneQueues ();
		}
	}

	private void CheckLaneQueues ()
	{
		if (this.m_tower != null) {
			int index = this.m_tower.currentSpawningLaneIndex;

			if (this.m_tower.laneQueues [index].Count > 0 && this.m_tower.aliveUnits < this.m_tower.maxAliveUnits) {
				Unit unit = this.m_tower.laneQueues [index].Peek ();

				Unit spawnedUnit;
				if (SpawnUnit (unit, index, out spawnedUnit)) {
					this.m_tower.DequeueUnit (index);
					this.m_tower.UnitSpawned(spawnedUnit);
				}
			}
			
			this.m_tower.MoveLaneIndex ();
		}
	}

	public bool SpawnUnit (Unit Unit, int enterPointIndex, out Unit spawnedUnit)
	{
		return SpawnUnit (Unit, this.spawnPoint.transform, enterPointIndex, out spawnedUnit);
	}

	public bool SpawnUnit (Unit unit, Transform spawnPoint, int enterPointIndex, out Unit spawnedUnit)
	{
		if (unit != null && this.enterPoints != null && this.enterPoints.Length > enterPointIndex) {
			var enter = this.enterPoints [enterPointIndex];	

			Transform targetTransform = enter.transform;
			int layerMask = GameSingleton.Instance.config.tankLayerMask.value;
			Vector3 forwardVector = targetTransform.position - spawnPoint.position;

			Collider[] hitColliders = Physics.OverlapSphere (spawnPoint.position, 1.2f, layerMask);
			if (hitColliders == null || hitColliders.Length == 0) {
				spawnedUnit = (Unit)Instantiate (unit, spawnPoint.position, Quaternion.LookRotation (forwardVector, spawnPoint.up));
				spawnedUnit.tower = this.GetComponent<Tower> ();
				// Parent the instantiated tank to this gameObject
				spawnedUnit.transform.parent = this.transform;

				return true;
			}
		}

		spawnedUnit = null;
		return false;
	}
}

