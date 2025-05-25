using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InvisAbility : Ability
{
    private Invis invisScript;

    public override void Activate(GameObject parent)
    {
        InvisForPhotonView invisForPhotonView = parent.GetComponentInParent<InvisForPhotonView>();
        PhotonView photonView = invisForPhotonView.GetComponent<PhotonView>();
        photonView.RPC("RPCInvisible", RpcTarget.AllBuffered);
    }

    public override void BeginCooldown(GameObject parent)
    {
        InvisForPhotonView invisForPhotonView = parent.GetComponentInParent<InvisForPhotonView>();
        PhotonView photonView = invisForPhotonView.GetComponent<PhotonView>();
        photonView.RPC("RPCStopInvisible", RpcTarget.AllBuffered);
    }
}
