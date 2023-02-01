using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermanentUI : MonoBehaviour
{
    public int scoreCherry = 0;

    public int scoreGem = 0;

    public int scoreEnemy = 0;

    public int score = 0;

    public int cherry = 0;

    public int gem = 0;

    public Text scoreText;

    public Text cherryText;

    public Text gemText;

    public static PermanentUI perm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //Store the score for entire game
        if (!perm)
        {
            perm = this;
        }
        else
        {
            
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        score = 0;
        cherry = 0;
        gem = 0;
        scoreCherry = 0;
        scoreGem = 0;
        scoreEnemy = 0;
        scoreText.text = score.ToString();
        cherryText.text = cherry.ToString();
        gemText.text = gem.ToString();
    }
}
