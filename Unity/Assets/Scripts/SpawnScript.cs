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
			Instantiate (this.spawnedObject, spawnTransform.position, Quaternion.LookRotation (targetTransform.position - spawnTransform.position, spawnTransform.up));

		}
	}
}

