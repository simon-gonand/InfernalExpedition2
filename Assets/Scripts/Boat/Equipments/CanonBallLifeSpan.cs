using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define when a Canon ball must be destroy
public class CanonBallLifeSpan : MonoBehaviour
{
    [SerializeField]
    private Transform self;

    private Transform canonWhichFired;

    // If the canon ball is under the water then it must be destroy
    // (Replace -1.0f by the water mesh)
    private void IsUnderWater()
    {
        if (self.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }

    // Define which canon shot
    public void SetCanonWhichFired(Transform canon)
    {
        canonWhichFired = canon;
    }

    // If the canon ball is colliding with something then it must be destroy
    private void OnCollisionEnter(Collision collision)
    {
        // The canon cannot collide with the canon that shot it
        if (!(collision.transform.position == canonWhichFired.position))
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        IsUnderWater();
    }
}
