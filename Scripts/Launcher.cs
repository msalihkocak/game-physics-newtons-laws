using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
  [SerializeField] private GameObject ballPrefab;
  [SerializeField] private float throwingForceMagnitude = 0;
  [SerializeField] private AudioClip windupSound;
  [SerializeField] private AudioClip launchSound;

  private AudioSource _audioSource;
  private bool _isWindupSoundPlayed = false;

  public static event Action OnCannonFired = delegate {  };

  private void Start() {
    _audioSource = GetComponent<AudioSource>();
  }

  private void OnMouseDrag() {
    throwingForceMagnitude = Mathf.Lerp(throwingForceMagnitude, 3,Time.deltaTime * 3);
    _audioSource.clip = windupSound;
    if (!_isWindupSoundPlayed) _audioSource.Play();
    _isWindupSoundPlayed = true;
  }

  private void OnMouseUp() {
    _audioSource.clip = launchSound;
    _audioSource.Play();
    var instantiated = Instantiate(ballPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
    instantiated.GetComponent<PhysicsController>().AddForce(new Vector3(-1, 1, 0) * throwingForceMagnitude * 50000);
    OnCannonFired();
    throwingForceMagnitude = 0;
    _isWindupSoundPlayed = false;
  }
}