using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    //[SerializeField] List<GameObject> checkpoints;
    //[SerializeField] Transform spawnPoint;
    [SerializeField] float spawnValue;


    // Update is called once per frame
    void Update()
    {
        CharacterController CC = player.GetComponent<CharacterController>();
        if(player.transform.position.y < -spawnValue)
        {   
            CC.enabled = false;
            player.transform.position = GameManager.Instance.lastcheckpoint.position;
            CC.enabled = true;
        }
        //Debug.Log(player.transform.position.y);
        
    }
    
    

}
