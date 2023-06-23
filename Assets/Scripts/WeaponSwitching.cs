using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    private GameObject player;
    private bool timeChange = true;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        SelectWeapon();
    }

    void Update(){

        int previousSelectedWeapon = selectedWeapon;

        if(Input.GetKey(KeyCode.Tab))
        {
            if(timeChange){

                if(selectedWeapon >= transform.childCount - 1){
                    selectedWeapon = 0;
                }
                else{
                    selectedWeapon++;
                    player.GetComponent<PlayerController>().switchShootDistance();
                }
                //StartCoroutine(SwitchDelay());
            
            }
        }

        if(previousSelectedWeapon != selectedWeapon){
            SelectWeapon();
        }
    }
    
    void SelectWeapon(){
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    private IEnumerator SwitchDelay(){
        timeChange = false;
        yield return new WaitForSeconds (0.5f);
        timeChange = true;
    }
    
}
