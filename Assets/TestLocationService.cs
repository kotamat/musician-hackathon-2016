using UnityEngine;
using System.Collections;

public class TestLocationService : MonoBehaviour
{
	private string locationStr = "hogehoge";
	IEnumerator Start()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser){
			this.locationStr= "enable location service!!!";
			//			print("enable location service!!");
			yield break;
		}

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			//			print("Timed out");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			//			print("Unable to determine device location");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			this.locationStr = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
			//			print(this.Star);
		}

		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}

	void OnGUI ()
	{

		GUILayout.BeginArea (new Rect(0,0,200,200));
		GUILayout.Box (this.locationStr);
		GUILayout.EndArea ();
	}
		
}