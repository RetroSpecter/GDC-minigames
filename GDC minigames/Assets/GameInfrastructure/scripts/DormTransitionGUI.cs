using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DormTransitionGUI : GameManagerGUI {
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public override void transitionIntoGame() {
        anim.Play("DormZoomIn");
    }

    public override void transitionOutGame() {
        anim.Play("DormZoomOut");
    }

    public override void transitionIntoRound()
    {
        anim.Play("DormShiftLeft");
    }

    public override void transitionOutOfRound()
    {
        GetComponent<Animator>().Play("DormShiftRight");
    }

    public override void updateLivesCount(int value){
        //this assumes livesCount is a text field. Override to change this later
        livesCount.GetComponent<LivesGUI>().updateHealthGUI(value);
    }

    public override void updateRoundCount(int value) {
        roundCount.GetComponent<Text>().text = "" + value;
    }
}
