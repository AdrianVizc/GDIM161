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

    public bool ground;

    //private float offset = 1.55f;

    // Update is called once per frame
    void Update()
    {
        if (placeNow == true)
        {
            SendRay();
        }

        if (placeWall == true)
        {
            objectToPlace = brickWall;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Starting Placing");
            PlaceWall();
        }

        TouchingGround();
    }

    public void SendRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            place = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            if (hit.transform.CompareTag("Ground"))
            {
                if (tempObjectExists == false)
                {
                    Debug.Log("Getting Temp");
                    Instantiate(tempWall, place, Quaternion.identity);
                    tempObject = GameObject.Find("Temp Wall(Clone)");
                    tempObjectExists = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Left Click, Placing");
                    Instantiate(objectToPlace, place, Quaternion.identity);
                    placeNow = false;
                    placeWall = false;

                    Destroy(tempObject);
                    tempObjectExists = false;
                }

                if (tempObject != null)
                {
                    //Debug.Log("Moving Temp");
                    tempObject.transform.position = place;
                }
            }

            if (Input.GetMouseButtonDown(1))
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

    public void TouchingGround()
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
