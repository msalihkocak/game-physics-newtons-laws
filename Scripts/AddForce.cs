using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallPhysics))]
public class AddForce : MonoBehaviour {
    public Vector3 force = new Vector3(0,0,0);

    private BallPhysics _ballPhysics;

    void Start() {
      _ballPhysics = GetComponent<BallPhysics>();
    }

    void FixedUpdate() {
      _ballPhysics.AddForce(force);
    }
}
