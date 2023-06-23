using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    private float hp;
    private GameObject player;
    public GameObject Face;
    public GameObject Face2;
    public GameObject Face3;
    public GameObject Face4;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hp = player.GetComponent<PlayerController>().hp();
    }

    // Update is called once per frame
    void Update()
    {
        hp = player.GetComponent<PlayerController>().hp();

        if(hp<20){
            Face4.SetActive(true);
            Face3.SetActive(false);
            Face2.SetActive(false);
            Face.SetActive(false);
        }else if(hp<40){
            Face4.SetActive(false);
            Face3.SetActive(true);
            Face2.SetActive(false);
            Face.SetActive(false);
        }else if(hp<80){
            Face2.SetActive(true);
            Face4.SetActive(false);
            Face3.SetActive(false);
            Face.SetActive(false);
        }else{
            Face.SetActive(true);
            Face4.SetActive(false);
            Face3.SetActive(false);
            Face2.SetActive(false);
        }
    }
}
