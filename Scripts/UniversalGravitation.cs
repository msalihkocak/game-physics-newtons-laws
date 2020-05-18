using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour {
  
  private PhysicsController[] _physicsBodies;
  
  private const float GConstant = 6.673e-11f; // [m^3/kg*s^2]

  void Start() {
    ScanForPhysicsControllers();
    Launcher.OnCannonFired += ScanForPhysicsControllers;
  }

  void ScanForPhysicsControllers() {
    _physicsBodies = FindObjectsOfType<PhysicsController>();
  }

  private void FixedUpdate() {
    CalculateGravity();
  }

  private void CalculateGravity() {
    foreach (var physicsBodyA in _physicsBodies) {
      foreach (var physicsBodyB in _physicsBodies) {
        if (physicsBodyA == physicsBodyB) continue;
        var positionDifference = physicsBodyA.transform.position - physicsBodyB.transform.position;
        var gravityMagnitude = GConstant * physicsBodyA.Mass * physicsBodyB.Mass / positionDifference.sqrMagnitude;
        var gravityVector = gravityMagnitude * positionDifference.normalized;
        physicsBodyA.AddForce(-gravityVector);
      }
    }
  }
}