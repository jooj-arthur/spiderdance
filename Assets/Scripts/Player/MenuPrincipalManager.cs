using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private string GameLevel;
    [SerializeField] private GameObject initialMenu;
    [SerializeField] private GameObject ControlsMenu;

    public void Play()
    {
        SceneManager.LoadScene(GameLevel);
        ControlsMenu.SetActive(false);
    }

    public void Options()
    {
        initialMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    public void ExitOpcoes()
    {
        initialMenu.SetActive(true);
        ControlsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }

}
