using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InvisForPhotonView : MonoBehaviour
{
    private Invis invisScript;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        invisScript = GetComponentInChildren<Invis>();
    }

    [PunRPC]
    public void RPCInvisible(int viewID)
    {
        PhotonView targetView = PhotonView.Find(viewID);
        Invis invisScript = targetView.GetComponentInChildren<Invis>();
        invisScript.Invisible();
    }

    [PunRPC]
    public void RPCStopInvisible(int viewID)
    {
        PhotonView targetView = PhotonView.Find(viewID);
        Invis invisScript = targetView.GetComponentInChildren<Invis>();
        invisScript.StopInvisible();
    }
}
