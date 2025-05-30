using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Build : MonoBehaviour
{
    [SerializeField]
    private Ability ability;

    private Vector3 place;

    private RaycastHit hit;

    private Shooting shooting;
    private AbilityHolder[] abilityHolderList;
    private AbilityHolder multishotAbility;

    [SerializeField]
    private GameObject objectToPlace, previewObject;

    [SerializeField]
    private GameObject realObject, tempObject;

    private bool placeNow;
    private bool placeObj;
    private bool tempObjectExists;

    private bool ground; //for testing purposes only
    private bool multishotPicked;
    private KeyCode buildKey;

    [SerializeField]
    private float offset;

    [SerializeField]
    private float placementReach;

    private bool rotate;

    [SerializeField]
    private Material[] materials;

    private Renderer render;

    private void Start()
    {
        //Debug.Log("NAME: " + this.name);
        shooting = transform.root.GetComponentInChildren<Shooting>();
        abilityHolderList = transform.root.GetComponentsInChildren<AbilityHolder>();

        foreach(AbilityHolder ab in abilityHolderList)
        {
            if (ab.GetType().GetField("ability").GetValue(ab).ToString() == "Multishot (MultishotAbility)")
            {
                //Debug.Log("FOUND MULTISHOT IN BUILD");
                if (ab.isActiveAndEnabled)
                {
                    multishotAbility = ab;
                    multishotPicked = true;
                }
                else
                {
                    multishotPicked = false;
                }

                //break;
            }

            if (ab.GetType().GetField("ability").GetValue(ab).ToString() == "BuildWall (BuildAbility)")
            {
                if (ab.isActiveAndEnabled)
                {
                    buildKey = ab.key;
                }
            }

            //Debug.Log("NAME: " + this.name);
            //Debug.Log("ab : " + ab.GetType().GetField("ability").GetValue(ab).ToString());
            //Debug.Log("Contains: " + ab.GetType().GetField("ability").GetValue(ab).ToString().Contains(this.name));

            //if (ab.GetType().GetField("ability").GetValue(ab).ToString().Contains(this.name))
            //{
            //    buildKey = ab.key;
            //    Debug.Log("ACTIVATE KEY: " + buildKey);
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (placeNow == true)
        {
            SendRay(); //start raycasting
            shooting.enabled = false;
            if (multishotPicked)
            {
                multishotAbility.enabled = false;
            }

        }
        else
        {
            shooting.enabled = true;
            if (multishotPicked)
            {
                multishotAbility.enabled = true;
            }
        }

        if (placeObj == true)
        {
            objectToPlace = realObject; //actual item we want to place
        }

        //if (Input.GetKeyDown(KeyCode.F)) //Initiate placing procedure
        //{
        //Debug.Log("Starting Placing");
        //PlaceWall();
        //}

        if (Input.GetKeyDown(buildKey) && tempObjectExists == true)
        {
            rotate = true;
        }

        if (rotate == true)
        {
            Rotate();
        }

        TouchingGround();
    }

    public void SendRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, placementReach))
        {
            place = new Vector3(hit.point.x, hit.point.y + offset, hit.point.z); //determine place to put wall based on mouse position

            if (hit.transform.gameObject.layer == 6) //only allow placement on the ground
            {
                if (tempObjectExists == false) //if wall preview doesn't exist, create one
                {
                    Instantiate(tempObject, place, Quaternion.identity);

                    //previewObject = GameObject.Find("Temp Claymore(Clone)");
                    previewObject = GameObject.Find(GetPreviewName(tempObject));

                    render = previewObject.GetComponentInChildren<Renderer>();
                    for (int i = 0; i < render.materials.Length; i++)
                    {
                        render.materials[i].color = materials[0].color;
                    }
                    tempObjectExists = true;
                }

                if (Input.GetMouseButtonDown(0) && !InGameUI.globalInputLock) //if left mouse button clicked, place the actual wall
                {
                    GameObject obj = PhotonNetwork.Instantiate(objectToPlace.name, place, previewObject.transform.rotation, 0);
                    placeNow = false;
                    placeObj = false;

                    Destroy(previewObject); //destroy temp/preview wall
                    tempObjectExists = false;

                    //Debug.Log("CD Time is " + ability.cooldownTime);
                    StartCoroutine(DestroyObjOnCD(obj, ability.activeTime));

                    shooting.gameObject.SetActive(true);
                }

                if (previewObject != null)
                {
                    //change temp wall to blue = can place
                    for (int i = 0; i < render.materials.Length; i++)
                    {
                        render.materials[i].color = materials[0].color;
                    }
                    previewObject.transform.position = place; //move preview with mouse

                }
            }

            if (Input.GetMouseButtonDown(1)) //right mouse button to cancel
            {
                //Debug.Log("Stopped Placing");
                placeNow = false;
                placeObj = false;
                tempObjectExists = false;

                Destroy(previewObject);
            }
        }
        else //if nothing is hit by raycast
        {
            place = Camera.main.transform.position + Camera.main.transform.forward * placementReach; //use camera to find position of preview placement

            if (previewObject != null)
            {
                //change temp wall to red = cannot place
                for (int i = 0; i < render.materials.Length; i++)
                {
                    render.materials[i].color = materials[1].color;
                }
                previewObject.transform.position = place; //move preview with mouse
            }
            else if (tempObjectExists == false)
            {
                Instantiate(tempObject, place, Quaternion.identity);
                previewObject = GameObject.Find(GetPreviewName(tempObject));
                render = previewObject.GetComponentInChildren<Renderer>(); //already red to begin with

                tempObjectExists = true;
            }
        }
    }
    public void Place()
    {
        placeNow = true;
        placeObj = true;
    }

    public void Rotate()
    {
        previewObject.transform.Rotate(0f, 90f, 0f, Space.World);
        rotate = false;
    }

    private IEnumerator DestroyObjOnCD(GameObject obj, float cd)
    {
        yield return new WaitForSeconds(cd);
        if (obj != null)
        {
            PhotonNetwork.Destroy(obj);
        }
    }

    public void TouchingGround() //for testing purposes only
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                ground = true;
            }
            else
            {
                ground = false;
            }
        }
    }

    public string GetPreviewName(GameObject tempObject)
    {
        Debug.Log("" + tempObject.name + "(Clone)");
        return "" + tempObject.name + "(Clone)";
    }
}
