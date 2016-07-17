using UnityEngine;
using System.Collections;

public class TestLocationService : MonoBehaviour
{
	private string locationStr = "hogehoge";
	private Vector2 currentLatLng;
	private Vector3 compassVector;
	private string compassStr = "";
	private Vector3 velocity;
	private GUIStyle style;

	public Vector2[] CHECKPOINTS;
	public int currentCheckpointIndex = 0;

	[SerializeField]
	public float rangeCheckpoint = 0.000001f;
	void Start()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser){
			this.locationStr= "enable location service!!!";
			return;
		}

		// Start service before querying location
		Input.location.Start(1f,1f);
		Input.compass.enabled = true;

		InvokeRepeating("getLocation", 2, 1);
		InvokeRepeating("getCompass", 2, 0.3F);

	}

	void OnGUI ()
	{
		CHECKPOINTS = new Vector2[2];
		CHECKPOINTS[0] = new Vector2(35.664199f, 139.702397f);
		CHECKPOINTS[1] = new Vector2(35.665981f, 139.703532f);

		this.style = new GUIStyle();
		this.style.fontSize = 100;
		GUI.Label(new Rect(0, 0, 600, 100), this.locationStr, this.style);

		GUI.Label(new Rect(0, 100, 600, 100), "compass " + this.compassStr, this.style);


		Vector2 diff = CHECKPOINTS[currentCheckpointIndex] - this.currentLatLng;
		GUI.Label(new Rect(0, 200, 600, 100), "diff for checkpoint " + diff.x.ToString() + " " + diff.y.ToString(), this.style);

		GUI.Label(new Rect(0, 400, 600, 100), "diff magnitude " + diff.magnitude.ToString(), this.style);
		// check near checkpoint
		if(diff.magnitude < rangeCheckpoint){
			// TODO: alert checkpoint
			GUI.Label(new Rect(0, 400, 600, 100), diff.magnitude.ToString(), this.style);
		}

		// calculate direction
		float lat2 = CHECKPOINTS[0].x;
		float lng2 = CHECKPOINTS[0].y;
		float lat1 = this.currentLatLng.x;
		float lng1 = this.currentLatLng.y;
		var Y = Mathf.Cos(lng2 * Mathf.PI / 180) * Mathf.Sin(lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180);
		var X = Mathf.Cos(lng1 * Mathf.PI / 180) * Mathf.Sin(lng2 * Mathf.PI / 180) - Mathf.Sin(lng1 * Mathf.PI / 180) * Mathf.Cos(lng2 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180);
		var dirE0 = 180 * Mathf.Atan2(Y, X) / Mathf.PI; // 東向きが０度の方向
		if (dirE0 < 0) {
			dirE0 = dirE0 + 360; //0～360 にする。
		}
		var dirN0 = (dirE0 + 90) % 360; //(dirE0+90)÷360の余りを出力 北向きが０度の方向
		var directionForCheckpoint = (dirN0 - Input.compass.trueHeading + 360) % 360; 
		transform.rotation = Quaternion.Euler(0, 180 + directionForCheckpoint, 0);
		GUI.Label(new Rect(0, 300, 600, 100), "direction for checkpoint " + directionForCheckpoint.ToString(), this.style);
	}

	void getLocation()
	{

		if(Input.location.status == LocationServiceStatus.Initializing)
		{
			this.locationStr = "Location: is initializing";
			return;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			this.locationStr = "Location: is Failed";
			return;
		}
		else
		{
			// Access granted and location value could be retrieved
			this.locationStr = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
			//			print(this.Star);
			this.currentLatLng = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
		}



		// Stop service if there is no need to query location updates continuously
//		Input.location.Stop();
	}

	void getCompass()
	{
		this.compassStr = Input.compass.trueHeading.ToString();
	}
}