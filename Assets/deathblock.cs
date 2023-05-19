using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathblock : MonoBehaviour
{   
    [SerializeField] GameObject player;

   
    private void OnTriggerEnter(Collider other){
        CharacterController CC = player.GetComponent<CharacterController>();
        if(other.gameObject.CompareTag("Player")){
            CC.enabled = false;
            other.gameObject.transform.position = GameManager.Instance.lastcheckpoint.position;
            CC.enabled = true;
       }
    }
}
