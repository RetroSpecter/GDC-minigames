using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGUI : MonoBehaviour {

    private Slider slider;
    private float maxTime;

    private void Start() {
        slider = GetComponent<Slider>();
    }

    public void setFullTime(float time) {
        maxTime = time;
    }

    public void updateTime(float currentTime) {
        slider.value = currentTime / maxTime;
    }
}
