using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Camera mainCamera;
    public Ball Projectile;
    public AudioSource ShotFired;
    public static int paddleMovementDir = 0;


    float leftmax;
    float rightmax;
    float lastpos;
    int projMod = 1;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();

        paddleMovementDir = GetPaddleMovementDir();

        lastpos = this.transform.position.x;

        if(Input.GetMouseButtonDown(1) && GameManager.CanFireCannon)
        {
            FireProjectile();
            ShotFired.Play();
            projMod *= -1;

            GameManager.Shots--;
        }
    }

    void movement()
    {
        rightmax = mainCamera.WorldToScreenPoint(new Vector3(5.5f,0f,0f)).x;
        leftmax = mainCamera.WorldToScreenPoint(new Vector3(-5.5f,0f,0f)).x;    

        float maxMovement = Mathf.Clamp(Input.mousePosition.x, leftmax, rightmax);

        float worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(maxMovement,0,0)).x;

        this.transform.position = new Vector3(worldPosition, 4, 0);
    }

    int GetPaddleMovementDir()
    {
        if(lastpos > this.transform.position.x)
        {
            return -1;
        }
        else if(lastpos < this.transform.position.x)
        {
            return 1;
        }

        return 0;
    }

    void FireProjectile()
    {
        var projectile = Instantiate(Projectile, new Vector3(0,0,0), Quaternion.identity);

        
        projectile.transform.position = gameObject.transform.position + new Vector3(1.15f*projMod,-.2f,0);
        projectile.rb.velocity = new Vector2(0,-10);
    }

}
