using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    //Basic GameStates for the menu, In Game and game Ending
    public enum GameState
    {
        Start,
        InGame,
        End
    }

    private List<Killer> Killers;
    private List<PickUp> PickUps;
    public Vector2 SpawnValues;

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

        //chargement de menu
        //Application.LoadLevel("Menu");
        Application.LoadLevel("arena");
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
}
