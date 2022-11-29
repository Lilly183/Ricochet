using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public List<Vector2> spawnPoints = new List<Vector2>();
	public List<GameObject> enemyPrefabs = new List<GameObject>();

	[HideInInspector]
	static public int currentEnemies = 0;
	public int spawnLimit = 2;

	public float timeBetweenEnemies = 1.0f;
	public float timeBetweenWaves = 4.0f;

	private int prevSpawnIndex = -1;
	private int currSpawnIndex;

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(SpawnEnemies());
    }

	IEnumerator SpawnEnemies()
	{
		while (currentEnemies < spawnLimit)
		{
			// Choose a random enemy to spawn from the list of enemy prefabs:

			GameObject spawnObject = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

			// Choose a random spawn point from the list of spawn points:

			do
			{
                /*
				Assign to currSpawnIndex a random index that's within the bounds of spawnPoints. 
				Do this for as long as currSpawnIndex has the same value as prevSpawnIndex. 

				This means that the only way out of this loop is for currSpawnIndex to be different 
				from prevSpawnIndex, which prevents our enemies from spawning twice in the same place 
				back-to-back.

				We should consider the possibility that only one spawnPoint exists (making it impossible 
				to assign currSpawnIndex with an index other than prevSpawnIndex). Thus, we need to check
				for one more condition: spawnPoints.Count must also be greater than 1. This prevents 
				infinite loops.
				*/

                currSpawnIndex = Random.Range(0, spawnPoints.Count);

			} while ((currSpawnIndex == prevSpawnIndex) && spawnPoints.Count > 1);

			prevSpawnIndex = currSpawnIndex;

			Instantiate(spawnObject, spawnPoints[currSpawnIndex], Quaternion.identity);
			++currentEnemies;
			//Debug.Log("<color=orange>Enemy spawned!</color>");
			yield return new WaitForSeconds(timeBetweenEnemies);
		}

		//Debug.Log("<color=green>Wave Spawned!</color>");

		yield return new WaitForSeconds(timeBetweenWaves);

		StartCoroutine(SpawnEnemies());
	}

	private void OnDrawGizmos()
	{
		foreach (var item in spawnPoints)
		{
			Gizmos.DrawSphere(item, 0.2f);
		}
	}
}
