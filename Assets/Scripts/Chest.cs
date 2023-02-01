using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    private Animator animator;

    public GameObject itemLoot;

    private void Start()
    {

        animator = GetComponent<Animator>();

    }
    //Random spawn item loot after few seconds
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(itemLoot, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.6f);

    }
    
    //When player reach the chest, chest open
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //checkPoint.lastCheckPoint = transform.position;
            animator.SetBool("IsOpen", true);
            StartCoroutine(WaitForSceneLoad());
            
        }
    }

    
}
