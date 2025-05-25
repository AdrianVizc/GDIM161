using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InvisForPhotonView : MonoBehaviour
{
    private Invis invisScript;

    void Start()
    {
        invisScript = GetComponentInChildren<Invis>();
    }

    [PunRPC]
    public void RPCInvisible()
    {
        invisScript.Invisible();
    }

    [PunRPC]
    public void RPCStopInvisible()
    {
        invisScript.StopInvisible();
    }
}
