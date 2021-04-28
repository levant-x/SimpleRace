using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager 
{
    const string RECORDS_KEY = "records";
    const int RECORDS_QUANTITY = 10;
    static Record[] records;


    public static Record[] GetRecords()
    {
        LoadRecords();
        return records;
    }


    static void LoadRecords()
    {
        if (PlayerPrefs.HasKey(RECORDS_KEY)) ParseRecords();
        else InitRecords();
    }

    static void InitRecords()
    {
        records = new Record[RECORDS_QUANTITY];
        for (int i = 0; i < RECORDS_QUANTITY; i++) records[i] = new Record();
    }

    static void SaveRecords()
    {

    }

    static void ParseRecords()
    {

    }
}
