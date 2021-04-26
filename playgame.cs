using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class playgame : MonoBehaviour
{
    public Button button;
    public Text Points;
    public Text Depth;

    private void Start() 
    {
        button.onClick.AddListener(onClick);
    }

    void onClick()
    {
        SceneManager.LoadScene(1);
    }


}
