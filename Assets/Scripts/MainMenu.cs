using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string gameScene;
    //Play next scene after clicking the botton

    [SerializeField]
    private string selectLevel;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Play the game
    public void Play()
    {
        audioSource.Play();
        SceneManager.LoadScene(gameScene);
        
    }

    public void SelecLevel()
    {
        audioSource.Play();
        SceneManager.LoadScene(selectLevel);
    }

    //Quit the game after clicking the botton
    public void Quit()
    {
        audioSource.Play();
        Application.Quit();
    }

    
}
