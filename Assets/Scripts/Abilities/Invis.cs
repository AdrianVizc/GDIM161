using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invis : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer objRender;
    private bool invisEnabled;
    private bool inputCheckEnabled;
    private PhotonView photonView;

    private void Start()
    {
        inputCheckEnabled = false;
        photonView = GetComponentInParent<PhotonView>();
    }

    void Update()
    {
        if (inputCheckEnabled && invisEnabled && 
            (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R)))
        {
            photonView.RPC("RPCStopInvisible", RpcTarget.AllBuffered);
        }
    }

    public void Invisible()
    {
        objRender.enabled = false;
        invisEnabled = true;
        StartCoroutine(EnableInputCheckNextFrame());
    }

    IEnumerator EnableInputCheckNextFrame()
    {
        yield return null; // wait one frame
        inputCheckEnabled = true;
    }

    public void StopInvisible()
    {
        objRender.enabled = true;
        invisEnabled = false;
    }
}
