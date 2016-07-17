using UnityEngine;
using System.Collections;
using Holoville.HOTween;
public class SceneController : MonoBehaviour {

	public enum Status
	{
		None=100,
		Cat=0,
		Meiji=1
	}

	static private SceneController _instance;
	static public SceneController Instance {
		get { return _instance;}
	}

	public string [] lylics;
	public AudioClip [] audioClips;
	public AudioSource audioSource;
	public ClapController clap;
	public int[] clapWait;
	private Sequence clapWaiter;
	public PlaySubtitle playPrefab;
	private PlaySubtitle subtitle;
	public Transform cloneParent;
	public GameObject arrowCube;
	public Transform arCamera;

	public Status status = Status.None;

	// Use this for initialization
	void Start () {
		_instance = this;
		clap.gameObject.SetActive (false);
		arrowCube.SetActive (false);
		//PlaySound (1);

	}
	void Update ()
	{
		//LoastMarker ();
	}
	public void Checkpoint ()
	{
	}

	public void LoastMarker ()
	{
		if (arCamera == null) return;
		arCamera.localPosition = Vector3.zero;
		arCamera.localRotation = new Quaternion ();
	}

	public void PlaySound (int index=0)
	{
		if (index == -1) {
			StopAllSound ();
		}
		status = (Status)(index);
		arrowCube.SetActive (true);

		audioSource.clip = audioClips [index];
		audioSource.Play ();

		//if (index == 0 || index == 1) { }
		clapWaiter = new Sequence ();
		clapWaiter.AppendInterval (clapWait[index]);
		clapWaiter.AppendCallback (clap.StartClap);
		clapWaiter.Play ();

		playPrefab.cloneParent = cloneParent;
		playPrefab.lylicTextFileNameWithoutExtension = lylics [index];
		subtitle = Instantiate (playPrefab) as PlaySubtitle;
	}
	public void StopAllSound ()
	{
		audioSource.Stop ();
		clapWaiter.Kill ();
		arrowCube.SetActive (false);

		if (subtitle != null) {
			Destroy (subtitle.gameObject);
		}
	}

}
