using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothing = 0.2f;
    public float offset;
    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x + offset, this.transform.position.y, this.transform.position.z);
        if (targetPos.x >= 0 && targetPos.x <= 89.1)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}
