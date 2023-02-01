using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem dust;

    [SerializeField]
    private float speed;        //control player speed

    [SerializeField]
    private float jumpForce;    //control player jump force in order to change multiple gravity areas

    [SerializeField]
    private CircleCollider2D circle;    //set up collision volumes as circle

    [SerializeField]
    private LayerMask ground;

    [Range(0, .3f)] [SerializeField] 
    private float m_MovementSmoothing = .05f;

    [SerializeField]
    private string gameOverMenu;

    [SerializeField]
    private AudioSource footstap;

    [SerializeField]
    private AudioSource jumpSound;

    [SerializeField]
    private AudioSource death;

    [SerializeField]
    int pointsCherry = 100;

    [SerializeField]
    int pointsGem = 500;

    [SerializeField]
    int pointEnemy = 50;

    private Vector3 v = Vector3.zero;

    private Rigidbody2D r;              //provide Newtonian Physics to player charactor

    private Animator animator;          //hierarchical state machines for player movement animation

    private bool facingRight = true;    //Player always faces right side

    //Player Managing State
    public enum PlayerFSM
    {
        idle,
        running,
        jumping,
        falling,
        hurting
    }

    private PlayerFSM state = PlayerFSM.idle;


    private void Awake()
    {
        //Grab references from Rigidbody and animator from object
        r = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        
    }

    private void FixedUpdate()
    {
        if(state != PlayerFSM.hurting)
        {
            
            Movement();
        }

        
        AnimationState();
        animator.SetInteger("State", (int)state);
    }

    //Player movement
    private void Movement()
    {
        //Scoreboard
        PermanentUI.perm.score = PermanentUI.perm.scoreCherry + PermanentUI.perm.scoreGem + PermanentUI.perm.scoreEnemy;
        PermanentUI.perm.scoreText.text = PermanentUI.perm.score.ToString();

        PermanentUI.perm.cherryText.text = PermanentUI.perm.cherry.ToString();

        PermanentUI.perm.gemText.text = PermanentUI.perm.gem.ToString();

        

        //Set keyboard with different direction
        float horizontalmove = Input.GetAxisRaw("Horizontal");

        //Make player move to left and right
        Vector3 playerVelocity = new Vector2(horizontalmove * speed, r.velocity.y);                 //player movement function
        r.velocity = Vector3.SmoothDamp(r.velocity, playerVelocity, ref v, m_MovementSmoothing);

        //Flipping player
        if (horizontalmove > 0f && !facingRight)
        {
            Flip();
        }
        else if (horizontalmove < 0f && facingRight)
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(r.velocity.y) < 0.001f)
        {
            Jump();
        }

    }

    //Player Jump method
    private void Jump()
    {
        JumpSound();

        CreateDust();

        r.velocity = new Vector2(r.velocity.x, jumpForce);

        state = PlayerFSM.jumping;
    }

    //The function of changing the facing direction 
    private void Flip()
    {
        CreateDust();
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Animation Generator for Player FSM
    private void AnimationState()
    {
        if(state == PlayerFSM.jumping)
        {
            if(r.velocity.y < 0.1f)
            {
                state = PlayerFSM.falling;
            }
        }
        else if(state == PlayerFSM.falling)
        {
            if(circle.IsTouchingLayers(ground))
            {
                state = PlayerFSM.idle;
            }
        }
        else if(state == PlayerFSM.hurting)
        {
            
            state = PlayerFSM.hurting;
            

        }
        else if(Mathf.Abs(r.velocity.x) > 1f)
        {
            state = PlayerFSM.running;
        }
        else
        {
            state = PlayerFSM.idle;
        }
    }

    private void Footstep()
    {
        footstap.Play();
    }

    private void DeathSound()
    {
        death.Play();
    }

    private void JumpSound()
    {
        jumpSound.Play();
    }

    private void CreateDust()
    {
        dust.Play();
    }

    //Menu switching time delay
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(0.62f);
        SceneManager.LoadScene(gameOverMenu);   //loading next scene after 0.6f
    }

    //Reset Powerup abilities after 10f
    private IEnumerator ResetPowerup()
    {
        yield return new WaitForSeconds(8f);
        jumpForce = 11f;                                       //change gravity force back to normal
        GetComponent<SpriteRenderer>().color = Color.white;     //change color to original
        
        //changing player scale and mass back to normal
        if (facingRight)
        {
            transform.localScale += new Vector3(-2, -2, 0);
        }
        else
        {
            transform.localScale += new Vector3(2, -2, 0);
        }
    }


    //Collision Detection for player character
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Dectecting player falling 
        if (collision.gameObject.tag == "Air")
        {
            
            state = PlayerFSM.hurting;      //Set Animator parameters for hurting

            circle.enabled = false;         //Disable circle collider

            PermanentUI.perm.Reset();

            StartCoroutine(WaitForSceneLoad());     //Change to menu scene
            



        }

        if (collision.gameObject.tag == "NearCheckPoint")
        {

            state = PlayerFSM.hurting;      //Set Animator parameters for hurting

            circle.enabled = false;         //Disable circle collider

            PermanentUI.perm.Reset();

            StartCoroutine(WaitForSceneLoad());

        }

        //When colliding enermy, player will be killed or kill enermies by jumping
        if (collision.gameObject.tag == "Enermy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            //Detecting whether player is jumping or not
            if (state == PlayerFSM.falling)
            {
                //Enemy Death effect
                enemy.DeathEffect();

                Jump();

                PermanentUI.perm.scoreEnemy += pointEnemy;


            }
            else
            {
                
                state = PlayerFSM.hurting;      //Set Animator parameters for hurting
                
                circle.enabled = false;

                PermanentUI.perm.Reset();

                StartCoroutine(WaitForSceneLoad());     //Change to menu scene


            }

        }

        //When colliding Frog, only player will be destroy
        if (collision.gameObject.tag == "Frog")
        {
            
            state = PlayerFSM.hurting;

            circle.enabled = false;

            PermanentUI.perm.Reset();

            StartCoroutine(WaitForSceneLoad());         //Change to menu scene

        }

        //When colliding Frog, only player will be destroy
        if (collision.gameObject.tag == "FlockEnemy")
        {

            state = PlayerFSM.hurting;

            circle.enabled = false;

            PermanentUI.perm.Reset();

            StartCoroutine(WaitForSceneLoad());         //Change to menu scene

        }

    }

    //Dectecting different triggers and destroy them (Collision Response and Feedback)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When colliding Cherry, player will gain points
        if (collision.gameObject.tag == "Cherrys")
        {
            PermanentUI.perm.scoreCherry += pointsCherry;
            PermanentUI.perm.cherry++;

        }

        //When colliding Gem, player will gain more points
        if (collision.gameObject.tag == "Gems")
        {
            PermanentUI.perm.scoreGem += pointsGem;
            PermanentUI.perm.gem++;
        }

        //When colliding Powerup, player will be powerup and have special effects
        if (collision.gameObject.tag == "PowerUp")
        {
            
            jumpForce = 18f;        //change gravity force

            
            GetComponent<SpriteRenderer>().color = Color.yellow;        //change player color

            //change player scale and mass
            if (facingRight)
            {
                transform.localScale += new Vector3(2, 2, 0);
            }
            else
            {
                transform.localScale += new Vector3(-2, 2, 0);
            }

            
            StartCoroutine(ResetPowerup());         //reset powerup
            
        }

    }



}
