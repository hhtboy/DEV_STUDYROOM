using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        isGameOver = false;
        //GameObject obj = player.GetComponentInChildren<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            isGameOver = false;
            SceneManager.LoadScene("Main");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("BattleScene");
    }

}
