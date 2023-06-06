using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothing = 0.2f;
    private void FixedUpdate()
    {
        if(player.position.x >= 0)
        {
            Vector3 targetPos = new Vector3(player.position.x, this.transform.position.y, this.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}
