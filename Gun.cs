using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer.color = new Color(255f/255,0f/255,0f/255);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
