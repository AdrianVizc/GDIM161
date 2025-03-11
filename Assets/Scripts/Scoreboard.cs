using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using TMPro;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviourPunCallbacks
{
    Dictionary<Player, ScoreboardItem> scoreboardItems = new Dictionary<Player, ScoreboardItem>();

    List<GameObject> scoreboardItemList = new List<GameObject>();

    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;

    private void Start()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
        }
    }

    public void UpdateScoreBoard()
    {
        GetAllChildReference(container);
        SortScoreboardItemList();
        SortScoreboardItemObject();
        //DestroyAllChildObjects(container);
        //InstantiateScoreboard();
        //TurnEverythingOn();
        scoreboardItemList.Clear();
    }

    private void GetAllChildReference(Transform parent)
    {
        int skipFirst = 0;
        foreach (Transform child in parent)
        {
            if (skipFirst > 0)
            {
                scoreboardItemList.Add(child.gameObject);
            }
            skipFirst++;
        }
    }
    
    private void SortScoreboardItemList()
    {
        scoreboardItemList.Sort((a, b) =>
        {
            int numA = GetNumber(a);
            int numB = GetNumber(b);
            return numB.CompareTo(numA);
        });        
    }

    private int GetNumber(GameObject obj)
    {
        string killsText = obj.GetComponentInChildren<TMP_Text>().text;

        return int.Parse(killsText);
    }

    private void SortScoreboardItemObject()
    {
        for(int i = 0; i < scoreboardItemList.Count; i++)
        {
            scoreboardItemList[i].transform.SetSiblingIndex(i + 1);
        }
    }

    /*private void DestroyAllChildObjects(Transform parent)
    {
        int skipFirst = 0;
        foreach (Transform child in parent)
        {
            if (skipFirst > 0)
            {
                Destroy(child.gameObject);
            }
            skipFirst++;
        }
    }

    private void InstantiateScoreboard()
    {
        foreach (GameObject item in scoreboardItemList)
        {
            Instantiate(item, container);
        }
    }

    private void TurnEverythingOn()
    {
        GetAllChildReference(container);
        foreach (GameObject item in scoreboardItemList)
        {
            item.GetComponent<ScoreboardItem>().enabled = true;
            item.GetComponentInChildren<Image>().enabled = true;
            item.transform.GetChild(1).GetComponent<TMP_Text>().enabled = true; //kills
            item.transform.GetChild(2).GetComponent<TMP_Text>().enabled = true; //username
            item.transform.GetChild(3).GetComponent<TMP_Text>().enabled = true; //deaths
        }

        scoreboardItemList.Clear();
    }*/

    private void AddScoreboardItem(Player player)
    {
        ScoreboardItem item = Instantiate(scoreboardItemPrefab, container).GetComponent<ScoreboardItem>();
        item.Initialize(player);
        scoreboardItems[player] = item;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboardItem(otherPlayer);
    }

    private void RemoveScoreboardItem(Player player)
    {
        //Destroy(scoreboardItems[player].gameObject);
        //scoreboardItems.Remove(player);

        GetAllChildReference(container);
        foreach(GameObject item in scoreboardItemList)
        {
            //Debug.Log("Item name: " + item.transform.GetChild(2).GetComponent<TMP_Text>().text.ToString());
            //Debug.Log("Player name: " + player.NickName);
            if (item.transform.GetChild(2).GetComponent<TMP_Text>().text.ToString() == player.NickName)
            {
                Destroy(item);
                //Debug.Log("Destroyed scoreboardItem");
            }
            /*else
            {
                Debug.Log("Didn't Find");
            }*/
        }
        scoreboardItemList.Clear();
    }
}
