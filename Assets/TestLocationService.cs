using UnityEngine;
using System.Collections;

public class TestLocationService : MonoBehaviour
{
	private string locationStr = "hogehoge";
	private Vector2 currentLatLng;
	private Vector3 compassVector;
	private string compassStr;
	private Vector3 velocity;
	private GUIStyle style;

	private Vector2[] CHECKPOINTS;
	void Start()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser){
			this.locationStr= "enable location service!!!";
			return;
		}

		CHECKPOINTS[0] = new Vector2(35.665981f, 139.703532f);

		style = new GUIStyle();
		style.fontSize = 100;

		// Start service before querying location
		Input.location.Start();
		Input.compass.enabled = true;

		InvokeRepeating("getLocation", 2, 3);
		InvokeRepeating("getCompass", 2, 0.3F);

	}

	void OnGUI ()
	{


		Rect rect = new Rect(0, 0, 600, 100);
		GUI.Label(rect, locationStr, style);

		rect = new Rect(0, 100, 600, 100);
		GUI.Label(rect, compassStr, style);

		rect = new Rect(0, 500, 600, 100);
		Vector2 diff = CHECKPOINTS[0] - currentLatLng;
		GUI.Label(rect, diff.x.ToString() + " " + diff.y.ToString(), style);

	}

	void getLocation()
	{

		if(Input.location.status == LocationServiceStatus.Initializing)
		{
			return;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			return;
		}
		else
		{
			// Access granted and location value could be retrieved
			this.locationStr = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
			//			print(this.Star);
			currentLatLng = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
		}

		// Stop service if there is no need to query location updates continuously
//		Input.location.Stop();
	}

	void getCompass()
	{
		this.compassStr = Input.compass.trueHeading.ToString();
	}
}