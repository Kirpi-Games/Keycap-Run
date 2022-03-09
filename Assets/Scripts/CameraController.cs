using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public GameObject target;
    public float smoothSpeed;
    public Vector3 offset;
    public Vector3 lookatOffset,finalOffset;
    public bool isFollow,isFinal;
    public float min, max;
    public GameObject newTarget;
    
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        isFollow = true;
        Instance = this;
    }
    
    public void Final()
    {
        offset = Vector3.Lerp(offset, finalOffset, 1f * Time.deltaTime);
    }
    
    public void CameraFollow()
    {
        if (target == null) return;
    
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.transform.position.x,0,target.transform.position.z) + offset;
            Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothed;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,min,max),transform.position.y,transform.position.z);
            Vector3 lookAtTarget = new Vector3(target.transform.position.x,0,target.transform.position.z) + lookatOffset;
            transform.LookAt(new Vector3(lookAtTarget.x,lookAtTarget.y,lookAtTarget.z));    
        }
    
    }
    
    private void LateUpdate()
    {
            
        if (target == null)
        {
            return;
        }
    
        if (isFollow)
        {
            CameraFollow();
        }

        if (isFinal)
        {
            Final();
        }
    }
}
