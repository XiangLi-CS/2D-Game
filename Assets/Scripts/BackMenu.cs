using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{
    [SerializeField]
    private string mainMenu;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Quit the game after clicking the botton
    public void Back()
    {
        audioSource.Play();
        PermanentUI.perm.Reset();
        SceneManager.LoadScene(mainMenu);
    }
}
