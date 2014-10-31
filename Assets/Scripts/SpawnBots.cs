using UnityEngine;
using System.Collections;

public class SpawnBots : MonoBehaviour {

	public GameObject[] Bots;
	public float timeSpawn = 2f;

	private float lastSpawn = 0;

	// Use this for initialization
	void Start () {}

	// Magic numbers, magic numbers everywhere...
	void SpawnBot(GameObject bot) {
		Vector3 forward =  new Vector3(Random.value * 20 - 10, 1f, Random.value * 20 - 10);
		Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);
		Instantiate(bot, forward, rotation);

	}

	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectsWithTag("Player").Length > 0) {
			//foreach (GameObject bot in Bots) {
				if ((Time.time - lastSpawn) > (timeSpawn / (Time.time / 100))) {
					SpawnBot(Bots[0]);
					lastSpawn = Time.time;
				}
			//}
		}
	}
}
