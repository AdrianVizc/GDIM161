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
        noEnteredNameText = usernameInputText.text;
    }
    public void OnClickConnect()
    {
        if (usernameInputField.text.Length == 0 || usernameInputText.text == noEnteredNameText)
        {
            PhotonNetwork.NickName = CreateRandomUsername();
            loadingScreen.SetActive(true);
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.NickName = usernameInputText.text;
            loadingScreen.SetActive(true);
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
