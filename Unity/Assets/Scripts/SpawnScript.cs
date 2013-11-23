using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour
{
	public float frequency;
	public GameObject spawnedObject;
	public SpawnPoint[] spawnPoints;

	void Awake ()
	{
		Random.seed = Mathf.RoundToInt (Time.realtimeSinceStartup);

		if (this.spawnPoints == null || this.spawnPoints.Length == 0) {
			this.spawnPoints = GetComponentsInChildren<SpawnPoint> ();
		}
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
		if (this.spawnedObject != null && this.spawnPoints != null && this.spawnPoints.Length > 0) {
			var spawn = this.spawnPoints [Random.Range (0, this.spawnPoints.Length)];

			Transform spawnTransform = spawn.transform;
			Transform targetTransform = spawn.target.transform;
			int layerMask = 1 << 10;
			RaycastHit hit;
			Vector3 forwardVector = targetTransform.position - spawnTransform.position;
			if (!Physics.Raycast (spawnTransform.position, forwardVector, out hit, 3, layerMask))
			{
				GameObject go = (GameObject) Instantiate (this.spawnedObject, spawnTransform.position, Quaternion.LookRotation (forwardVector, spawnTransform.up));
				go.GetComponent<Unit>().tower = this.GetComponent<Tower>();
				// Parent the instantiated tank to this gameObject
				go.transform.parent = this.transform;
			}
		}
	}
}

