using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{

    public int Lives;
    public bool Explodes;
    public Ball Projectile;


    public SpriteRenderer spRenderer;
    // Start is called before the first frame update
    void Start()
    {
        if(Lives == 20)
        {
            Lives = Random.Range(2,4);
        }
    }


    Color deepGreen = new Color(15f/255,90f/255,20f/255);
    Color deepBlue = new Color(20f/255,15f/255,90f/255);

    // Update is called once per frame
    void Update()
    {
        if(Lives == 1 && !Explodes)
        {
            spRenderer.color = Color.grey;
        }
        if(Lives == 2)
        {
            spRenderer.color = deepGreen;
        }
        if(Lives == 3)
        {
            spRenderer.color = deepBlue;
        }
    }

    private void OnDestroy() 
    {
        if(Explodes && GameManager.CanFireCannon)
        {
            CreateProjectile(0,1,0,.2f);
            CreateProjectile(1,1,.2f,.2f);
            CreateProjectile(1,0,.2f,0);
            CreateProjectile(1,-1,.2f,-.2f);
            CreateProjectile(0,-1,0,-.2f);
            CreateProjectile(-1,-1,-.2f,-.2f);
            CreateProjectile(-1,0,-.2f,0);
            CreateProjectile(-1,1,-.2f,.2f);
        }
            
        if(!GameManager.isGameOver)
        {
            GameManager.points++;
            GameManager.BrickBreak.Play();
        }
    }

    void CreateProjectile(int x, int y, float px, float py)
    {
        var shrapnel = Instantiate(Projectile, new Vector3(0,0,0), Quaternion.identity);
        shrapnel.transform.position = gameObject.transform.position + new Vector3(px,py,0);

        shrapnel.rb.velocity = new Vector2 (x*5, y*5);
    }

}
