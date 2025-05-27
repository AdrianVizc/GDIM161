using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_Text usernameInputText;
    [SerializeField] private GameObject loadingScreen;

    private string noEnteredNameText;

    private List<string> namesList = new List<string>();

    private void Start()
    {
        if (PlayerPrefs.HasKey("Slot1"))
        {
            PlayerPrefs.DeleteKey("Slot1");
        }
        if (PlayerPrefs.HasKey("Slot2"))
        {
            PlayerPrefs.DeleteKey("Slot2");
        }
        if (PlayerPrefs.HasKey("Slot3"))
        {
            PlayerPrefs.DeleteKey("Slot3");
        }
        if (PlayerPrefs.HasKey("Ready"))
        {
            PlayerPrefs.DeleteKey("Ready");
        }

        noEnteredNameText = usernameInputText.text;

        if(PlayerPrefs.HasKey("NickName"))
        {
            usernameInputField.text = PlayerPrefs.GetString("NickName");
        }
    }
    public void OnClickConnect()
    {
        if (usernameInputField.text.Length == 0 || usernameInputText.text == noEnteredNameText)
        {
            PhotonNetwork.NickName = CreateRandomUsername();
            loadingScreen.SetActive(true);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.NickName = usernameInputText.text;
            PlayerPrefs.SetString("NickName", usernameInputText.text);
            PlayerPrefs.Save();
            loadingScreen.SetActive(true);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }

    private string CreateRandomUsername()
    {
        int randomNumber = Random.Range(0, 102);

        namesList.Add("Adrian");
        namesList.Add("Dominic");
        namesList.Add("Jack");
        namesList.Add("Nick");
        namesList.Add("Thomas");

        return namesList[Random.Range(0, namesList.Count)] + randomNumber.ToString();
    }
}
