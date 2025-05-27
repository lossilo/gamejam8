using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector2 playerCenterOffset;
    [SerializeField] float centeringTime;
    [SerializeField] float distanceToStopMovingAt;
    [SerializeField] float decelerationTime;

    [Header("Camera Limits")]
    [SerializeField] Vector2 lowerCameraLimit;
    [SerializeField] Vector2 upperCameraLimit;
    
    float currentMaxMoveSpeed;
    float lastMaxMoveSpeed;

    private void Update()
    {
        Vector2 playerCameraOffset = (Vector2)transform.position - ((Vector2)playerTransform.position + playerCenterOffset);
        float playerCameraDistance = playerCameraOffset.magnitude;
        

        if (playerCameraDistance > distanceToStopMovingAt)
        {
            currentMaxMoveSpeed = playerCameraDistance / centeringTime;
            lastMaxMoveSpeed = currentMaxMoveSpeed;
        }
        else
        {
            currentMaxMoveSpeed -= (lastMaxMoveSpeed / decelerationTime) * Time.deltaTime;
            currentMaxMoveSpeed = Mathf.Clamp(currentMaxMoveSpeed, 0, lastMaxMoveSpeed);
        }

        Vector2 nextMovePosition = transform.position + currentMaxMoveSpeed * Time.deltaTime * (playerTransform.position + (Vector3)playerCenterOffset - transform.position);
        float clampedX = Mathf.Clamp(nextMovePosition.x, lowerCameraLimit.x, upperCameraLimit.x);
        float clampedY = Mathf.Clamp(nextMovePosition.y, lowerCameraLimit.y, upperCameraLimit.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Vector2 cornerBL = lowerCameraLimit;
        Vector2 cornerBR = new Vector2(upperCameraLimit.x, lowerCameraLimit.y);
        Vector2 cornerUL = new Vector2(lowerCameraLimit.x, upperCameraLimit.y);
        Vector2 cornerUR = upperCameraLimit;

        float extraSpaceY = GetComponent<Camera>().orthographicSize;
        float extraSpaceX = extraSpaceY * (16f / 9f);

        Vector2 outerCornerBL = new Vector2(cornerBL.x - extraSpaceX, cornerBL.y - extraSpaceY);
        Vector2 outerCornerBR = new Vector2(cornerBR.x + extraSpaceX, cornerBR.y - extraSpaceY);
        Vector2 outerCornerUL = new Vector2(cornerUL.x - extraSpaceX, cornerUL.y + extraSpaceY);
        Vector2 outerCornerUR = new Vector2(cornerUR.x + extraSpaceX, cornerUR.y + extraSpaceY);

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(cornerBL, cornerBR);
        Gizmos.DrawLine(cornerBL, cornerUL);
        Gizmos.DrawLine(cornerUL, cornerUR);
        Gizmos.DrawLine(cornerBR, cornerUR);
        Gizmos.DrawLine(cornerBL, cornerUR);
        Gizmos.DrawLine(cornerUL, cornerBR);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(outerCornerBL, outerCornerBR);
        Gizmos.DrawLine(outerCornerUL, outerCornerUR);
        Gizmos.DrawLine(outerCornerBL, outerCornerUL);
        Gizmos.DrawLine(outerCornerBR, outerCornerUR);
    }
}
