using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMinigame : Minigame
{

    [Header("Falling Game Specifics")]
    public shipMovement player;

    public override void gameStart(difficulty difficulty) {
        player.die += lose;
    }
}
