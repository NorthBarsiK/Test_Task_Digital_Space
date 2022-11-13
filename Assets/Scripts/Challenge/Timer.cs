using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TimeRecords))]
public class Timer : MonoBehaviour, IChallenge
{
    public static Timer instance;
    void Awake()
    {
        instance = this;
    }

    public float currentTime = 0.0f;
    private bool isTimerStart = false;

    private TimeRecords recordsData = null;
    void Start()
    {
        recordsData = GetComponent<TimeRecords>();
    }

    public void ChallengeStart()
    {
        isTimerStart = true;
    }

    void Update()
    {
        if(isTimerStart)
        {
            currentTime += Time.deltaTime;
        }
    }

    public void ChallengeEnd()
    {
        isTimerStart = false;
        recordsData.AddNewRecord(currentTime);
        currentTime = 0.0f;
    }

    public string GetTimeStringFormatted()
    {
        return FormatTime(currentTime);
    }

    public string GetTimeStringFormatted(float value)
    {
        return FormatTime(value);
    }

    private string FormatTime(float time)
    {
        int milliseconds = (int)(time % 1 * 1000);
        string millisecondsString = string.Format("{0:D3}", milliseconds);

        int seconds = (int)(time % 60);
        string secondsString = string.Format("{0:D2}", seconds);

        int minutes = (int)(time / 60);
        string minutesString = string.Format("{0:D2}", minutes);

        string result = minutesString + ":" + secondsString + ":" + millisecondsString;
        return result;
    }

    public string GetRecordsFormatted()
    {
        float[] records = recordsData.GetTimeRecords();
        string result = "";
        int i = 1;

        foreach(float record in records)
        {
            result += i.ToString() + ": - " + GetTimeStringFormatted(record) + "\n";
            i++;
        }

        return result;
    }
}
