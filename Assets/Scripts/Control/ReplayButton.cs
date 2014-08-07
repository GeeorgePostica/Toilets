using UnityEngine;
using System.Collections;

public class ReplayButton : InputHandler {
	
	public override bool HandleTouchDown(Vector2 touchPoint, int touchIndex)
	{
		Application.LoadLevel(Application.loadedLevel);
		return true;
	}

	public override bool HandleTouchUp(Vector2 touchPoint, int touchIndex)
	{
		return true;
	}

	public override bool HandleTouchMove(Vector2 touchPoint, Vector2 speed, int touchIndex)
	{
		return true;
	}
}
