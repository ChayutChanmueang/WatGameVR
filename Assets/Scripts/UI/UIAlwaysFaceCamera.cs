using UnityEngine;

public class UIAlwaysFaceCamera : MonoBehaviour
{
    private Transform MainCamTransform;

    private void Start()
    {
        MainCamTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + MainCamTransform.rotation * Vector3.forward , 
        MainCamTransform.rotation * Vector3.up);
    }
}
