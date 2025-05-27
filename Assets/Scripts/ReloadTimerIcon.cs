using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTimerIcon : MonoBehaviour
{
    [SerializeField] private GameObject reloadtTimerIcon;
    [SerializeField] private Slider mySlider;

    private float timer;

    private void Start()
    {
        PhotonView photonView = GetComponentInParent<PhotonView>();
        if (!photonView.IsMine)
        {
            Destroy(gameObject);
        }
        mySlider.maxValue = GetComponentInParent<Shooting>().reloadTimer;
    }

    private void Update()
    {
        timer = GetComponentInParent<Shooting>().timer;
        mySlider.value = timer;
    }
}
