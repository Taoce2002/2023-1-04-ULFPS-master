using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    
    [SerializeField]
    public float SpawnTime = 60f;
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    private float NextTimeSpawn;
    public GameObject enemy;
    public float hp = 10f;


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(NextTimeSpawn<Time.time){
            Spawn();
        }
    }
    
    public void Spawn(){
        Instantiate(enemy,position1.transform.position,Quaternion.identity);
        
        
        NextTimeSpawn = Time.time + SpawnTime;
        if(10f < SpawnTime ){
            SpawnTime = SpawnTime -5f;
        }
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
