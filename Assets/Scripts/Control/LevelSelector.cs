using UnityEngine;
using System.Collections;

public class LevelSelector : InputHandler {

	public string level;

	public override bool HandleTouchDown(Vector2 touchPoint, int touchIndex)
	{
		Application.LoadLevel(level);
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
