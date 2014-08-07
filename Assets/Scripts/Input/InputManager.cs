using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour {
	public Camera mainCamera;
	public Camera secondaryCamera;
	public int maxNumberOfTouches = 5;
	private Vector2[] lastMousePosition;
	private float[] lastMouseTime;
	private bool[] moveEnabled;
	private InputHandler[] lastHandler = null;
	//public Coordinator coordinator;
	
	// Use this for initialization
	void Awake () {
		//Game.InputManager = this;
		lastMousePosition = new Vector2[maxNumberOfTouches];
		lastMouseTime = new float[maxNumberOfTouches];
		moveEnabled = new bool[maxNumberOfTouches];
		lastHandler = new InputHandler[maxNumberOfTouches];
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		//Debug.Log("InputHandler: " + lastHandler[0]);
		if(Input.GetMouseButtonDown(0)){
			//Debug.Log("ClickDown performed");
			lastMouseTime[0] = Time.time;
			moveEnabled[0] = true;
			TouchObject(0, Input.mousePosition, TouchDown);
		}
		else if(Input.GetMouseButtonUp(0)){
			if(lastHandler[0] == null){
				TouchObject(0, Input.mousePosition, TouchUp);
			}
			else{
				RaycastHit hit;
				
				//Some checks... consider using a try-catch block
				if(Physics.Raycast(secondaryCamera.ScreenPointToRay(Input.mousePosition), out hit)){
					TouchUp(lastHandler[0], hit.point, 0);
				}
			}
			lastHandler[0] = null;
		}
		else if(Input.GetMouseButton(0)){
			//Debug.Log("ClickNormal performed");
			if(lastHandler[0] == null){
				TouchObject(0, Input.mousePosition, TouchNormal);
			}
			else{
				RaycastHit hit;
				
				//Some checks... consider using a try-catch block
				if(Physics.Raycast(secondaryCamera.ScreenPointToRay(Input.mousePosition), out hit)){
					TouchNormal(lastHandler[0], hit.point, 0);
				}
			}
		}
		
		#else
		if (Input.touchCount > 0) {
			for(int i = 0; i < Input.touchCount; i++){
				if(Input.GetTouch(i).phase == TouchPhase.Ended){
					if(lastHandler[Input.GetTouch(i).fingerId] == null){
						TouchObject(Input.GetTouch(i).fingerId, Input.mousePosition, TouchUp);
					}
					else{
						RaycastHit hit;
						
						//Some checks... consider using a try-catch block
						if(Physics.Raycast(secondaryCamera.ScreenPointToRay(Input.mousePosition), out hit)){
							TouchUp(lastHandler[Input.GetTouch(i).fingerId, hit.point, Input.GetTouch(i).fingerId);
						}
					}
					lastHandler[Input.GetTouch(i).fingerId] = null;
				}
				else if(Input.GetTouch(i).phase == TouchPhase.Began){
					lastMouseTime[Input.GetTouch(i).fingerId] = Time.time;
					moveEnabled[Input.GetTouch(i).fingerId] = true;
					TouchObject(Input.GetTouch(i).fingerId, Input.GetTouch(i).position, TouchDown);
				}
				else if(Input.GetTouch(i).phase == TouchPhase.Moved){
					//TouchObject(i, Input.GetTouch(i).position, TouchNormal);
					if(lastHandler[Input.GetTouch(i).fingerId] == null){
						TouchObject(Input.GetTouch(i).fingerId, Input.GetTouch(i).position, TouchNormal);
					}
					else{
						RaycastHit hit;
						
						//Some checks... consider using a try-catch block
						if(Physics.Raycast(secondaryCamera.ScreenPointToRay(Input.GetTouch(i).position), 
						                   out hit)){
							TouchNormal(lastHandler[Input.GetTouch(i).fingerId], 
							            hit.point, Input.GetTouch(i).fingerId);
						}
					}
				}
			}
		}
		#endif
		if(Input.GetKeyDown(KeyCode.Escape)){	//This will be the BACK key on windows phone
			//coordinator.ManageEscKey();
			if(Application.loadedLevelName != "levelSelect"){
				Application.LoadLevel("levelSelect");
			}
			else{
				Application.Quit();
			}
		}
		
	}
	
	GameObject TouchedObject(Vector3 input){
		return Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(input)).collider.gameObject;
	}
	
	void TouchObject(int touchIndex, Vector3 input, Action<InputHandler, Vector2, int> touchAction){
		if(!TouchCameraObject(touchIndex, input, mainCamera, touchAction)){
			TouchCameraObject(touchIndex, input, secondaryCamera, touchAction);
		}
	}
	
	bool TouchCameraObject(int touchIndex, Vector3 input, Camera touchCam, Action<InputHandler, Vector2, int> touchAction){
		//RaycastHit2D hit = Physics2D.GetRayIntersection(touchCam.ScreenPointToRay(input));
		RaycastHit hit;

		//Some checks... consider using a try-catch block
		if(Physics.Raycast(touchCam.ScreenPointToRay(input), out hit)){
			//Debug.Log("Input handler found");

			InputHandler inputHandler = hit.collider.gameObject.GetComponent<InputHandler>();
			
			if(inputHandler != null){
				//Debug.Log("Touch vector: " + hit.point);
				if(touchAction == TouchDown){
					lastMousePosition[touchIndex] = hit.point;
				}
				if(!inputHandler.HandleComplexTouch(touchIndex, hit.point)){
					touchAction(inputHandler, hit.point, touchIndex);
					return true;
				}
			}
		}
		
		return false;
	}
	
	void TouchDown(InputHandler handler, Vector2 point, int touchIndex){
		handler.HandleTouchDown(point, touchIndex);
	}
	
	void TouchNormal(InputHandler handler, Vector2 point, int touchIndex){
		//Debug.Log("Last Time registered = " + lastMouseTime);
		if(moveEnabled[touchIndex] && lastHandler[touchIndex] != handler){
			moveEnabled[touchIndex] = false;
			lastHandler[touchIndex] = handler;
		}
		if(lastHandler[touchIndex] == handler){
			Vector2 mouseSpeed = (point - lastMousePosition[touchIndex])/(Time.time - lastMouseTime[touchIndex]);
			//Debug.Log("Time difference = " + (Time.time - lastMouseTime));
			lastMousePosition[touchIndex] = point;
			lastMouseTime[touchIndex] = Time.time;
			handler.HandleTouchMove(point, mouseSpeed, touchIndex);
		}
	}
	
	void TouchUp(InputHandler handler, Vector2 point, int touchIndex){
		handler.HandleTouchUp(point, touchIndex);
	}
}

