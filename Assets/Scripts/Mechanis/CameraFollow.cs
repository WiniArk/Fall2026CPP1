using System.Security.Claims;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public enum CameraState
    {
        None, 
        Follow, 
        Bound, 
        Transition
    }

    public CameraState currentState;
    public float minXPos;
    public float maxXPos;
    public Transform target;
    public float cameraSpeed = 5;

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("No Target assigned as player. Please include valid reference in the camera follow script");
                return;
            }
            target = player.transform;
        } 
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        
        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(target.position.x , minXPos , maxXPos) ;

        transform.position = Vector3.MoveTowards(transform.position, currentPos, cameraSpeed * Time.deltaTime);

        
    }
}