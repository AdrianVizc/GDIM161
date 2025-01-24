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
    private float offset = 1.5f;

    private bool rotate;

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

        TouchingGround();
    }

    public void SendRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            place = new Vector3(hit.point.x, hit.point.y + offset, hit.point.z); //determine place to put wall based on mouse position

            if (hit.transform.gameObject.layer == 6) //only allow placement on the ground
            {
                if (tempObjectExists == false) //if wall preview doesn't exist, create one
                {
                    Debug.Log("Getting Temp");
                    Instantiate(tempWall, place, Quaternion.identity);

                    tempObject = GameObject.Find("Temp Wall(Clone)");
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
                    tempObject.transform.position = place; //move preview with mouse
                }
            }

            if (Input.GetMouseButtonDown(1)) //right mouse button to cancel
            {
                Debug.Log("Stopped Placing");
                placeNow = false;
                placeWall = false;
                tempObjectExists = false;

                Destroy(tempObject);
            }
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
            }
            else
            {
                ground = false;
            }
        }
    }
}
