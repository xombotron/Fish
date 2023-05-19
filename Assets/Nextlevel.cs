using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextlevel : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private void OnTriggerEnter(Collider other){
        if(SceneManager.GetActiveScene().buildIndex == 4){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-4);
        }else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        
    }
}
