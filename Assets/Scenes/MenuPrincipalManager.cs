using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private string GameLevel;
    [SerializeField] private GameObject initialMenu;
    [SerializeField] private GameObject OptionsMenu;
    public void Play()
    {
        SceneManager.LoadScene(GameLevel);
    }

    public void Options()
    {
        initialMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void ExitOpcoes()
    {
        initialMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }

}
