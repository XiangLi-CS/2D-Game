using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelMenu : MonoBehaviour
{
    [SerializeField]
    private string levelOne;

    [SerializeField]
    private string levelTwo;

    [SerializeField]
    private string levelFinal;

    [SerializeField]
    private string mainMenu;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Play next scene after clicking the botton
    public void LevelOne()
    {

        audioSource.Play();
        SceneManager.LoadScene(levelOne);

    }

    public void LevelTwo()
    {

        audioSource.Play();
        SceneManager.LoadScene(levelTwo);

    }

    public void FinalLevel()
    {

        audioSource.Play();
        SceneManager.LoadScene(levelFinal);

    }


    //Quit the game after clicking the botton
    public void Back()
    {
        audioSource.Play();
        SceneManager.LoadScene(mainMenu);
    }
}
