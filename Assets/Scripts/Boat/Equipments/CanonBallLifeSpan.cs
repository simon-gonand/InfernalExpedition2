using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallLifeSpan : MonoBehaviour
{
    [SerializeField]
    private Transform self;

    private Transform canonWhichFired;

    private void IsUnderWater()
    {
        if (self.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetCanonWhichFired(Transform canon)
    {
        canonWhichFired = canon;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.transform.position == canonWhichFired.position))
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsUnderWater();
    }
}
