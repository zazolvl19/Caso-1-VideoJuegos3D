using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] MouseSensitivity mouseSensitivity;
    [SerializeField] CameraAngle cameraAngle;

    CameraRotation cameraRotation;
    Vector2 input;

    float distanceToTarget;

    private void Awake()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);
    }

    private void Update()
    {
        HandleInputs();
        HandleRotation();
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(cameraRotation.getPitch(), cameraRotation.getYaw(), 0.0f);
        transform.position = target.position - transform.forward * distanceToTarget;
    }

    private void HandleInputs()
    {
        // Capturar el movimiento del rat�n en ambos ejes
        input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }

    private void HandleRotation()
    {
        float yaw = cameraRotation.getYaw();
        float pitch = cameraRotation.getPitch();

        // Rotar la c�mara horizontalmente
        yaw += input.x * mouseSensitivity.getHorizontal() * Time.deltaTime;

        // Rotar la c�mara verticalmente
        pitch -= input.y * mouseSensitivity.getVertical() * Time.deltaTime; // Invertido para que el movimiento vertical del rat�n corresponda a una rotaci�n vertical apropiada

        // Limitar el �ngulo de rotaci�n vertical
        pitch = Mathf.Clamp(pitch, cameraAngle.getMin(), cameraAngle.getMax());

        // Aplicar los cambios de rotaci�n
        cameraRotation.setYaw(yaw);
        cameraRotation.setPitch(pitch);
    }
}
