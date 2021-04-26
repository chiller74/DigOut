using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    int downwardVelocity = -8;
    int nextIncrease = 100;

    // Start is called before the first frame update
    void Start()
    {
        downwardVelocity = -8;
        if(GameManager.points != 0 && gameObject.tag != "Projectile")
        {
            rb.velocity = new Vector2(Random.Range(-8,9), -8);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(gameObject.tag != "Projectile")
        {
            MovementCorrection();
        }

        if(this.transform.position.y > 5.5 || GameManager.Lives == -5)
        {
            Destroy(this.gameObject);
            if(gameObject.tag != "Projectile" && GameManager.Lives <= 0)
            {
                Debug.LogError("GAME OVER");
                GameManager.GameOver.Play(); 
                

                GameObject[] bl = GameObject.FindGameObjectsWithTag("BlockLayer");

                foreach (var item in bl)
                {
                    Destroy(item);
                }

                
                GameManager.isGameOver = true;
                
            }
        }

        if(GameManager.points>=nextIncrease && gameObject.tag != "Projectile")
        {
            downwardVelocity--;
            nextIncrease+=100;

            rb.velocity = new Vector2(rb.velocity.x, downwardVelocity);
        }
    }

    void Movement()
    {
        if(GameManager.startGame && !GameManager.gameStarted && gameObject.tag != "Projectile")
        {
            rb.velocity = new Vector2(Random.Range(-8,9),-8);
            GameManager.gameStarted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Block")
        {
            collision.gameObject.GetComponent<Block>().Lives --;
            GameManager.BrickLoseLife.Play();

            if(collision.gameObject.GetComponent<Block>().Lives == 0)
            {
                Destroy(collision.gameObject);
            }
        }
        if(gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }
    }

    void MovementCorrection()
    {
        if( ( -.5 <= rb.velocity.x && rb.velocity.x <= .5 ) || ( -.5 <= rb.velocity.y && rb.velocity.y <= .5 ))
        {
            rb.velocity = new Vector2(-3,downwardVelocity);
        }
    }


    private void OnDestroy() 
    {
        if(spriteRenderer.color == Color.white)
        {
            GameManager.awaitingRespawn = true;
            GameManager.CanFireCannon = false;
            GameManager.Lives--;
        }
    }
}
