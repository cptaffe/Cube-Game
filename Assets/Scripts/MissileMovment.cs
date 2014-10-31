using UnityEngine;
using System.Collections;

public class MissileMovment : MonoBehaviour {

	public bool ConvergeByTime = true;
	public float speed = 1.0f;
	public float Worth;
	public float maxWorth;
	public string targetName = "Player";
	public GameObject parent; // assigned by instantiator

	private GameObject target;
	private float TimeTraverse;
	private float BeginTime;

	// Use this for initialization
	void Start () {
		// finds nearest gameobject at time of instantiation
		GetClosestTarget();
		BeginTime = Time.time;
		Worth += Random.value * (maxWorth - Worth);
		TimeTraverse = speed * 100;
	}

	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move() {
		// if target no longer exists, reevaluate target
		if (parent == null) {Destroy(gameObject);}
		if (target == null) {
			// if no targets, die
			if ((target = GetClosestTarget()) == null) {
				Destroy(gameObject);
			}
		} else {
			transform.LookAt(target.transform);
			// old movement
			if (ConvergeByTime) {
				rigidbody.position = Vector3.Slerp(rigidbody.position, target.rigidbody.position, (Time.time - BeginTime) / TimeTraverse);
			} else {
				rigidbody.position = Vector3.Slerp(rigidbody.position, target.rigidbody.position, speed / 100);
			}
		}
	}

	GameObject GetClosestTarget() {
		GameObject[] targets = GameObject.FindGameObjectsWithTag(targetName);
		GameObject bestTarget = parent;

		float closestDist = Mathf.Infinity;

		foreach (GameObject possibleTarget in targets) {
			float dist = (transform.position - possibleTarget.transform.position).sqrMagnitude;

			if (dist < closestDist) {
				closestDist = dist;
				bestTarget = possibleTarget;
			}
		}
		return bestTarget;
	}
}
