using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 10.0f;  // Súbanle el Drag a su rigidbody
    public float RotationSpeed = 90.0f;
    public float JumpPower = 10.0f;
    public bool CanJump = true;
    int FloorLayer;
    int WallLayer;

    private Rigidbody _Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        if (_Rigidbody.drag < 0.5)
        {
            Debug.LogWarning("Cuidado, el drag es muy bajo, tu personaje se deslizará mucho.");
        }
        FloorLayer = LayerMask.NameToLayer("Floor");
        WallLayer = LayerMask.NameToLayer("Wall");


    }

    // Update is called once per frame
    void Update()
    {
        float MoveIntensity = Input.GetAxis("Vertical");
        _Rigidbody.AddForce(MoveIntensity * transform.forward * MovementSpeed, ForceMode.Acceleration);
        transform.Rotate(0, Input.GetAxis("Horizontal")* Time.deltaTime * RotationSpeed, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    void Jump()
    {
        if (CanJump)
        {
            _Rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
            CanJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // checamos que tenga alguna de las Layers de colisión que nos interesan (Floor o Wall)
        if (collision.collider.gameObject.layer == FloorLayer)
        {
            // Entonces estamos tocando el piso y podemos hacer que vuelva a brincar
            CanJump = true;
        }
        else if (collision.collider.gameObject.layer == WallLayer)
        {
            // Debug.Log("Collided with a wall");
            // Si es con una pared, hay que ver si la colisión fue por arriba. Si no, no deberíamos poder brincar.
            ContactPoint[] contactPoints = { };
            float angleSimilarity = Vector3.Dot(collision.impulse.normalized, Vector3.up);
            // si fue lo suficientemente por arriba
            if (angleSimilarity > 0.8f)
            {
                CanJump = true;
            }
        }
    }
}
