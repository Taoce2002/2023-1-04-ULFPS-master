using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float turnSpeed = 4f;
    [SerializeField]
    private float shootDistance = 4f;
    [SerializeField]
    private float damage = 1f;
    [SerializeField]
    private ParticleSystem shootPS;
    [SerializeField]
    private float health = 100f;
    

    private Rigidbody mRb;
    private Vector2 mDirection;
    private Vector2 mDeltaLook;
    private Transform cameraMain;
    private GameObject debugImpactSphere;
    private GameObject bloodObjectParticles;
    private GameObject otherObjectParticles;

    private void Start()
    {
        mRb = GetComponent<Rigidbody>();
        cameraMain = transform.Find("Main Camera");

        debugImpactSphere = Resources.Load<GameObject>("DebugImpactSphere");
        bloodObjectParticles = Resources.Load<GameObject>("BloodSplat_FX Variant");
        otherObjectParticles = Resources.Load<GameObject>("GunShot_Smoke_FX Variant");
        Debug.Log(health);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        mRb.velocity = mDirection.y * speed * transform.forward 
            + mDirection.x * speed * transform.right;

        transform.Rotate(
            Vector3.up,
            turnSpeed * Time.deltaTime * mDeltaLook.x
        );
        cameraMain.GetComponent<CameraMovement>().RotateUpDown(
            -turnSpeed * Time.deltaTime * mDeltaLook.y
        );

    }

    private void OnMove(InputValue value)
    {
        mDirection = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        mDeltaLook = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        shootPS.Play();

        RaycastHit hit;
        if (Physics.Raycast(
            cameraMain.position,
            cameraMain.forward,
            out hit,
            shootDistance
        ))
        {
            if (hit.collider.CompareTag("Enemigos"))
            {
                var bloodPS = Instantiate(bloodObjectParticles, hit.point, Quaternion.identity);
                Destroy(bloodPS, 3f);
                var enemyController = hit.collider.GetComponent<EnemyController>();
                enemyController.TakeDamage(damage);
            }else if(hit.collider.CompareTag("Spawn")){
                var Spawn = hit.collider.GetComponent<SpawnController>();
                Spawn.TakeDamage(10f);
            }else
            {
                var otherPS = Instantiate(otherObjectParticles, hit.point, Quaternion.identity);
                otherPS.GetComponent<ParticleSystem>().Play();
                Destroy(otherPS, 3f);
            }
            
        }
    }

    public void TakeDamage(float damage)
    {   

        health -= damage;
        Debug.Log(health);
        if (health <= 0f)
        {
            // Fin del juego
            Debug.Log("F");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemigo-Attack"))
        {
            Debug.Log("Player recibio danho");
            TakeDamage(10f);
        }else if(col.CompareTag("Enemigos")){
            Debug.Log("Tocaste");
            TakeDamage(5f);
        }
        
    }

    public void switchShootDistance(){
        if(shootDistance == 4f){
            shootDistance = 50f;
            damage = 100f;
        }
        else{
            shootDistance = 4f;
            damage = 1f;
        }
    }
    public float hp(){
        return health;
    }
}
