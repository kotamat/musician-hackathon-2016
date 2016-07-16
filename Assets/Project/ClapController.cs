using UnityEngine;
using System.Collections;
using Holoville.HOTween;
public class ClapController : MonoBehaviour {


	public GameObject pink;
	public GameObject black;


	public float intervalSecond = 2;
	public int blick = 60;

	private bool isPink = true;
	private int count;
	// Use this for initialization
	void Start () {
		count = blick;
		StartClap ();
	}

	public void StartClap ()
	{
		gameObject.SetActive (true);

		var seq = new Sequence ();
		seq.loops = 1000;
		seq.AppendInterval (intervalSecond);
		seq.Append (HOTween.To (transform, 0.05f, "localScale", Vector3.one * 1.5f));
		seq.Append (HOTween.To (transform, 0.3f, "localScale", Vector3.one ));
		seq.Play ();
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
