using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InvisAbility : Ability
{

    public override void Activate(GameObject parent)
    {
        InvisForPhotonView invisForPhotonView = parent.GetComponentInParent<InvisForPhotonView>();
        PhotonView photonView = invisForPhotonView.GetComponent<PhotonView>();
        photonView.RPC("RPCInvisible", RpcTarget.All, photonView.ViewID);
    }

    public override void BeginCooldown(GameObject parent)
    {
        InvisForPhotonView invisForPhotonView = parent.GetComponentInParent<InvisForPhotonView>();
        PhotonView photonView = invisForPhotonView.GetComponent<PhotonView>();
        photonView.RPC("RPCStopInvisible", RpcTarget.All, photonView.ViewID);
    }
}
