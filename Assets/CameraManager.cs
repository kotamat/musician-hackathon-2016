using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Camera arCam;
	public Transform bgPanel;
	// Use this for initialization
	void Start () {
		Invoke ("InitCam", 2);
	}

	void InitCam ()
	{
		arCam.farClipPlane = 4000;
		bgPanel.localPosition = new Vector3 (0, 0, 3000);
	}


	// Update is called once per frame
	void Update () {
	
	}
}
