using UnityEngine;
using System.Collections;

public class MoveDoorInputCapture : InputHandler {
	public Rigidbody door;
	public float speedMultiplier = 3;

	private TestForce tforce;
	
	// Use this for initialization
	void Start () {
		tforce = door.gameObject.GetComponent<TestForce>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override bool HandleTouchDown(Vector2 touchPoint, int touchIndex)
	{
		return true;
	}
	
	public override bool HandleTouchUp(Vector2 touchPoint, int touchIndex)
	{
		//tforce.GenerateOpenForce();
		return true;
	}

	public override bool HandleTouchMove(Vector2 touchPoint, Vector2 speed, int touchIndex)
	{
		//Debug.Log("Received speed: " + speed.magnitude);
		Vector3 forceToApply = Vector3.zero;
		door.AddForce(Vector3.right * speed.x * speedMultiplier + Vector3.up * speed.y * speedMultiplier);
		return true;
	}
	
}
