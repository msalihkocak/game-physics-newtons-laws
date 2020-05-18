using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsController))]
public class FluidDrag : MonoBehaviour {
  [Range(1f, 2f)] [SerializeField] private float velocityExponent;
  [SerializeField] private float dragConstant;

  private PhysicsController _physicsController;

  private void Start() {
    _physicsController = GetComponent<PhysicsController>();
  }

  private void FixedUpdate() {
    var velocityVector = _physicsController.Velocity;
    var speed = velocityVector.magnitude;
    var dragVector = CalculateDrag(speed) * -velocityVector.normalized;
    _physicsController.AddForce(dragVector);
  }

  private float CalculateDrag(float speed) {
    return dragConstant * Mathf.Pow(speed, velocityExponent);
  }
}