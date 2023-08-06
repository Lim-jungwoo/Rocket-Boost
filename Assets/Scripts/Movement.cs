using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Movement : MonoBehaviour
{
	[SerializeField] float rocketMainThrust;
	[SerializeField] float rocketSideThrust;
	[SerializeField] AudioClip boostAudio;
	[SerializeField] ParticleSystem mainEngineParticle;
	[SerializeField] ParticleSystem leftEngineParticle;
	[SerializeField] ParticleSystem rightEngineParticle;

	Rigidbody rb;
	AudioSource audioSource;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		// Rocket Thrust
		RocketThrust();
		ProcessRotation();

		if (Input.GetKey(KeyCode.Escape)) Quit();
	}

	private void ProcessRotation()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			ApplyRotation(rocketSideThrust);
			if (rightEngineParticle.isPlaying == false)
				rightEngineParticle.Play();
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			ApplyRotation(-rocketSideThrust);
			if (leftEngineParticle.isPlaying == false)
				leftEngineParticle.Play();
		}
		else
		{
			rightEngineParticle.Stop();
			leftEngineParticle.Stop();
		}
	}

	private void ApplyRotation(float rotationThisFrame)
	{
		//* freezing rotation so we can manually rotate
		rb.freezeRotation = true;
		transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
		//* unfreezing rotation so the physics system can take over
		rb.freezeRotation = false;
	}

	private void RocketThrust()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			rb.AddRelativeForce(Vector3.up * rocketMainThrust * Time.deltaTime);
			if (mainEngineParticle.isPlaying == false)
				mainEngineParticle.Play();
			if (audioSource.isPlaying == false)
				audioSource.PlayOneShot(boostAudio);
		}
		else
		{
			mainEngineParticle.Stop();
			audioSource.Stop();
		}
	}

	void Quit()
	{
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
	}
}

