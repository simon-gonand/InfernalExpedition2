using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovements : MonoBehaviour
{
    [SerializeField]
    private float steerSpeed;
    [SerializeField]
    [Header("Stats")]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;

    [Header("References")]
    public Rigidbody selfRigidBody;
    public Transform self;

    private float currentSpeed;
    private float currentRotation;

    public static BoatMovements instance;

    private void Awake()
    {
        instance = this;

        currentSpeed = maxSpeed / 2 - minSpeed / 2;
    }

    public void SetVelocity(float speed)
    {
        if (speed < 0 && currentSpeed > minSpeed || speed > 0 && currentSpeed < maxSpeed)
            currentSpeed += speed;
        selfRigidBody.velocity = self.forward * Time.deltaTime * currentSpeed;
    }

    public void Steer(float steering)
    {
        steering *= steerSpeed * Time.deltaTime;
        currentRotation = currentRotation + steering;
        currentRotation = Mathf.Clamp(currentRotation, -45.0f, 45.0f);
        self.rotation = Quaternion.Euler(new Vector3(0.0f, currentRotation, 0.0f));
    }
}
