using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLayer : MonoBehaviour
{
    public Block BasicBlockPrefab;
    public Block MultiBlockPrefab;
    public Block ExplosiveBlockPrefab;
    // Start is called before the first frame update
    void Start()
    {
        CreateBlock(0);
        CreateBlock(10);
        CreateBlock(20);
        CreateBlock(30);
        CreateBlock(40);
        CreateBlock(50);
        CreateBlock(-10);
        CreateBlock(-20);
        CreateBlock(-30);
        CreateBlock(-40);
        CreateBlock(-50);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.childCount == 0)
        {
            Destroy(this.gameObject);

            if(!GameManager.isGameOver)
            {
                 GameManager.points += 10;
            }
        }

        if(gameObject.transform.position.y>1)
        {
            GameManager.Lives = -5;
            Debug.LogError("TOO HIGH! GAME OVER!");
        }
    }


    void CreateBlock(int x)
    {
        var i = Random.Range(0,100);
        Block blocktype = BasicBlockPrefab;

        if(i<70){ blocktype = BasicBlockPrefab; }
        else if(i<95){ blocktype = MultiBlockPrefab; }
        else if(i<100){ blocktype = ExplosiveBlockPrefab; }

        var block = Instantiate(blocktype, new Vector3(0,0,0), Quaternion.identity);

        
        block.transform.parent = gameObject.transform;
        block.transform.localPosition = new Vector3(x,0,0);
        block.transform.localScale = new Vector3(1,1,0);
    }
}
