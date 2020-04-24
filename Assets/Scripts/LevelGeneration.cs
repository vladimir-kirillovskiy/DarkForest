using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public List<Transform> groundList;
	public GameObject player;
    public Transform groundStart;
    private Vector3 lastEndPostion;
    private const float PLAYER_DISTANCE_SPAWN = 30.0f;

    // Start is called before the first frame update
    private void Awake() {
		lastEndPostion = groundStart.Find("EndPosition").position;
		SpawnGround();
	}
    private void Update() {
		// need to destroy old ones
		// need to add variety
		if (player) {
			if (Vector3.Distance(player.transform.position, lastEndPostion) < PLAYER_DISTANCE_SPAWN) {
				SpawnGround();
			}
		}
	}

	private void SpawnGround() {
		Transform choosenGround = groundList[Random.Range(0, groundList.Count)];
		Transform lastGroundTransform = SpawnGround(choosenGround, lastEndPostion);
		lastEndPostion = lastGroundTransform.Find("EndPosition").position;
	}

	private Transform SpawnGround(Transform ground, Vector3 spawnPosition) {
        // there is an overlap between sections
		Transform GroundTransform = Instantiate(ground, spawnPosition, Quaternion.identity);
		return GroundTransform;
	}
}
