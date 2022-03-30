using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    private Slider slider;
    private GameManager manager;
    
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        slider = GetComponent<Slider>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetComponent<PlayerMovement>().curHp / 100f;
        if(player.GetComponent<PlayerMovement>().curHp == 0)
        {
            slider.transform.Find("Fill Area").gameObject.SetActive(false);
            manager.isGameOver = true;
            
        }
    }
}
