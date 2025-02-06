using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrackingBot : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private List<float> allDistances;

    [SerializeField]
    private GameObject[] allPlayerObjects;

    [SerializeField]
    private GameObject nearestObj;

    [SerializeField]
    private GameObject secondNearest;

    [SerializeField]
    private GameObject playerSelf;

    private float distance;
    private float nearestDistance = 100000;

    // Start is called before the first frame update
    void Start()
    {
        allPlayerObjects = GameObject.FindGameObjectsWithTag("Player");

        //float[] distances = new float[allPlayerObjects.Length];
        for (int i = 0; i < allPlayerObjects.Length; i++)
        {
            //distances[i] = Vector3.Distance(this.transform.position, allPlayerObjects[i].transform.position);
            allDistances.Add(Vector3.Distance(this.transform.position, allPlayerObjects[i].transform.position));
            //if (distance < nearestDistance)
            //{
            //    nearestObj = allPlayerObjects[i];
            //    nearestDistance = distance;
            //}
        }
        //playerSelf = nearestObj;

        allDistances.Sort();

        for (int i = 0; i < allPlayerObjects.Length; i++)
        {
            if (Vector3.Distance(this.transform.position, allPlayerObjects[i].transform.position) == allDistances[1])
            {
                secondNearest = allPlayerObjects[i];
            }
        }
        agent.SetDestination(secondNearest.transform.position);
        //agent.SetDestination(nearestObj.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(secondNearest.transform.position);
    }
}
