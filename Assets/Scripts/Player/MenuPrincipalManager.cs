using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private string GameLevel;
    [SerializeField] private GameObject initialMenu;
    [SerializeField] private GameObject ControlsMenu;

    void Start()
    {
        ReiniciarMenu();
    }

    public void Play()
    {
        StartCoroutine(LoadGameLevel());
    }

    private IEnumerator LoadGameLevel()
    {
        yield return SceneManager.LoadSceneAsync(GameLevel);
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

    private void ReiniciarMenu()
    {
        initialMenu.SetActive(true);
        ControlsMenu.SetActive(false);
    }

}
