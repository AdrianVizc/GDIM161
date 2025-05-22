using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invis : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer objRender;
    private bool invisEnabled;

    // Update is called once per frame
    void Update()
    {
        if (invisEnabled && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R)))
        {
            StopInvisible();
        }
    }

    public void Invisible()
    {
        objRender.enabled = false;
        invisEnabled = true;
    }

    public void StopInvisible()
    {
        objRender.enabled = true;
        invisEnabled = false;
    }
}
