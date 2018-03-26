using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroidSpawner : MonoBehaviour {

    public float range = 1;
    public float rate = 0.75f;

    public GameObject asteroid;

    public IEnumerator spawnAsteroids() {
        while (true) {
            spawnAsteroid();
            yield return new WaitForSeconds(Random.Range(0.25f,rate));
        }
    }

    private void spawnAsteroid() {
        float xPos = Random.Range(transform.position.x - range/2, transform.position.x + range/2);
        Vector3 targetPosition = transform.position;
        targetPosition.x = xPos;

        Vector3 randomAngle = transform.eulerAngles;
        randomAngle.z = Random.Range(0, 360);

        Instantiate(asteroid, targetPosition, Quaternion.Euler(randomAngle));
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(range,1,1));
    }
}
