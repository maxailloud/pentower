using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour
{
	public float frequency;
	public GameObject spawnedObject;
	public EnterPoint[] enterPoints;
	public SpawnPoint spawnPoint;

	void Awake ()
	{
		Random.seed = Mathf.RoundToInt (Time.realtimeSinceStartup);

		if (this.enterPoints == null || this.enterPoints.Length == 0) {
			this.enterPoints = GetComponentsInChildren<EnterPoint> ();
		}

		if (this.spawnPoint == null) {
			this.spawnPoint = GetComponentInChildren<SpawnPoint> ();
		}
	}

	void OnDestroy()
	{
		this.spawnedObject = null;
		this.enterPoints = null;
		this.spawnPoint = null;
	}

	// Update is called once per frame
	IEnumerator Start ()
	{
		for (;;) {
			DoSpawn ();
			yield return new WaitForSeconds (1.0f / this.frequency);
		}
	}

	private void DoSpawn ()
	{
		if (this.spawnedObject != null && this.enterPoints != null && this.enterPoints.Length > 0) {
			var enter = this.enterPoints [Random.Range (0, this.enterPoints.Length)];
			var spawn = this.spawnPoint;

			Transform spawnTransform = spawn.transform;
			Transform targetTransform = enter.transform;
			int layerMask = 1 << 8;
			RaycastHit hit;
			Vector3 forwardVector = targetTransform.position - spawnTransform.position;
			Collider[] hitColliders = Physics.OverlapSphere(spawnTransform.position, 1.2f, layerMask);
			if (hitColliders == null || hitColliders.Length == 0)
			{
				GameObject go = (GameObject) Instantiate (this.spawnedObject, spawnTransform.position, Quaternion.LookRotation (forwardVector, spawnTransform.up));
				go.GetComponent<Unit>().tower = this.GetComponent<Tower>();
				// Parent the instantiated tank to this gameObject
				go.transform.parent = this.transform;
			}
		}
	}
}

