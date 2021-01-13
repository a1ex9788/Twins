using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDurationTime : MonoBehaviour
{
    [SerializeField]Text timeText;
    float durationTime;
    public void StartGameDuration()
    {
        durationTime = Time.time;
    }

    public void Pause()
    {
        durationTime = Time.time - durationTime;
        float minutes = Mathf.FloorToInt(durationTime / 60);
        float seconds = Mathf.FloorToInt(durationTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
