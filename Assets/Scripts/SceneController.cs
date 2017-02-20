using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private GameObject GameOver;

    public bool isDead = false;

    // Use this for initialization
    void Start()
    {
        GameOver = GameObject.Find("Canvas/GameOver");
        GameOver.SetActive(isDead);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            ShowAlert();

            if(Input.GetButton(Constants.A))
            {
                ReloadScene();
            }
        }
    }

    void ShowAlert()
    {
        GameOver.SetActive(isDead);
    }

    void ReloadScene()
    {
        isDead = false;
        GameOver.SetActive(isDead);
        SceneManager.LoadScene(0);
    }
}
