using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{   
    [Header("Layer Masks")]
    [SerializeField] private LayerMask _groundlayer;
    [SerializeField] bool lockaim = false;
    //[SerializeField]
    //private float sensitivity = 0.5f;
    [SerializeField]
    private float _groundraycastlength;
    [SerializeField]
    private float _hangtime = 0.1f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpey = 20f;
    public float moveSmooth = 0.1f;
    [SerializeField]
    private float _moveSpeed = 7.5f;
    [SerializeField]
    public Text debug;
    private bool _onground;
    private float _hangtimeCount;
    private float _jumpSpeed = 7.5f;
    private BoxCollider _hitbox;
    private CharacterController _controller;
    private float _directionY;
    private Animator animator;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelo = Vector2.zero;
    float smoothturn;
    public Transform cam;
    [SerializeField] bool lockCursor = true;
    // Start is called before the first frame update
    void Start()
    {  
        animator = GetComponent<Animator>();
        _groundcollisions();
        _controller = GetComponent<CharacterController>();
        _hitbox = GetComponent<BoxCollider>();
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (Input.GetKeyUp(KeyCode.P)){
            if(SceneManager.GetActiveScene().buildIndex == 4){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-4);
            }else{
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        }
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        _groundcollisions();
        //Debug.Log(_onground.ToString());
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput,0f,verticalInput);
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));  

        //horizontalInput.Normalize();
        //verticalInput.Normalize();
        direction.Normalize();

        if(_onground || _controller.isGrounded ){
            _hangtimeCount = _hangtime;
            if(_controller.isGrounded){
                _directionY = 0;
            }
        }else{
            debug.text = "not ground";
           _directionY -= _gravity * Time.deltaTime;
           _hangtimeCount -= Time.deltaTime;

        }
    
        if(_hangtimeCount > 0f){
            debug.text = "grounded"; 
            if(Input.GetButton("Jump")){
                _directionY = _jumpey;
                _onground = false;
                _hangtimeCount = 0f;
            }
        }
        

        //Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x + transform.up * direction.y);
        Vector3 velocity = (transform.up * _directionY);
        _controller.Move(velocity * _jumpSpeed * Time.deltaTime);
        //Debug.Log();
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothturn, moveSmooth);
        if(lockaim){
            transform.rotation = Quaternion.Euler(0f,cam.eulerAngles.y, 0f);
        }else{
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        //_moveSpeed = 3.5f * Time.deltaTime;
        if (direction.magnitude != 0f){
            animator.SetBool("Is moving", true);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _moveSpeed * Time.deltaTime);
        }else
        {
            animator.SetBool("Is moving", false);
        }
    }
    private void _groundcollisions(){
       if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hitinfo, _groundraycastlength)){
           //Debug.Log("true");
           _onground = true;
           Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down)*hitinfo.distance, Color.green);
       }else{
           //Debug.Log("false");
           _onground = false;
           Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down)*_groundraycastlength, Color.red);
       }
    }
    
}
