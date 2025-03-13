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
    private GameObject secondNearest; //player is always the closest

    [SerializeField]
    private Ability ability;

    // Start is called before the first frame update
    void Start()
    {
        allPlayerObjects = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < allPlayerObjects.Length; i++)
        {
            allDistances.Add(Vector3.Distance(this.transform.position, allPlayerObjects[i].transform.position));
        }

        allDistances.Sort();

        if (allPlayerObjects.Length > 1)
        {
            for (int i = 0; i < allPlayerObjects.Length; i++)
            {
                if (Vector3.Distance(this.transform.position, allPlayerObjects[i].transform.position) == allDistances[1])
                {
                    secondNearest = allPlayerObjects[i];
                }
            }

            agent.SetDestination(secondNearest.transform.position);
        }

        StartCoroutine(DestroyAfterCD(ability.activeTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (secondNearest != null)
        {
            agent.SetDestination(secondNearest.transform.position);
        }
    }

    private IEnumerator DestroyAfterCD(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
