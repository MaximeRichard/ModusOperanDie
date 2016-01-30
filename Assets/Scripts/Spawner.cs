using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public List<Item> Items;
    public List<Victim> Civils;
    public List<Cop> Cops;
    public List<Killer> Killers;

    //variable static  qui ne peut exister qu'une fois dans le programme
    private static Spawner spawner = null;

    void Start()
    {
        //SAFE CODE : Check si il existe un autre gamemanager si c'est le cas, je m'autodetruit
        if (spawner != null && spawner != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // pour qu'on puisse y accéder dans les methodes statiques
        spawner = this;
    }

    // fonction a appeller qui retourne le script gameManager
    public static Spawner getInstance()
    {
        return spawner;
    }
    
    //Loading 
    public static void Spawn()
    {
        spawner.SpawnObjects();
        spawner.SpawnCivils();
        spawner.SpawnCops();
        spawner.SpawnKillers();
    }


    void SpawnObjects()
    {
        foreach(Item item in Items)
        {
            Vector3 spawnPosition = new Vector3(2, 2, 0);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(item, spawnPosition, spawnRotation);
        }
    }
    void SpawnCivils()
    {
        foreach(Victim civil in Civils)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(civil, spawnPosition, spawnRotation);
        }
    }
    void SpawnCops()
    {
        foreach (Cop cop in Cops)
        {
            Vector3 spawnPosition = new Vector3(-6, -3, 0);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(cop, spawnPosition, spawnRotation);
        }
    }
    void SpawnKillers()
    {
        foreach (Killer killer in Killers)
        {
            Vector3 spawnPosition = new Vector3(-2, -2, 0);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(killer, spawnPosition, spawnRotation);
        }
    }
}
