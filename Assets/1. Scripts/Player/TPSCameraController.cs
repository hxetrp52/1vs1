using Cinemachine;
using UnityEngine;

public class TPSCameraController : MonoBehaviour
{
    public Transform target; // 플레이어
    public Vector3 offset = new Vector3(0f, 30f, -15f);
    public float mouseSensitivity = 2f;
    public float zoomSpeed = 5f;
    public float minZoom = -5f;
    public float maxZoom = -30f;

    private float yaw;
    private float pitch;
    private CinemachineVirtualCamera vCam;

    private void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    void LateUpdate()
    {
        // 마우스 입력으로 회전
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -50f, 30f); // 위아래 제한

        // 마우스 휠 줌
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (!Input.GetKey(KeyCode.LeftControl)) 
        {
            offset.z += scroll * zoomSpeed;
        }
        offset.z = Mathf.Clamp(offset.z, maxZoom, minZoom); 
        if (Input.GetKey(KeyCode.LeftControl)) vCam.m_Lens.FieldOfView -= scroll * zoomSpeed * 2;
        vCam.m_Lens.FieldOfView = Mathf.Clamp(vCam.m_Lens.FieldOfView, 10, 60);
        if (Input.GetKeyDown(KeyCode.L)) vCam.m_Lens.FieldOfView = 60;

        // 카메라 위치 및 회전 적용
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f); // 캐릭터 위쪽을 바라보게
    }
}
