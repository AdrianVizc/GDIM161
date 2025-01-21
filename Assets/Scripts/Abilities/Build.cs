using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public Vector3 place;

    private RaycastHit hit;

    [SerializeField]
    private GameObject objectToPlace, tempObject;

    [SerializeField]
    private GameObject brickWall, tempWall;

    public bool placeNow;
    public bool placeWall;
    public bool tempObjectExists;

    public bool ground; //for testing purposes only

    [SerializeField]
    private float offset = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (placeNow == true)
        {
            SendRay(); //start raycasting
        }

        if (placeWall == true)
        {
            objectToPlace = brickWall; //actual item we want to place
        }

        if (Input.GetKeyDown(KeyCode.F)) //Initiate placing procedure
        {
            //Debug.Log("Starting Placing");
            PlaceWall();
        }

        TouchingGround();
    }

    public void SendRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            place = new Vector3(hit.point.x, hit.point.y + offset, hit.point.z); //determine place to put wall based on mouse position

            if (hit.transform.CompareTag("Ground")) //only allow placement on the ground
            {
                if (tempObjectExists == false) //if wall preview doesn't exist, create one
                {
                    //Debug.Log("Getting Temp");
                    Instantiate(tempWall, place, Quaternion.identity);
                    tempObject = GameObject.Find("Temp Wall(Clone)");
                    tempObjectExists = true;
                }

                if (Input.GetMouseButtonDown(0)) //if left mouse button clicked, place the actual wall
                {
                    //Debug.Log("Left Click, Placing");
                    Instantiate(objectToPlace, place, Quaternion.identity);
                    placeNow = false;
                    placeWall = false;

                    Destroy(tempObject); //destroy temp/preview wall
                    tempObjectExists = false;
                }

                if (tempObject != null)
                {
                    //Debug.Log("Moving Temp");
                    tempObject.transform.position = place; //move preview with mouse
                }
            }

            if (Input.GetMouseButtonDown(1)) //right mouse button to cancel
            {
                Debug.Log("Stopped Placing");
                placeNow = false;
                placeWall = false;

                Destroy(tempObject);
            }
        }
    }

    public void PlaceWall()
    {
        placeNow = true;
        placeWall = true;
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
            }
            else
            {
                ground = false;
            }
        }
    }
}
