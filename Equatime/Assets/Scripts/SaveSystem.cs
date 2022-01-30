using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveTimes(List<string> times)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/times.bat";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, times);
        stream.Close();
    }
    
    public static List<string> LoadTimes()
    {
        string path = Application.persistentDataPath + "/times.bat";
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<string> times = formatter.Deserialize(stream) as List<string>;
            stream.Close();
            return times;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }
}
