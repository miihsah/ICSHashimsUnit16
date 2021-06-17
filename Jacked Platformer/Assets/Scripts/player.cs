using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class player : MonoBehaviour
{
    // Configurations 

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathJump = new Vector2(25f, 25f);
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource footstep;
    [SerializeField] public AudioSource bgMusic;
    [SerializeField] public AudioSource deathSound;
    //States 
    bool isAlive = true; //Player is alive

    // Cached component references 
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D BodyCollider2D; // Capsule collider for player body
    BoxCollider2D Feet;

    // Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        BodyCollider2D = GetComponent<CapsuleCollider2D>();
        Feet = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) {
            return;
        }
        Run();
        Jump();
        FlipSprite();
        Death();
       
    }
    //Method allows player to run by pressing left or right arrow key (UPDATE NEEDED FOR MOBILE)
    private void Run()
    {
        
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        


        // Player always moving at running speed  
        bool playerMovingHoriz = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        
       
        myAnimator.SetBool("RUnning", playerMovingHoriz);
    }
    //Method allows player to jump by pressing space (UPDATE NEEDED FOR MOBILE)
    private void Jump() {
        // Makes sure player can only jump when they are touching the ground layer
        if (!Feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        
        // Lets the player jump by changing y velocity 
        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            jump.Play();//play jump sound
            Vector2 jumpVelocityAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityAdd;// make player jump
        }
    }
    //Flips the sprite of the player when the direction of horizontal movement is changed 
    private void FlipSprite() {
        // if player is moving horizontally 
        bool playerMovingHoriz = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerMovingHoriz)
        {
            // Vector2 becomes +1 or -1 depending on the sign of the movement, y stays the same, flips the character sprite
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        
        }

    }
    //Method that determines if the player has touched a hazard and then kills the player 
    private void Death() 
    {
        // if the player is touching the enemy or hazard
        bool PlayedDeathSound = false;
        if (BodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            // Turns off background music if it is playing 
            if (bgMusic.isPlaying) {
                bgMusic.Stop();
            }
            // Plays deathsound if it has not already been played
            if (!deathSound.isPlaying && PlayedDeathSound == false)
            {
                deathSound.Play();
                PlayedDeathSound = true;//prevents infinite loop
            }
            //Player is dead
            isAlive = false;
            //Set animation to death, throw the body of the player into the air 
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathJump;
            FindObjectOfType<GameSession>().ProcessPlayerDeath(); // Get ProcessPlayerDeath method from GameSession script process death
        }
    }
    //method for footstep sound during running animation 
    private void FootStep() {
        // if the player is in the air dont play the footstep sound effect
        if (!Feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        else footstep.Play();
        }
    
    }
    


