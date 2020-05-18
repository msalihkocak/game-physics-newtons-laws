using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsController : MonoBehaviour {
  [SerializeField] private float mass; // [kg]
  [SerializeField] private Vector3 velocity; // [m/s]
  [SerializeField] private Vector3 netForce; // [m/s]
  [SerializeField] private bool showForceLines = true;

  private List<Vector3> _forces;

  private LineRenderer _lineRenderer;

  public float Mass {
    get => mass;
    set => mass = value;
  }

  public Vector3 Velocity {
    get => velocity;
    set => velocity = value;
  }

  private void Awake() {
    _forces = new List<Vector3>();
    SetLineRenderDefaultParams(Color.yellow, 0.2f);
  }

  private void FixedUpdate() {
    RenderLinesIfEnabled();
    UpdatePosition();
  }

  public void AddForce(Vector3 force) {
    _forces.Add(force);
  }

  private Vector3 CalculateNetForce() {
    var netForce = _forces.Aggregate(Vector3.zero, (total, f) => total + f);
    _forces.Clear();
    return netForce;
  }

  private void UpdatePosition() {
    netForce = CalculateNetForce();
    var acceleration = netForce / mass;
    velocity += acceleration * Time.deltaTime;
    transform.position += velocity * Time.deltaTime;
  }

  private void RenderLinesIfEnabled() {
    if (showForceLines) {
      _lineRenderer.enabled = true;
      _lineRenderer.positionCount = _forces.Count * 2;
      int i = 0;
      foreach (var forceVector in _forces) {
        _lineRenderer.SetPosition(i, Vector3.zero);
        _lineRenderer.SetPosition(i + 1, -forceVector);
        i = i + 2;
      }
    }
    else _lineRenderer.enabled = false;
  }

  private void SetLineRenderDefaultParams(Color color, float width) {
    _lineRenderer = gameObject.AddComponent<LineRenderer>();
    _lineRenderer.material = new Material(Shader.Find("Unlit/Color")) {color = color};
    _lineRenderer.startWidth = width;
    _lineRenderer.endWidth = width;
    _lineRenderer.useWorldSpace = false;
  }
}