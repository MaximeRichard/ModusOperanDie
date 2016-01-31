using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //Basic GameStates for the menu, In Game and game Ending
    public enum GameState
    {
        Start,
        InGame,
        End
    }

    public List<GameObject> Killers;
    private List<PickUp> PickUps;
    public Vector2 SpawnValues;

    //UI
    public List<Killer.PickUpData> weapons;
    public List<Killer.PickUpData> victims;
    public List<Killer.PickUpData> signatures;
    /*public Dictionary<Killer, Color> colors;
    public Dictionary<Killer, Killer.PickUpData> victims;
    public Dictionary<Killer, Killer.PickUpData> weapons;*/
    //END UI


    GameState gameState;

    //variable static  qui ne peut exister qu'une fois dans le programme
    private static GameController gameController = null;


        // Use this for initialization
        void Start()
    {
        //SAFE CODE : Check si il existe un autre gamemanager si c'est le cas, je m'autodetruit
        if (gameController != null && gameController != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // pour qu'on puisse y accéder dans les methodes statiques
        gameController = this;

        // L'objet n'est pas détruit quand on change de scène
        DontDestroyOnLoad(this.gameObject);
        SettingTarget();
        //chargement de menu
        //Application.LoadLevel("Menu");
        //Application.LoadLevel("arena");
        foreach (GameObject go in Killers)
        {
            LoadObjectives(go);
        }
    }

    // fonction a appeller qui retourne le script gameManager
    public static GameController getInstance()
    {
        return gameController;
    }

    //Accesseurs de GameState
    public static GameState GetGameState()
    {
        return gameController.gameState;
    }
    public static void SetGameState(GameState state)
    {
        gameController.gameState = state;
    }

    // Update is called once per frame
    void Update () {
	
	}

    //OnWin when a killer has accomplished his ritual
    void OnWin()
    {

    }

    //To be put inside MenuManager
    void LaunchGame()
    {
        //SceneManager.LoadScene(); //Add Scene build index
        Application.LoadLevel("arena"); //Is obsolete
    }


    //UI
    //Arme Signature Victime
    //Arme Signature

        //2 Listes
        //Listes d'objets 2*2 Armes 2*2 Victimes
        //Dico
        //A vider dans les dicos correspondant
        //Signatures assignées dans KillerTargetSignature
        //
    void SettingTarget()
    {
        Killer.PickUpData pu;
        pu.Type = PickUp.PickUpType.Signature;
        pu.Name = "AppareilPhoto";
        signatures.Add(pu);
        pu.Name = "Chat";
        signatures.Add(pu);
        pu.Name = "Huitre";
        signatures.Add(pu);
        pu.Name = "Rose";
        signatures.Add(pu);

        Killer.PickUpData pu2;
        pu2.Type = PickUp.PickUpType.Weapon;
        pu2.Name = "Canard";
        weapons.Add(pu2);
        pu2.Name = "Hache";
        weapons.Add(pu2);
        pu2.Name = "Marteau";
        weapons.Add(pu2);
        pu2.Name = "Rape";
        weapons.Add(pu2);

        Killer.PickUpData pu3;
        pu3.Type = PickUp.PickUpType.Victim;
        pu3.Name = "BW";
        victims.Add(pu3);
        pu3.Name = "RW";
        victims.Add(pu3);
        pu3.Name = "BS";
        victims.Add(pu3);
        pu3.Name = "HS";
        victims.Add(pu3);
    }
    void BrowseLists()
    {

    }
    //Vert Rouge Bleu Violet
    /*void FillDictionnary()
    {
        colors.Add(Killers[0], Color.green);
        colors.Add(Killers[1], Color.red);
        colors.Add(Killers[2], Color.blue);
        colors.Add(Killers[3], Color.magenta);

        foreach (Killer kil in Killers)
        {
            int i = 0;
            foreach (Killer.PickUpData obj in objects)
            {
            if (objects[i].Type == PickUp.PickUpType.Weapon)
                {
                    weapons.Add(kil, objects[i]);
                    kil.TargetWeapon = objects[i].Name;
                }
            }
            i++;
        }
        foreach (Killer kil in Killers)
        {
            int i = 0;
            foreach (Killer.PickUpData obj in objects)
            {
                if (objects[i].Type == PickUp.PickUpType.Victim)
                {
                    victims.Add(kil, objects[i]);
                    kil.TargetVictim = objects[i].Name;
                }
            }
            i++;
        }

        foreach (Killer kil in Killers)
        {
            int i = 0;
                kil.TargetSignature = signatures[i].Name;
            i++;
        }
    }*/

    void LoadObjectives(GameObject k)
    {
        HUD hud = k.GetComponent<HUD>();
        Killer killer = k.GetComponent<Killer>();
        hud.ObjectiveWeapon.sprite = Resources.Load<Sprite>("Sprites/photoArmes/photo" + killer.TargetWeapon);
        hud.ObjectiveSignature.sprite = (Sprite)Resources.Load<Sprite>("Sprites/photoObjets/photo" + killer.TargetSignature);
        hud.ObjectiveVictim.sprite = (Sprite)Resources.Load<Sprite>("Sprites/photoPersos/" + killer.TargetVictim);
    }

}
