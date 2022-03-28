using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    CinemachineVirtualCamera thirdPerson;

    [SerializeField]
    CinemachineVirtualCamera pov;

    [SerializeField]
    Text text;

    CinemachinePOV povComponent;

    bool isPOV;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb.inertiaTensor = Vector3.one;
        povComponent = pov.GetCinemachineComponent<CinemachinePOV>();
    }

    void FixedUpdate()
    {
        if (isPOV)
        {
            return;
        }

        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        rb.AddRelativeTorque(new Vector3(-y, x, 0) * 2, ForceMode.Impulse);
    }

    void Update()
    {
        text.text = $"X: {povComponent.m_HorizontalAxis.Value}\nY: {povComponent.m_VerticalAxis.Value}";

        if (Input.GetMouseButtonDown(0))
        {
            if (isPOV)
            {
                pov.Priority = 0;
                thirdPerson.Priority = 1;
            }
            else
            {
                pov.Priority = 1;
                thirdPerson.Priority = 0;
            }

            isPOV = !isPOV;
        }
    }
}
