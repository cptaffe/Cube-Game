using UnityEngine;
using System.Collections;

public class BotLaunchMissile : MonoBehaviour {

	public GameObject[] Weapons;
	public float launchTime = 5f;

	private float[] lastLaunchTime = {0};
	private Color MissileColor;

	// Use this for initialization
	void Start () {
		MissileColor = new Color(Random.value, Random.value, Random.value, 0.5f);
	}

	// Update is called once per frame
	void Update () {
		FireWeapons();
	}

	void FireWeapons() {
		for (int i = 0; i < Weapons.Length; i++) {
			GameObject w = Weapons[i];
			float lasttime = lastLaunchTime[i];
			if ((Time.time - lasttime) > launchTime && GameObject.FindGameObjectsWithTag("Player").Length > 0) {
				LaunchMissile(w);
				lastLaunchTime[i] = Time.time;
			}
		}
	}

	void LaunchMissile(GameObject obj) {
		// launch missile inside bot.
		Quaternion rotation = Quaternion.LookRotation(rigidbody.position, Vector3.up);
		GameObject missile = Instantiate(obj, rigidbody.position + Vector3.one, rotation) as GameObject;

		// assign this bot instance as its parent
		missile.GetComponent<MissileMovment>().parent = gameObject;

		// assign misslile special color
		missile.renderer.material.color = MissileColor;
	}
}
