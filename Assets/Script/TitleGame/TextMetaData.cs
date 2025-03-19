using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TextMetaData
{
    public string No;
    public QType Type;
    public float Time;
    public string Title;


    public static List<TextMetaData> Create(List<Dictionary<string, object>> csv)
    {
        List<TextMetaData> list = new List<TextMetaData>();
        int cnt = csv.Count;
        int index = 0;

        for (int i = 0; i < cnt; i++)
        {
            index = 0;
            List<object> values = new List<object>(csv[i].Values);
            list.Add(new TextMetaData
            {
                No = values[index++].ToString(),
                Type = (QType)Enum.Parse(typeof(QType), values[index++].ToString()),
                Time = float.Parse(values[index++].ToString()),
                Title = values[index++].ToString(),
            });
        }

        return list;
    }
}
