using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickFottball : MonoBehaviour {
  [SerializeField] private Vector3 forceToAdd;
  [SerializeField] private Vector3 torqueToAdd;

  private Rigidbody _rigidbody;

  void Start() {
    _rigidbody = GetComponent<Rigidbody>();
  }

  private void OnMouseDown() {
    _rigidbody.AddForce(forceToAdd);
    _rigidbody.AddTorque(torqueToAdd);
  }
}