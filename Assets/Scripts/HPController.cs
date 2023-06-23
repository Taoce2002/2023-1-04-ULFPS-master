using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPController : MonoBehaviour
{   
    private float hp;
    public TextMeshProUGUI  hpText;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        hp = player.GetComponent<PlayerController>().hp();
    }

    // Update is called once per frame
    void Update()
    {
        hp = player.GetComponent<PlayerController>().hp();

        if(hp< 0){
            hp= 0;
            hpText.text = hp+" HP";
        }
        hpText.text = hp+" HP";
    }
}
