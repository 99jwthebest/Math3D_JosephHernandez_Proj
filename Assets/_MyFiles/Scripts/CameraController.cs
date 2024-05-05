using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject projectileObject;
    [SerializeField] float cameraSpeed;
    [SerializeField] float cameraLookAtSpeed;
    [SerializeField] Vector3 startCamLocation;
    [SerializeField] Quaternion startCamRotation;





    private void Awake()
    {
        startCamLocation = transform.position;
        startCamRotation = transform.rotation;
    }

    void Start()
    {
        
    }
    private void LateUpdate()
    {
        FollowProjectile();
        
    }


    void FollowProjectile()
    {
        if (projectileObject != null)
        {
            transform.position = Vector3.Lerp(transform.position, projectileObject.transform.position - new Vector3(-4f, 0, -4f), cameraSpeed * Time.deltaTime);
            Quaternion OriginalRot = transform.rotation;
            transform.LookAt(projectileObject.transform.position);
            Quaternion newRot = transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * cameraLookAtSpeed);
        }
    }

    public void FindProjectileObject()
    {
        if (projectileObject != null)
        {
            return;
        }
        if (projectileObject == null)
        {
            projectileObject = FindObjectOfType<Projectile>().gameObject;
        }
    }
    public void SetProjectile(GameObject projectile)
    {
        projectileObject = projectile;
    }

    public Vector3 GetStartPosition()
    {
        return startCamLocation;
    }

    public Quaternion GetStartRotation()
    {
        return startCamRotation;
    }
}
