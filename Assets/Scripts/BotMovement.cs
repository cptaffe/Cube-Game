using UnityEngine;
using System.Collections;

public class BotMovement : MonoBehaviour {

	public float Rarity = 1f;
	public float speed = 6f;
	public float turnSmoothing = 15f;
	public float Worth;
	public float maxWorth = 20;

	private Vector3 moveDirection;

	// Use this for initialization
	void Start () {
		// add random worth to current worth
		Worth += Random.value * (maxWorth - Worth);
		renderer.material.color = Color.black;
	}

	void Update() {
		Move(0f, 0f);
	}

	void Move (float h, float v) {
		moveDirection = new Vector3(h, 0f, v);
		moveDirection *= speed;
		moveDirection = Oscilate(moveDirection);
		rigidbody.MovePosition(rigidbody.position + (moveDirection * Time.deltaTime));
		Rotate(moveDirection);
	}

	void Rotate(Vector3 targetDirection) {
		if (targetDirection != Vector3.zero) {
			// Create a rotation based on vector assuming that up is the global y axis.
			Quaternion targetRotation = Quaternion.LookRotation(-targetDirection, Vector3.up);
			// lerp to new rotation
			Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
			// Change the players rotation to this new rotation.
			rigidbody.MoveRotation(newRotation);
		}
	}

	Vector3 Oscilate(Vector3 targetDirection) {
		targetDirection.y = Mathf.Sin(Time.time * speed);
		return targetDirection;
	}

	void OnDestroy() {
		Destroy(transform.parent.gameObject);
	}

	// Eats bots
	void OnTriggerEnter(Collider other) {
		// Bot Missiles do damage
		if (other.gameObject.tag == "Weapon") {
			Destroy(other.gameObject);
			Destroy(gameObject); // destroy self
		}
	}
}
