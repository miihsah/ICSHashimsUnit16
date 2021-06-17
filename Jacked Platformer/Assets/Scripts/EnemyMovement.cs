using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D Body;

    void Start()
    {
        // Get enemys rigid body
        Body = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        if (isFacingRight()) {
            // Enemy moves right across the screen 
            Body.velocity = new Vector2(moveSpeed, 0f);

        }
        else
        {
            // Enemy moves left across the screen 
            Body.velocity = new Vector2(-moveSpeed, 0f);
        }
        
    }
    // Determines if enemy is facing righr
    bool isFacingRight() 
    {
        // If this value is positive, it means enemy is facing right
        return transform.localScale.x > 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // If enemy is moving right, sign is +1, -1 if left, this will change the sign of scale so enemy goes in opposite direction 
        transform.localScale = new Vector2(-(Mathf.Sign(Body.velocity.x)), 1f); 
    }
}
