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

    private bool placeNow;

    private bool placeWall;

    private bool tempObjectExists;

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
                    tempObject = Instantiate(tempWall, place, Quaternion.identity);
                    //tempObject = GameObject.Find("Temp Wall");
                    tempObjectExists = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Placing");
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
}
