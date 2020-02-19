using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]

    [Header("Speed")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float duckSpeed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float speedChangeTime;

    [Header("Height")]
    [SerializeField]
    private float defHeight;
    [SerializeField]
    private float duckHeight;
    [SerializeField]
    private float changeTime;

    [Header("Masks")]
    [SerializeField]
    private LayerMask duckMask;

    private float speed;
    private float speedChange;
    private float height;
    private float refDuckVel;

    private RaycastHit duckHit;
    
    private Vector3 inp;
    private Vector3 vel;
    private Vector3 refVel;
    
    private bool isMoving;
    
    private CharacterController cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        height = defHeight;
    }

    private void Update()
    {
        inp = Vector3.Normalize(new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")));

        isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

        vel = Vector3.SmoothDamp(vel, transform.rotation * inp * speed * Time.deltaTime, ref refVel, 0.1f);

        Gravity(ref vel);

        transform.localScale = new Vector3(1, Duck(), 1);

        cc.Move(vel);

        Debug.DrawRay(transform.position, Vector3.up * defHeight * 2, Color.red);
    }
    
    private void Gravity(ref Vector3 vel){
        if(cc.isGrounded){
            vel.y = -0.1f;
            if(Input.GetKeyDown(KeyCode.Space)){
                vel.y = jumpPower;
            }
        }else{
            vel.y += Physics.gravity.y / 10 * Time.deltaTime;
        }

        if((cc.collisionFlags & CollisionFlags.Above) != 0 && vel.y > 0){
            vel.y = 0;
        }
    }

    private float Duck(){
        if(Input.GetKey(KeyCode.LeftControl)){
            height = Mathf.SmoothDamp(height, duckHeight, ref refDuckVel, changeTime);
            speed = Mathf.SmoothDamp(speed, duckSpeed, ref speedChange, speedChangeTime);
        }else{
            if(!Physics.Raycast(transform.position, Vector3.up, out duckHit, defHeight * 2, duckMask)){
                height = Mathf.SmoothDamp(height, defHeight, ref refDuckVel, changeTime);
                speed = Mathf.SmoothDamp(speed, walkSpeed, ref speedChange, speedChangeTime);
            }
        }
        return height;
    }

}
