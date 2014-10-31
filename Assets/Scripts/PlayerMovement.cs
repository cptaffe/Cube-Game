using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 6.0f;
	public float turnSmoothing = 15f;

    private Vector3 parentMoveDirection;
	private Vector3 childMoveDirection;
	private Transform Parent;

	void Start() {
		Parent = transform.parent;
		renderer.material.color = Color.black;
	}

	// Late update is called after update
	void LateUpdate() {
		Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

	void Move (float h, float v) {
		parentMoveDirection = new Vector3(h, 0f, v);

		Parent.position = Vector3.MoveTowards(Parent.rigidbody.position, Parent.rigidbody.position + parentMoveDirection, Time.deltaTime * speed);

		childMoveDirection = Vector3.zero;

		// oscilate and apply to child
		childMoveDirection = Oscilate(childMoveDirection * speed);

		rigidbody.MovePosition(rigidbody.position + (childMoveDirection * Time.deltaTime));
		Rotate(parentMoveDirection);
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
}
