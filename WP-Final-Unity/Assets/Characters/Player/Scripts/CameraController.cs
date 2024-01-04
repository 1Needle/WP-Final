using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform WatchAt;
    [SerializeField] float cameraHeight = 5f;
    [SerializeField] float cameraDistance = 10f;
    [SerializeField] float sensitivity = 0.2f;

    void Update()
    {

        Vector3 playerForwardDirection = player.forward;

        transform.position = player.position -4 * playerForwardDirection;
        transform.position += new Vector3(0, 4f, 0);

        transform.LookAt(WatchAt);

    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }


    void LateUpdate()
    {

        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        cameraHeight += mouseY;
        if (cameraHeight >= 10)
            cameraHeight = 10f;
        if (cameraHeight <= 0.5)
            cameraHeight = 0.5f;


        Vector3 face = player.forward;

        transform.position = player.position + new Vector3((face.x * -cameraDistance), cameraHeight, (face.z * -cameraDistance));

        transform.LookAt(player);
    }
}
