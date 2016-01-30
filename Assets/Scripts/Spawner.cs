using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public GameObject Item;
    public GameObject Civil;
    public GameObject Cop;
    public GameObject Killer;
    public int ItemNumber;
    public int CivilNumber;
    public int CopNumber;
    public int KillerNumber;
    public List<Vector3> ItemsPosition;
    public List<Vector3> CivilsPosition;
    public List<Vector3> CopsPosition;
    public List<Vector3> KillersPosition;

    void Start()
    {

        Spawn();
    }
    
    //Loading 
    public void Spawn()
    {
        SpawnObjects();
        SpawnCivils();
        SpawnCops();
        SpawnKillers();
    }


    void SpawnObjects()
    {
        foreach(Vector3 item in ItemsPosition)
        {
            Vector3 spawnPosition = item;
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Item, spawnPosition, spawnRotation);
        }
    }
    void SpawnCivils()
    {
        foreach(Vector3 civil in CivilsPosition)
        {
            Vector3 spawnPosition = civil;
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Civil, spawnPosition, spawnRotation);
        }
    }
    void SpawnCops()
    {
        foreach (Vector3 cop in CopsPosition)
        {
            Vector3 spawnPosition = cop;
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Cop, spawnPosition, spawnRotation);
        }
    }
    void SpawnKillers()
    {
        foreach (Vector3 killer in KillersPosition)
        {
            Vector3 spawnPosition = killer;
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Killer, spawnPosition, spawnRotation);
        }
    }
}
