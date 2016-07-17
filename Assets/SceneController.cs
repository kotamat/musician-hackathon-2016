using UnityEngine;
using System.Collections;
using Holoville.HOTween;
public class SceneController : MonoBehaviour {

	static private SceneController _instance;
	static public SceneController Instance {
		get { return _instance;}
	}

	public AudioClip [] audioClips;
	public AudioSource audioSource;
	public ClapController clap;
	public int clapWait = 3;
	private Sequence clapWaiter;

	// Use this for initialization
	void Start () {
		_instance = this;
		//PlaySound ();

	}

	public void PlaySound (int index=0)
	{
		if (index == -1) {
			StopAllSound ();
		}
		audioSource.clip = audioClips [index];
		audioSource.Play ();

		clapWaiter = new Sequence ();
		clapWaiter.AppendInterval (clapWait);
		clapWaiter.AppendCallback (clap.StartClap);
		clapWaiter.Play ();

	}
	public void StopAllSound ()
	{
		audioSource.Stop ();
		clapWaiter.Kill ();
	}

}
