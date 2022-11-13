using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecords : MonoBehaviour
{
    public List<float> Records = new List<float>();

    public void AddNewRecord(float record)
    {
        Records.Add(record);
        Records.Sort();
    }

    public float[] GetTimeRecords()
    {
        return Records.ToArray();
    }

}
