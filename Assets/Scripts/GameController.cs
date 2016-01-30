using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    private List<Killer> Killers;
    private List<PickUp> PickUps;
    public Vector2 SpawnValues;
    public GameObject Item;
    public GameObject Civil;
    public GameObject Cop;
    // Use this for initialization
    void Start()
    {
        //SpawnObjects();
        //SpawnCivils();
        SpawnCops();
    }

        // Update is called once per frame
        void Update () {
	
	}

    void CheckForWin()
    {

    }
    /*void SpawnObjects()
    {
        for (int i = 0; i < 6; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), Random.Range(-SpawnValues.y, SpawnValues.y), 0);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Item, spawnPosition, spawnRotation);
        }
    }*/
    /*void SpawnCivils()
    {
        for (int i = 0; i <10; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), Random.Range(-SpawnValues.y, SpawnValues.y), 0);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Civil, spawnPosition, spawnRotation);
        }
    }*/
    void SpawnCops()
    {
            Vector3 spawnPosition = new Vector3(-6, -3, 0);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Cop, spawnPosition, spawnRotation);
    }
}
