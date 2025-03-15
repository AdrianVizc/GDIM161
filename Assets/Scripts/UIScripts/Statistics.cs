using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    [SerializeField] private GameObject riveObject;
    [SerializeField] private GameObject InputFieldObject;
    [SerializeField] private GameObject StatisticsButtonObject;
    [SerializeField] private GameObject CloseButtonObject;
    [SerializeField] private GameObject StatsList;
    [SerializeField] private GameObject StatsPanel;

    [SerializeField] private TMP_Text killsNumber;
    [SerializeField] private TMP_Text deathsNumber;
    [SerializeField] private TMP_Text KDNumber;
    [SerializeField] private TMP_Text DashNumber;
    [SerializeField] private TMP_Text DoubleJumpNumber;
    [SerializeField] private TMP_Text MultishotNumber;
    [SerializeField] private TMP_Text MineNumber;
    [SerializeField] private TMP_Text WallNumber;
    [SerializeField] private TMP_Text BarrierNumber;
    [SerializeField] private TMP_Text TrackingBotNumber;

    private void Start()
    {        
        CloseButtonObject.SetActive(false);
        StatsList.SetActive(false);
        StatsPanel.SetActive(false);
    }

    public void StatisticsButton()
    {
        Debug.Log("Stats");
        riveObject.SetActive(false);
        InputFieldObject.SetActive(false);
        StatisticsButtonObject.SetActive(false);

        CloseButtonObject.SetActive(true);
        StatsList.SetActive(true);
        StatsPanel.SetActive(true);

        UpdateStats();
    }

    public void CloseButton()
    {
        riveObject.SetActive(true);
        InputFieldObject.SetActive(true);
        StatisticsButtonObject.SetActive(true);

        CloseButtonObject.SetActive(false);
        StatsList.SetActive(false);
        StatsPanel.SetActive(false);
    }

    private void UpdateStats()
    {
        killsNumber.text = PlayerPrefs.GetInt("Kills", 0).ToString();
        deathsNumber.text = PlayerPrefs.GetInt("Deaths", 0).ToString();
        if (PlayerPrefs.GetInt("Deaths") == 0)
        {
            KDNumber.text = PlayerPrefs.GetInt("Kills").ToString();
        }
        else
        {
            float temp = (float)PlayerPrefs.GetInt("Kills") / PlayerPrefs.GetInt("Deaths");
            KDNumber.text = temp.ToString("F2");
        }        
        DashNumber.text = PlayerPrefs.GetInt("Dash", 0).ToString();
        DoubleJumpNumber.text = PlayerPrefs.GetInt("DoubleJump", 0).ToString();
        MultishotNumber.text = PlayerPrefs.GetInt("Multishot", 0).ToString();
        MineNumber.text = PlayerPrefs.GetInt("Mine", 0).ToString();
        WallNumber.text = PlayerPrefs.GetInt("Wall", 0).ToString();
        BarrierNumber.text = PlayerPrefs.GetInt("Barrier", 0).ToString();
        TrackingBotNumber.text = PlayerPrefs.GetInt("TrackingBot", 0).ToString();
    }
}
