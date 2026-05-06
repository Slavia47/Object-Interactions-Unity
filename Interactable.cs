using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float forceAmount = 5f; 
    private float holdingDistance = 2f; 
    public Transform playerCamera;
    private Rigidbody rb;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    public void Interact(Vector3 hitPoint, Vector3 hitNormal, Vector3 pickplace, string actionState)
    {

        if (actionState == "Pushing")
        {
            rb.AddForce(-hitNormal * forceAmount, ForceMode.Impulse);
        }
        else if (actionState == "Pulling")
        {
            rb.AddForce(hitNormal * (forceAmount * 5), ForceMode.Force);
        }
        else if (actionState == "Holding")
        {
            rb.isKinematic = true;
            if (playerCamera != null)
            {
                Vector3 lookDirection = playerCamera.forward;
                Vector3 desiredPosition = playerCamera.position + lookDirection * holdingDistance;
                transform.position = desiredPosition;
            }
        rb.isKinematic = false;
        }
    }
}
