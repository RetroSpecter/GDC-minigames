using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// LivesGUI is the GUI representation of the number of lives the player has
[ExecuteInEditMode]
public class LivesGUI : MonoBehaviour {

    public int maxHealth = 3;
    public List<GameObject> healthUnits = new List<GameObject>();

    public GameObject healthUnit;
    public float padding;
    public float scale = 1;

    void Start() {
        setupHealthGUI(maxHealth);
    }

    /// <summary>
    /// sets up the initial GUI, spawning in visuals for each life
    /// </summary>
    /// <param name="amount">the intial number of lives the player has</param>
    void setupHealthGUI(int amount) {
        foreach (GameObject unit in healthUnits) {
            if (!Application.isPlaying) {
                DestroyImmediate(unit);
            } else {
                Destroy(unit);
            }
        }
        healthUnits.Clear();

        for (int i = 0; i < maxHealth; i++) {
            GameObject heart = Instantiate(healthUnit, new Vector3(transform.position.x + padding * i, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            healthUnits.Add(heart);
            heart.transform.localScale = Vector3.one * scale;
            heart.transform.SetParent(this.transform);
        }
        updateHealthGUI(amount);
    }

    /// <summary>
    /// Update the healthGUI to match amount
    /// </summary>
    /// <param name="amount"> the number of lives the player has</param>
    public void updateHealthGUI(int amount) {
        for (int i = 0; i < maxHealth; i++) {
            if (i >= amount) {
                healthUnits[i].SetActive(false);
            }
        }
    }
}
