using UnityEngine;
using System.Collections;
using Holoville.HOTween;
public class SceneController : MonoBehaviour {

	static private SceneController _instance;
	static public SceneController Instance {
		get { return _instance;}
	}

	public string [] lylics;
	public AudioClip [] audioClips;
	public AudioSource audioSource;
	public ClapController clap;
	public int clapWait = 3;
	private Sequence clapWaiter;
	public PlaySubtitle playPrefab;
	private PlaySubtitle subtitle;
	public Transform cloneParent;
	// Use this for initialization
	void Start () {
		_instance = this;
		PlaySound (1);

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

		playPrefab.cloneParent = cloneParent;
		playPrefab.lylicTextFileNameWithoutExtension = lylics[index];
		subtitle = Instantiate (playPrefab) as PlaySubtitle;

	}
	public void StopAllSound ()
	{
		audioSource.Stop ();
		clapWaiter.Kill ();
		if (subtitle != null) {
			Destroy (subtitle.gameObject);
		}
	}

}
