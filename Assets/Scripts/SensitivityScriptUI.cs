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
        slider.value = PlayerPrefs.GetFloat("Sens");
        Debug.Log(slider.value);

        playerList = GameObject.FindGameObjectsWithTag("PlayerParent");
        foreach (GameObject player in playerList)
        {
            Debug.Log(player.name);
            if (player.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("got the photonview");
                cameraFollowMouse = player.GetComponentInChildren<CameraFollowMouse>();
                if (cameraFollowMouse != null)
                {
                    Debug.Log("get fucked");
                }
                else
                {
                    Debug.Log("should be in");
                }
                
            }
        }
    }

    private void Update()
    {
        cameraFollowMouse.sensitivityX = slider.value;
        cameraFollowMouse.sensitivityY = slider.value;
        PlayerPrefs.SetFloat("Sens", slider.value);
    }
}
