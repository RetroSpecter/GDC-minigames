using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LameMinigame : Minigame {

    public bool spaceToWin;
    public GameObject text;

    public override void gameStart(difficulty difficulty) {
        if (UnityEngine.Random.Range(0, 101) < 50) {
            spaceToWin = true;
        }

        if (UnityEngine.Random.Range(0, 101) < 50) {
            text.transform.eulerAngles = new Vector3(0,0,180);
        }

        if (spaceToWin) {
            if (UnityEngine.Random.Range(0, 101) < 50) {
                text.GetComponent<TextMesh>().text = "backspace to lose \n \n Space to win";
            } else {
                text.GetComponent<TextMesh>().text = "Space to win \n \n backspace to lose";
            }
        } else {
            if (UnityEngine.Random.Range(0, 101) < 50) {
                text.GetComponent<TextMesh>().text = "Space to lose \n \n backspace to win";
            } else {
                text.GetComponent<TextMesh>().text = "backspace to win \n \n Space to lose";
            }
        }
    }

    public override void gameUpdate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (spaceToWin)
                win();
            else
                lose();
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            if (!spaceToWin)
                win();
            else
                lose();
        }
    }
}
