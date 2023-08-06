using UnityEngine;

public class Falling : MonoBehaviour
{
	[SerializeField] float fallingSpeed = 10f;

	Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		rb.AddForce(Vector3.down * fallingSpeed * Time.deltaTime);

	}
}
