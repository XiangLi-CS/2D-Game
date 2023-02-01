using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private string gameLevel;

    [SerializeField]
    private string mainMenu;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Play next scene after clicking the botton
    public void Play()
    {

        audioSource.Play();
        SceneManager.LoadScene(gameLevel);

    }

    //Quit the game after clicking the botton
    public void Back()
    {
        audioSource.Play();
        SceneManager.LoadScene(mainMenu);
    }
}