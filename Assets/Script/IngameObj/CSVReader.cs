using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader
{
    public static string[][] Read(string filename)
    {
        string text = (Resources.Load(filename) as TextAsset).text;
        string[] texts = text.Split('\n');
        string[][] output = new string[texts.Length][];

        for(int i = 0; i < output.Length; i++)
        {
            output[i] = texts[i].Split(',');

            /*
            string s = "";
            for(int j = 0; j < output[i].Length; j++)
            {
                s += output[i][j];
            }
            Debug.Log(s);
            */
        }
        return output;
    }
}