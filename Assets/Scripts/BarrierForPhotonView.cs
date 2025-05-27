using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierForPhotonView : MonoBehaviour
{
    private ObjectSpawner objectSpawner;

    private void Start()
    {
        objectSpawner = GetComponentInChildren<ObjectSpawner>();
    }

    [PunRPC]
    public void RPCSpawn(int childViewID, int parentViewID)
    {
        PhotonView ParentView = GetComponent<PhotonView>();
        PhotonView childView = GetComponentInChildren<PhotonView>();

        //childView.transform.SetParent(parentView.transform, true);
    }
}
