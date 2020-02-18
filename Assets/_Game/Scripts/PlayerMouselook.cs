using UnityEngine;

public class PlayerMouselook : MonoBehaviour
{
    [SerializeField]
    private float sensitivity;

    private float pitch;

    private Transform head;
    
    private void Awake(){
        head = transform.Find("Head");
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity);
        pitch = Mathf.Clamp(pitch - Input.GetAxisRaw("Mouse Y") * sensitivity, -90, 90);
        head.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
