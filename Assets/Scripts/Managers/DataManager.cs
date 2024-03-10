using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager 
{
    const string RECORDS_KEY = "RECORDS";
    const int RECORDS_QUANTITY = 10;


    public static void UpdateRecords(Record record)
    {
        var data = JsonUtility.ToJson(record);
        PlayerPrefs.SetString(RECORDS_KEY, data);
    }

    static void LoadRecords()
    {
        if (PlayerPrefs.HasKey(RECORDS_KEY)) ParseRecords();
        else InitRecords();
    }

    static void InitRecords()
    {

    }

    static void ParseRecords()
    {

    }
}
