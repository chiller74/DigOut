using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Ball initialBall;
    public Ball ballPrefab;
    public BlockLayer blockLayerPrefab;
    public Paddle paddle;
    public Camera maincamera;
    public Text DepthText;
    public Text PointsText;
    public Text ShotsText;
    public Text LivesText;
    public AudioSource LoseLife;
    public AudioSource _GameOver;
    public AudioSource Reload;
    public AudioSource _BrickBreak;
    public AudioSource _BrickLoseLife;
    public Button RestartButton;

    public static AudioSource BrickBreak;
    public static AudioSource BrickLoseLife;
    public static AudioSource GameOver;


    public static bool isGameOver = false;

    public static bool startGame = false;
    public static bool gameStarted = false;
    public static bool awaitingRespawn = false;
    public static bool CanFireCannon = false;
    public static int points=0;
    public static int depth=0;
    public static int Shots=20;
    public static int Lives=5;
    int nextLevelAt = 30;

    Button button;

    // Start is called before the first frame update
    void Start()
    {
        RestartButton.onClick.AddListener(Restart);

        button = FindObjectOfType<Button>();
        button.gameObject.SetActive(false);

        Cursor.visible = false;

        BrickBreak = _BrickBreak;
        BrickLoseLife = _BrickLoseLife;
        GameOver = _GameOver;

        InitializeBall();
        initializeBlockLayer(4);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 paddlePos = paddle.gameObject.transform.position;
        Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y - .25f, 0);

        if(isGameOver)
        {
            Cursor.visible = true;
        }

        if(!isGameOver)
        {
            Cursor.visible = false;
        }

        if(Input.GetMouseButtonDown(0) && startGame == false)
        {
            startGame = true;
            CanFireCannon = true;
        }

        if(awaitingRespawn && Lives >= 0)
        {
            InitializeBall();
            LoseLife.Play();
            startGame = false;
            awaitingRespawn = false;
        }


        if(!startGame)
        {
            initialBall.transform.position = ballPos;
        }

        PointsText.text = points.ToString();


        if(points >= nextLevelAt && !isGameOver)
        {
            initializeBlockLayer(1);
            nextLevelAt+=30;

            depth++;
            DepthText.text = depth.ToString();
        }

        if(Shots <= 0 && !isGameOver)
        {
            Shots =20;
            Reload.Play();
            initializeBlockLayer(1);
        }

        if(isGameOver)
        {
            button.gameObject.SetActive(true);
            gameStarted = false;
        }

        ShotsText.text = GetDotDisplay(Shots);
        LivesText.text = GetDotDisplay(Lives);

    }

    void InitializeBall()
    {
        Vector3 paddlePos = paddle.gameObject.transform.position;
        Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y - .25f, 0);

        initialBall = Instantiate(ballPrefab, ballPos, Quaternion.identity);
    }


    void initializeBlockLayer(int numLayers)
    {
        for (var i = 0; i < numLayers; i++)
        {
            BlockLayer[] blockLayers = FindObjectsOfType(typeof(BlockLayer)) as BlockLayer[];

            if(blockLayers.Length > 0)
            {
                foreach (var item in blockLayers)
                {
                    Vector3 temp = item.transform.position;
                    temp.y += .5f;
                    item.transform.position = temp;
                }
            }

            Instantiate(blockLayerPrefab, new Vector3(0,-4,0), Quaternion.identity);
        }
    }






    string GetDotDisplay(int n) 
    {
        string dots = "";
        for (var i = 0; i < n; i++)
        {
            dots += ".";
        }
    
        return dots;
    }



    void Restart()
    {
        button.gameObject.SetActive(false);

        points = 0;
        depth = 0;
        Lives = 5;
        Shots = 20;

        initializeBlockLayer(4);

        isGameOver = false;
    }
}
