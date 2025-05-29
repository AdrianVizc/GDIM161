using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityScriptUI : MonoBehaviour
{
    Slider slider;
    CameraFollowMouse cameraFollowMouse;
    GameObject[] playerList;

    private void Start()
    {
        slider = GetComponent<Slider>();
        if (PlayerPrefs.GetFloat("Sens") == 0)
        {
            slider.value = 3;
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("Sens");
        }      

        Invoke("FindPlayerObjects", 0.2f);
    }

    private void FindPlayerObjects()
    {
        playerList = GameObject.FindGameObjectsWithTag("PlayerParent");
        foreach (GameObject player in playerList)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                cameraFollowMouse = player.GetComponentInChildren<CameraFollowMouse>();                
            }
        }
    }

    private void Update()
    {
        Debug.Log(slider.value);
        cameraFollowMouse.sensitivityX = slider.value;
        cameraFollowMouse.sensitivityY = slider.value;
        PlayerPrefs.SetFloat("Sens", slider.value);
    }
}
