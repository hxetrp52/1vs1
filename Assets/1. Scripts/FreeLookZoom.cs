using UnityEngine;
using Cinemachine;

public class FreeLookZoom : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    private float currentZoom;

    void Start()
    {
        currentZoom = freeLookCam.m_Orbits[1].m_Radius;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            currentZoom -= scroll * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            UpdateZoom(currentZoom);
        }
    }

    void UpdateZoom(float zoom)
    {
        for (int i = 0; i < freeLookCam.m_Orbits.Length; i++)
        {
            freeLookCam.m_Orbits[i].m_Radius = zoom;
        }
    }
}
