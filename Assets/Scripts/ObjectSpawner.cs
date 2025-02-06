using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject objectToSpawn;

    public void Spawn()
    {
        Instantiate(objectToSpawn, player.transform.position, Quaternion.identity);
    }
}
