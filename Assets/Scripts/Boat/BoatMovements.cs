using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovements : MonoBehaviour
{
    [Header("References")]
    public Rigidbody selfRigidBody;
    public Transform self;
    [SerializeField]
    private BoatPresets boatPreset;

    private float currentSpeed;
    private float currentRotation;


    // BoatMovements Class is a singleton
    public static BoatMovements instance;

    private void Awake()
    {
        instance = this;

        // Initiate a speed at the beginning of the game
        currentSpeed = boatPreset.maxSpeed - boatPreset.minSpeed;
    }

    // Set the velocity of the boat
    public void SetVelocity(float speed)
    {
        // Clamp speed
        currentSpeed += speed;
        currentSpeed = Mathf.Clamp(currentSpeed, boatPreset.minSpeed, boatPreset.maxSpeed);

        // Update velocity
        selfRigidBody.velocity = self.forward * Time.deltaTime * currentSpeed;
    }

    // Function that steer the boat
    public void Steer(float steering)
    {
        // Multiply by the steerSpeed of the boat
        steering *= boatPreset.steerSpeed * Time.deltaTime;

        // Clamp the rotation of the boat
        currentRotation = currentRotation + steering;
        currentRotation = Mathf.Clamp(currentRotation, -45.0f, 45.0f);

        // Update the rotation of the boat
        self.rotation = Quaternion.Euler(new Vector3(0.0f, currentRotation, 0.0f));
    }
}
