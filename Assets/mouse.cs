using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mouse : MonoBehaviour
{
    [SerializeField] bool lockCursor = true;
    // Start is called before the first frame update
    void Start()
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
