using UnityEngine;
using System.Collections;

public class Overlord : MonoBehaviour {
	public int numberOfPenalties = 1;
	public int bonusPenalty = 50;
	public GameObject fader;
	public GameObject replayButton;
	public TextMesh timeGUIFinal;

	private TextMesh timeGUI;
	private float time;
	private bool timeRun;
	// Use this for initialization
	void Start () {
		timeGUI = GameObject.Find("Time").GetComponent<TextMesh>();
		time = Time.time;
		timeRun = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeRun){
			timeGUI.text = (Time.time - time).ConvertToTimeFormat();
		}
	}

	public void AddPenalty(){
		numberOfPenalties--;
		if(numberOfPenalties <= 0){
			StartCoroutine(EnableFinish(1.0f));
			timeRun = false;
			fader.SetActive(true);
			timeGUI.text = "points";
			timeGUIFinal.text = (Time.time - time).ConvertToFullTimeFormat();
			timeGUIFinal.gameObject.SetActive(true);
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("TouchSensor")){
				obj.SetActive(false);
			}
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Door")){
				//obj.rigidbody.velocity = Vector3.zero;
				//obj.rigidbody.angularVelocity = Vector3.zero;
				obj.collider.enabled = false;

			}
		}
	}

	IEnumerator EnableFinish(float time){
		yield return new WaitForSeconds(time);
		replayButton.SetActive(true);
		yield return null;
	}
}
