using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    public TMP_Text usernameText;
    public TMP_Text killsText;
    public TMP_Text deathsText;

    private WinnerMenu winnerMenu;
    Player player;

    private void Start()
    {
        winnerMenu = GameObject.Find("WinnerMenu").GetComponent<WinnerMenu>();
    }

    public void Initialize(Player player)
    {
        usernameText.text = player.NickName;
        this.player = player;
    }

    private void UpdateStats()
    {
        if(player.CustomProperties.TryGetValue("kills", out object kills))
        {
            killsText.text = kills.ToString();
            if (kills.ToString() == winnerMenu.amountOfKillsToWin.ToString())
            {
                winnerMenu.SetWinnerUsername(player.NickName);
                winnerMenu.gameOver = true;                
            }
        }
        if (player.CustomProperties.TryGetValue("deaths", out object deaths))
        {
            deathsText.text = deaths.ToString();
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(targetPlayer == player)
        {
            if (changedProps.ContainsKey("kills") || changedProps.ContainsKey("deaths"))
            {
                UpdateStats();
            }
        }
    }
}
