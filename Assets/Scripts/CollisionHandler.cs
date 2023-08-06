using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	Movement movement;
	AudioSource audioSource;

	[SerializeField] float restartTime = 1f;
	[SerializeField] AudioClip collideAudio;
	[SerializeField] AudioClip clearAudio;
	[SerializeField] ParticleSystem collideParticle;
	[SerializeField] ParticleSystem clearParticle;
	[SerializeField] Light headLight;
	[SerializeField] Light bodyLight;

	bool isTransitioning = false;

	private void Start()
	{
		movement = GetComponent<Movement>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		// testing
		Testing();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (isTransitioning == true) return;

		string tag = other.gameObject.tag;
		switch (tag)
		{
			case "Obstacle":
				RestartScene();
				break;
			case "Friend":
				Debug.Log("Collides with friend");
				break;
			default:
				RestartScene();
				break;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (isTransitioning == true) return;

		string tag = other.gameObject.tag;
		switch (tag)
		{
			case "Finish":
				StageClear();
				break;
			case "Teleport":
				Debug.Log("Teleport");
				break;
			default:
				Debug.Log("Trigger occured");
				break;
		}
	}

	/// <summary>Load Next Scene</summary>
	void LoadNextScene()
	{
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
			nextSceneIndex = 0;
		SceneManager.LoadScene(nextSceneIndex);
	}

	/// <summary>Reload Current Scene</summary>
	void ReloadCurrentScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/// <summary>When rocket destroyed, play audio, particle and restart scene</summary>
	void RestartScene()
	{
		Invoke("ReloadCurrentScene", restartTime);
		isTransitioning = true;
		movement.enabled = false;
		audioSource.Stop();
		audioSource.PlayOneShot(collideAudio);
		collideParticle.Play();
		headLight.enabled = false;
		bodyLight.enabled = false;
	}

	/// <summary>When stage is clear, play audio, particle and load next scene</summary>
	void StageClear()
	{
		Invoke("LoadNextScene", 1f);
		isTransitioning = true;
		movement.enabled = false;
		audioSource.Stop();
		audioSource.PlayOneShot(clearAudio);
		clearParticle.Play();

	}

	/*
	testing method
	*/
	private void Testing()
	{
		LoadNextSceneByCKey();
		ReloadCurrentSceneByLKey();
	}
	private void LoadNextSceneByCKey()
	{
		if (Input.GetKey(KeyCode.C))
		{
			LoadNextScene();
		}
	}

	private void ReloadCurrentSceneByLKey()
	{
		if (Input.GetKey(KeyCode.L))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
