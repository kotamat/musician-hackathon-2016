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

	private Vector2[,] CHECKPOINTS;
	private int currentRoute = -1;
	private int currentCheckpointIndex = 0;

	[SerializeField]
	public float rangeCheckpoint = 0.0001f;
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
		CHECKPOINTS = new Vector2[2, 4];
		CHECKPOINTS[0, 0] = new Vector2(35.664082f, 139.702359f);
		//		CHECKPOINTS[0, 0] = new Vector2(35.663025f, 139.702308f);
		// 明治通り1
		CHECKPOINTS[0, 1] = new Vector2(35.663688f, 139.702274f);
		// メディアラボ前
		CHECKPOINTS[0, 2] = new Vector2(35.664126f, 139.702395f);

		// キャットストリート1
		CHECKPOINTS[1, 0] = new Vector2(35.664185f, 139.702649f);
		//		CHECKPOINTS[1, 0] = new Vector2(35.663005f, 139.702442f);
		// キャットストリート角
		CHECKPOINTS[1, 1] = new Vector2(35.664098f, 139.702827f);
		// メディアラボ角
		CHECKPOINTS[1, 2] = new Vector2(35.664200f, 139.702435f);

		// ゴール
		CHECKPOINTS[1, 3] = new Vector2(35.664118f, 139.702463f);
		CHECKPOINTS[0, 3] = new Vector2(35.664118f, 139.702463f);

		this.style = new GUIStyle();
		this.style.fontSize = 100;
//		GUI.Label(new Rect(0, 0, 600, 100), this.locationStr, this.style);
//
//		GUI.Label(new Rect(0, 100, 600, 100), "compass " + this.compassStr, this.style);

		//route ditecting
		if(currentRoute == -1){
			Vector2 diff0 = CHECKPOINTS[0, 0] - this.currentLatLng;
			Vector2 diff1 = CHECKPOINTS[1, 0] - this.currentLatLng;

			GUI.Label(new Rect(0, 0, 600, 100), "Waiting first checkpoint: 0:" + diff0.magnitude.ToString() + " 1:" + diff1.magnitude.ToString(), this.style);

			if(diff0.magnitude < rangeCheckpoint){
				currentCheckpointIndex = 0;
				currentRoute = 0;
			}
			if(diff1.magnitude < rangeCheckpoint){
				currentCheckpointIndex = 0;
				currentRoute = 1;
			}
		} else if (currentCheckpointIndex < 3){
			// verify next checkpoint
			int nextIndex = currentCheckpointIndex + 1;
			Vector2 diff = CHECKPOINTS[currentRoute, nextIndex] - this.currentLatLng;

			GUI.Label(new Rect(0, 0, 600, 100), "Waiting " + nextIndex.ToString() + " checkpoint: " + diff.magnitude.ToString());

			if(diff.magnitude < rangeCheckpoint){
				currentCheckpointIndex ++;
				SceneController.Instance.Checkpoint();
			}

			// calculate direction
			float lat2 = CHECKPOINTS[currentRoute, 0].x;
			float lng2 = CHECKPOINTS[currentRoute, 0].y;
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