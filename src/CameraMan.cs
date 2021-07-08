using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform alvo;
    float deltaZ, deltaX;
    float dist,distX;
    float vel, smooth;
    public float y=0;
    public float maxDistance;
    public float z;
    public float x;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(alvo != null)
        {
            deltaZ = 0;
            dist = alvo.position.z - transform.position.z;

            if(dist > maxDistance * 0.6f)
                deltaZ = dist - maxDistance * 0.6f;
            else if(dist < -(maxDistance * 0.6f))
                deltaZ = dist -(-maxDistance * 0.6f);

            float nz = Mathf.SmoothDamp(transform.position.z, transform.position.z + deltaZ, ref vel, smooth);

            deltaX = 0;
            distX = alvo.position.x - transform.position.x;

            if( distX > maxDistance )
                deltaX = distX - maxDistance ;
            else if( distX < -(maxDistance) )
                deltaX = distX -(-maxDistance );

            float nx = Mathf.SmoothDamp(transform.position.x, transform.position.x + deltaX, ref vel, smooth);
            // float mov = x - transform.position.x;

            transform.position = new Vector3(nx,alvo.position.y+y,nz);
        }
    }
}
