using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsController))]
public class RocketEngine : MonoBehaviour {
  
  [SerializeField] private float fuelMass; // [kg]
  [SerializeField] private float maxThrust; // kN [kg * m/s^2]
  [Range(0,1f)]
  [SerializeField] private float thrustPercent;
  [SerializeField] private Vector3 thrustUnitVector;

  private PhysicsController _physicsController;
  private float _currentThrust;

  void Start() {
    _physicsController = GetComponent<PhysicsController>();
    _physicsController.Mass += fuelMass;
  }

  void FixedUpdate() {
    var fuelToBeConsumed = CalculateFuelConsumption();
    if (fuelMass <= fuelToBeConsumed) return;
    
    DecreaseFuel(fuelToBeConsumed);
    ExertForce();
  }

  private void DecreaseFuel(float fuelToBeConsumed) {
    fuelMass -= fuelToBeConsumed;
    _physicsController.Mass -= fuelToBeConsumed;
  }

  private float CalculateFuelConsumption() {
    const float effectiveExhaustVelocity = 4462f;
    return _currentThrust * Time.deltaTime / effectiveExhaustVelocity;
  }

  private void ExertForce() {
    _currentThrust = thrustPercent * maxThrust * 1000f; // to return the value in Newtons we multiply by 1000
    var thrustVector = thrustUnitVector.normalized * _currentThrust;
    _physicsController.AddForce(thrustVector);
  }
}