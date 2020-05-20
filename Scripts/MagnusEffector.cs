using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnusEffector : MonoBehaviour {
  [SerializeField] private float magnusConstant;
  
  private Rigidbody _rigidBody;

  void Start() {
    _rigidBody = GetComponent<Rigidbody>();
  }

  private void FixedUpdate() {
    _rigidBody.AddForce(magnusConstant * Time.deltaTime * Vector3.Cross(_rigidBody.angularVelocity,
                                                      _rigidBody.velocity));
  }
}