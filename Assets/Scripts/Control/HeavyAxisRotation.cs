using UnityEngine;
using System.Collections;

public class HeavyAxisRotation : MonoBehaviour {
	public float rotationSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * rotationSpeed);
	}
}
