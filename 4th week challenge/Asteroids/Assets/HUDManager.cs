using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the HUD
/// </summary>
public class HUDManager : MonoBehaviour {
    [SerializeField]
    Text text;
    
    [SerializeField]
    float timer;

    bool timerOn;

	void Start () {
        timer=0;
		text.text=""+timer;
        timerOn=true;
	}

	void Update () {
        if(timerOn)
        {
            timer+=Time.deltaTime;
		    text.text=""+(int)timer;
        }
	}

    public void StopGameTimer()
    {
        timerOn=false;
    }
}
