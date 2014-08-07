using UnityEngine;
using System.Collections;

public class TestForce : MonoBehaviour {
	public float power = 20.0f;
	public float maxPower = 80.0f;

	public float minAddedPower = -0.25f;
	public float maxAddedPower = 3.0f;
	public float minWaitTime = 0.5f;
	public float maxWaitTime = 3.0f;
	// Use this for initialization
	void Start () {
		rigidbody.AddForce(transform.forward * -power);
		minAddedPower *= Mathf.Sign(power);
		maxAddedPower *= Mathf.Sign(power);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateOpenForce(){
		power += Mathf.Min(Random.Range(minAddedPower, maxAddedPower), maxPower);
		StartCoroutine(DelayedForce(Random.Range(minWaitTime, maxWaitTime)));
		//maxWaitTime = Mathf.Lerp(maxWaitTime, minWaitTime, Random.Range(0.0f, 0.25f));
	}

	IEnumerator DelayedForce(float time){
		yield return new WaitForSeconds(time);
		/*rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;*/
		rigidbody.AddForce(transform.forward * -power);
		yield return null;
	}
}
