using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField]
    private BuildAbility ability;

    private Vector3 place;

    private RaycastHit hit;

    [SerializeField]
    private GameObject objectToPlace, tempObject;

    [SerializeField]
    private GameObject brickWall, tempWall;

    private bool placeNow;
    private bool placeWall;
    private bool tempObjectExists;

    public bool ground; //for testing purposes only

    [SerializeField]
    private float offset;

    [SerializeField]
    private float placementReach;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject floor;

    private float distance;
    private bool rotate;
    private bool canPlace;

    [SerializeField]
    private bool objectInRange;

    [SerializeField]
    private Transform shootingPoint;

    [SerializeField]
    private Material[] materials;

    private Renderer render;

    // Update is called once per frame
    void Update()
    {
        if (placeNow == true)
        {
            SendRay(); //start raycasting
            //FindPlace();
        }

        if (placeWall == true)
        {
            objectToPlace = brickWall; //actual item we want to place
        }

        //if (Input.GetKeyDown(KeyCode.F)) //Initiate placing procedure
        //{
        //Debug.Log("Starting Placing");
        //PlaceWall();
        //}

        if (Input.GetKeyDown(KeyCode.F) && tempObjectExists == true) //honestly is fine if F activates the ability & rotates, simple click will rotate it back + direction plays a part
        {
            rotate = true;
        }

        if (rotate == true)
        {
            RotateWall();
        }

        if (distance <= placementReach)
        {
            objectInRange = true;
        }
        else
        {
            objectInRange = false;
        }

        TouchingGround();
        //CanPlace();

    }

    public void SendRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, placementReach))
        //if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, 20))
        {
            place = new Vector3(hit.point.x, hit.point.y + offset, hit.point.z); //determine place to put wall based on mouse position
            //place = new Vector3(hit.point.x, floor.transform.position.y + 2.5f, hit.point.z);
            //distance = Vector3.Distance(place, player.transform.position);
            if (hit.transform.gameObject.layer == 6) //only allow placement on the ground
            {
                if (tempObjectExists == false) //if wall preview doesn't exist, create one
                {
                    //Debug.Log("Getting Temp");
                    Instantiate(tempWall, place, Quaternion.identity);
                    //Instantiate(tempWall, new Vector3(hit.point.x, floor.transform.position.y + 2.5f, hit.point.z), Quaternion.identity);

                    tempObject = GameObject.Find("Temp Wall(Clone)");

                    render = tempObject.GetComponent<Renderer>();
                    render.sharedMaterial = materials[0];
                    tempObjectExists = true;
                }

                if (Input.GetMouseButtonDown(0)) //if left mouse button clicked, place the actual wall
                {
                    //Debug.Log("Left Click, Placing");
                    GameObject wall = Instantiate(objectToPlace, place, tempObject.transform.rotation);
                    placeNow = false;
                    placeWall = false;

                    Destroy(tempObject); //destroy temp/preview wall
                    tempObjectExists = false;

                    StartCoroutine(DestroyWallOnCD(wall, ability.cooldownTime));
                }

                if (tempObject != null)
                {
                    //Debug.Log("Moving Temp");
                    render.sharedMaterial = materials[0];
                    tempObject.transform.position = place; //move preview with mouse

                }
            }

            if (Input.GetMouseButtonDown(1)) //right mouse button to cancel
            {
                //Debug.Log("Stopped Placing");
                placeNow = false;
                placeWall = false;
                tempObjectExists = false;

                Destroy(tempObject);
            }
        }
        else
        {
            place = Camera.main.transform.position + Camera.main.transform.forward * placementReach;

            if (tempObject != null)
            {
                render.sharedMaterial = materials[1];
                tempObject.transform.position = place; //move preview with mouse
            }
        }
    }

    public void FindPlace()
    {
        place = Camera.main.transform.position + Camera.main.transform.forward * placementReach;

        if (tempObjectExists == false) //if wall preview doesn't exist, create one
        {
            //Debug.Log("Getting Temp");
            Instantiate(tempWall, place, Quaternion.identity);
            //Instantiate(tempWall, new Vector3(hit.point.x, floor.transform.position.y + 2.5f, hit.point.z), Quaternion.identity);

            tempObject = GameObject.Find("Temp Wall(Clone)");
            tempObjectExists = true;
        }

        if (Input.GetMouseButtonDown(0) && ground) //if left mouse button clicked, place the actual wall
        {
            //Debug.Log("Left Click, Placing");
            GameObject wall = Instantiate(objectToPlace, place, tempObject.transform.rotation);
            placeNow = false;
            placeWall = false;

            Destroy(tempObject); //destroy temp/preview wall
            tempObjectExists = false;

            StartCoroutine(DestroyWallOnCD(wall, ability.cooldownTime));
        }

        if (tempObject != null)
        {
            //Debug.Log("Moving Temp");
            tempObject.transform.position = place; //move preview with mouse
        }

        if (Input.GetMouseButtonDown(1)) //right mouse button to cancel
        {
            //Debug.Log("Stopped Placing");
            placeNow = false;
            placeWall = false;
            tempObjectExists = false;

            Destroy(tempObject);
        }
    }
    public void CanPlace()
    {
        if (canPlace == true)
        {
            render.sharedMaterial = materials[0];
        }
        if (canPlace == false)
        {
            render.sharedMaterial = materials[1];
        }
    }
    public void PlaceWall()
    {
        placeNow = true;
        placeWall = true;
    }

    public void RotateWall()
    {
        tempObject.transform.Rotate(0f, 90f, 0f, Space.World);
        rotate = false;
    }

    private IEnumerator DestroyWallOnCD(GameObject wall, float cd)
    {
        yield return new WaitForSeconds(cd);
        Destroy(wall);
    }

    public void TouchingGround() //for testing purposes only
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                ground = true;
                canPlace = true;
            }
            else
            {
                ground = false;
                canPlace = false;
            }
        }
    }
}
