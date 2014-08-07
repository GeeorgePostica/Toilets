using UnityEngine;
using System.Collections;

public static class ExtensionMethods {
	
	/**----------    FLOAT    ----------------**/
	
	public static string ConvertToTimeFormat(this float f){
		/** TODO: Very basic but not efficient algorithm...
		 * Leave it here if on mobile devices TimeSpan doesn't work */
		string final;
		int min, sec;

			if(f > 60){
				sec = Mathf.FloorToInt(f % 60);
				min = Mathf.FloorToInt(f / 60);
				final = (min < 10 ? "0" + min : min.ToString()) +
					(sec < 10 ? ":0" + sec : ":" + sec);
			}
			else {
				int millis = Mathf.FloorToInt(f * 100) % 100;
				sec = Mathf.FloorToInt(f % 60);
				final = (sec < 10 ? "0" + sec : sec.ToString()) + 
					(millis < 10 ? ":0" + millis : ":" + millis);
			}

		return final;

		/*
		string time = null;
		try{
			time = System.TimeSpan.FromSeconds(f).ToString().Substring(6, 5);
		}
		catch (System.ArgumentOutOfRangeException e){
			time = "00:00";
		}
		return time;
		*/
	}

	public static string ConvertToFullTimeFormat(this float f){
		int sec = (int)(f % 60);
		int millis = (int)(f * 100) % 100;

		string final = (sec < 10 ? "0" + sec : sec.ToString()) + 
						(millis < 10 ? ":0" + millis : ":" + millis);

		if(f > 60){
			sec = (int)(f / 60);
			final = sec + ":" + final;
		}

		return final;
	}

	public static string ConvertToSecFormat(this float f){
		char[] time = new char[2];
		int sec = Mathf.FloorToInt(f % 60);
		
		time[0] += (char)(sec / 10 + 48);
		time[1] += (char)(sec % 10 + 48);
		
		return new string(time);
	}
	
	public static int ToSec(this float f){
		return Mathf.FloorToInt(f % 60);
	}
	
	/**----------------------------------------------**/
	

}
