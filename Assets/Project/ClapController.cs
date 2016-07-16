using UnityEngine;
using System.Collections;

public class ClapController : MonoBehaviour {


	public GameObject pink;
	public GameObject black;


	public int blick = 5;

	private bool isPink = true;
	private int count;
	// Use this for initialization
	void Start () {
		count = blick;
	}

	public void StartClap ()
	{
		gameObject.SetActive (true);
	}



	// Update is called once per frame
	void Update () 
	{
		if (count-- < 1) {
			count = blick;
			SwitchIcon ();
		}
	}

	void SwitchIcon ()
	{
		isPink = !isPink;
		pink.SetActive (isPink);
		black.SetActive (!isPink);
	}
}
