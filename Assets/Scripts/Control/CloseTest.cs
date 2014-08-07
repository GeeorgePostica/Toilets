using UnityEngine;
using System.Collections;

public class CloseTest : MonoBehaviour {
	TextMesh points;
	public float openForce;
	private static int pts;
	private Overlord controller;
	// Use this for initialization
	void Start () {
		points = GameObject.Find("Points").GetComponent<TextMesh>();
		controller = GameObject.Find("GameController").GetComponent<Overlord>();
		StartCoroutine(ResetPoints());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log("Collision Entered");
		if(other.gameObject.tag == "Door"){
			pts++;
			if(pts % controller.bonusPenalty == 0){
				controller.numberOfPenalties++;
			}
			points.text = pts.ToString();
			other.rigidbody.velocity = Vector3.zero;
			other.rigidbody.angularVelocity = Vector3.zero;
			TestForce tforce = other.gameObject.GetComponent<TestForce>();
			if(tforce != null){
				tforce.GenerateOpenForce();
			}
		}
	}

	void OnTriggerStay(Collider other){
		other.rigidbody.AddForce(Vector3.forward * -openForce);
	}

	IEnumerator ResetPoints(){
		yield return new WaitForSeconds(Time.fixedDeltaTime);
		pts = 0;
		points.text = "0";
		yield return null;
	}
}
