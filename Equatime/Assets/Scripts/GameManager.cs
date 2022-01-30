using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public DateTime DateTime;
    public TextMeshProUGUI digitalTime;
    public GameObject times;
    public List<string> clearedTimes;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI clearedTimesTitleText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start()
    {
        GetNewTime();
        GetClearedTimes();
        ClearMessage();
    }

    public void GetClearedTimes()
    {
        clearedTimes = SaveSystem.LoadTimes();
        if (clearedTimes != null)
        {
            clearedTimes.Sort();
            UpdateClearedTimes();
        }
        else
        {
            clearedTimes = new List<string>();
        }
    }

    public void TimeCleared()
    {
        string clearedTimeString = DateTime.ToString("HH:mm");
        if (!clearedTimes.Contains(clearedTimeString))
        {
            clearedTimes.Add(clearedTimeString);
        }
        InfoMessage("Well done");
        clearedTimes.Sort();
        SaveSystem.SaveTimes(clearedTimes);
        UpdateClearedTimes();
    }

    public void UpdateClearedTimes()
    {
        clearedTimesTitleText.text = "Cleared: " + clearedTimes.Count;
        TextMeshProUGUI t = times.GetComponent<TextMeshProUGUI>();
        t.text = "";
        foreach (string clearedTime in clearedTimes)
        {
            t.text += clearedTime + "\n";
        }
    }

    public char[] GetNewTime()
    {
        DateTime = DateTime.Now;
        digitalTime.text = DateTime.ToString("HH:mm");
        return DateTime.ToString("HHmm").ToCharArray();
    }

    public void LevelClear()
    {
        TimeCleared();
        GetNewTime();
    }
    
    public void InfoMessage(string msg)
    {
        infoText.color = Color.green;
        infoText.text = msg;
    }

    public void ErrorMessage(string msg)
    {
        infoText.color = Color.red;
        infoText.text = msg;
    }

    public void ClearMessage()
    {
        InfoMessage("");
    }
    
}