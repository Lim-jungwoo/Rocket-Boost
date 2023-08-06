using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
	[SerializeField] GameObject fallingObjectPrefab;
	[SerializeField] float respawnTime = 5f;
	[SerializeField] float destroyTime = 10f;

	float lastRespawnTime = 0f, playingTime = 0f;
	GameObject fallingObjectInstance;

	private void Update()
	{
		playingTime += Time.deltaTime;

		if (playingTime > lastRespawnTime + respawnTime)
		{
			fallingObjectInstance = Instantiate(fallingObjectPrefab, transform.position, transform.rotation);
			Destroy(fallingObjectInstance, destroyTime);
			lastRespawnTime = playingTime;
			Debug.Log($"Respawn Time : {playingTime}");
		}
	}
}
