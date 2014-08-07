using UnityEngine;
using System.Collections;

public abstract class InputHandler : MonoBehaviour {
	
	public virtual bool HandleTouchDown(Vector2 touchPoint, int touchIndex){
		return false;
	}
	
	public virtual bool HandleTouchUp(Vector2 touchPoint, int touchIndex){
		return false;
	}
	
	public virtual bool HandleTouchMove(Vector2 touchPoint, Vector2 speed, int touchIndex){
		return false;
	}
	
	//Changed by children if needed
	public bool HandleComplexTouch(int touchIndex, Vector2 touchPoint){
		return false;
	}
}