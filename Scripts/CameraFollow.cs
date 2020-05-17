using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private Transform ballTransform;

    private readonly Vector3 _cameraOffset = new Vector3(5,3,-10);
    private const float SmoothSpeed = 0.8f;
    private const float LookForwardOffset = 5;

    private void FixedUpdate() {
        var ballPosition = ballTransform.position;
        var desiredPosition = ballPosition + _cameraOffset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime * 10f);
        transform.position = smoothedPosition;
        
        transform.LookAt(ballPosition + Vector3.forward * LookForwardOffset);
    }
}
