using UnityEngine;
using System.Collections;

public class PlayerConsume : MonoBehaviour {

	// public float eatingTime = 2f;
	public float Health = 100f;
	public float boostSpeedMultiplier = 2;
	public float points = 0;

	// Boost
	private bool boost;
	private float boostBegin;
	private float boostTime;

	void Update() {
		ScaleByHealth();
		BoostExtra();
	}

	void ScaleByHealth() {
		Vector3 maxScale = Vector3.one * (Health / 100);
		Vector3 minScale = Vector3.zero;
		float zoomValue = (Health / 100) / 15;

		transform.localScale += Vector3.one * (zoomValue * Time.deltaTime);
		transform.localScale = Vector3.Max(transform.localScale, minScale);
		transform.localScale = Vector3.Min(transform.localScale, maxScale);
	}

	void BoostExtra() {
		if (boost && (Time.time - boostBegin) < boostTime) {
			gameObject.GetComponent<PlayerMovement>().speed *= boostSpeedMultiplier;
		} else if (boost) {
			boost = false;
		}
	}

	void TakeHit(float damage) {
		Health -= damage;
		if (Health <= 0) {
			Destroy(gameObject);
		}
	}

	void GetHealth(float health) {
		Health += health;
	}

	// Eats bots
	void OnTriggerEnter(Collider other) {
		// Bot Missiles do damage
		if (other.gameObject.tag == "Weapon") {
			// take hit
			TakeHit(other.gameObject.GetComponent<MissileMovment>().Worth);
			// adopt missile's color
			renderer.material.color = other.gameObject.renderer.material.color;
			Destroy(other.gameObject);
		}
		// Bots can be eaten
		else if (other.gameObject.name == "Bot") {
			float worth = other.gameObject.GetComponent<BotMovement>().Worth;
			GetHealth(worth);
			points += worth;
			Destroy(other.gameObject);
			renderer.material.color = Color.black;
		} else if (other.gameObject.name == "BoostBot") {
			boostBegin = Time.time;
			boostTime = other.gameObject.GetComponent<BotMovement>().Worth;
		}
	}
}
