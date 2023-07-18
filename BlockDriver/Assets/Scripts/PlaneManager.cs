using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] private float zSpawn;
    [SerializeField] private float planeLenght;
    [SerializeField] private int countOfPlanes;
    [SerializeField] private GameObject[] planes;
    [SerializeField] private Transform player;

    private List<GameObject> activePlanes = new List<GameObject>();
    
    void Start()
    {
        for (int i = 0; i < countOfPlanes; i++)
        {
            SpawnPlane(Random.Range(0, planes.Length));
        }
    }

    
    void Update()
    {
        if (player.position.z - planeLenght > zSpawn - (countOfPlanes * planeLenght))
        {
            SpawnPlane(Random.Range(0, planes.Length));
            DeletePlain();
        }
    }

    private void SpawnPlane(int planeIndex)
    {
        GameObject plane = Instantiate(planes[planeIndex], transform.forward * zSpawn, transform.rotation, gameObject.transform);
        activePlanes.Add(plane);
        zSpawn += planeLenght;
    }

    private void DeletePlain()
    {
        Destroy(activePlanes[0]);
        activePlanes.RemoveAt(0);
    }
}
