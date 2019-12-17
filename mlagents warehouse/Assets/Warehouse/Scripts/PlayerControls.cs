using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody forkliftRB;

    private void Start()
    {
        forkliftRB = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            forkliftRB.AddRelativeForce(new Vector3(85.0f, 0.0f, 0.0f), ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forkliftRB.AddRelativeForce(new Vector3(-85.0f, 0.0f, 0.0f), ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, -2.5f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, 2.5f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // this.GetComponent<WarehouseAgent>().ApplyForceToHeldObject();
        }
    }
}