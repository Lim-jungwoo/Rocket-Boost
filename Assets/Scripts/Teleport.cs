using UnityEngine;

public class Teleport : MonoBehaviour
{
	[SerializeField] GameObject destination;
	[SerializeField] AudioClip teleportAudio;
	[SerializeField] float offColliderTime = 3f;

	Transform destinationTransform;
	AudioSource audioSource;
	BoxCollider boxCollider, destinationBoxCollider;

	private void Start()
	{
		destinationTransform = destination.transform;
		destinationBoxCollider = destination.GetComponent<BoxCollider>();

		audioSource = GetComponent<AudioSource>();
		boxCollider = GetComponent<BoxCollider>();
	}

	private void OnTriggerEnter(Collider other)
	{
		string tag = other.gameObject.tag;
		switch (tag)
		{
			case "Player":
				other.transform.position = destinationTransform.position;
				audioSource.Stop();
				audioSource.PlayOneShot(teleportAudio);
				OffColliderTime();
				break;
		}
	}

	void OffColliderTime()
	{
		boxCollider.enabled = false;
		destinationBoxCollider.enabled = false;
		Invoke("OnCollider", offColliderTime);
	}

	void OnCollider()
	{
		boxCollider.enabled = true;
		destinationBoxCollider.enabled = true;
	}
}
