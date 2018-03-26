using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsMinigame : Minigame {

    [Header("Astroid Game Specifics")]
    public shipMovement player;
    public astroidSpawner astroidSpawn;


    public float easyRate = 0.75f;
    public float mediumRate = 0.5f;
    public float hardRate = 0.3f;

    public override void gameStart(difficulty difficulty) {
        if(difficulty == difficulty.EASY)
            astroidSpawn.rate = easyRate;
        else if (difficulty == difficulty.MEDIUM)
            astroidSpawn.rate = mediumRate;
        else if (difficulty == difficulty.HARD)
            astroidSpawn.rate = hardRate;

        StartCoroutine(astroidSpawn.spawnAsteroids());
        player.die += lose;
    }
}
