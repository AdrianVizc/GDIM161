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
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject connectingScreen;

    private List<string> namesList = new List<string>();

    public void OnClickConnect()
    {
        if (usernameInputField.text.Length == 0 || usernameInputText.text == "Enter Name...​")
        {
            PhotonNetwork.NickName = CreateRandomUsername();
            connectingScreen.SetActive(true);
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.NickName = usernameInputText.text;
            connectingScreen.SetActive(true);
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
