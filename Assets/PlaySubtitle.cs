using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Holoville.HOTween;
public class PlaySubtitle : MonoBehaviour
{
    public string lylicTextFileNameWithoutExtension;

	public Transform cloneParent;

    [SerializeField]
    private GameObject subtitlePrefab;
    private GameObject currentSubtitle;

    private IEnumerator DisplaySubtitle(string subtitle, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(currentSubtitle);
        currentSubtitle = Instantiate(subtitlePrefab) as GameObject;
		foreach (var t in currentSubtitle.GetComponentsInChildren<TextMesh> ()) {
			t.text = subtitle;
		}
		currentSubtitle.transform.SetParent (cloneParent);
		var loc = currentSubtitle.transform.localPosition;
		loc.z += 3;
		//currentSubtitle.transform.localPosition = loc;

		HOTween.To (currentSubtitle.transform, 8, "localPosition", loc).easeType = EaseType.EaseOutQuad;
    }

    public void Start()
    {
        var lylics = Resources.Load(lylicTextFileNameWithoutExtension) as TextAsset;
		var text = lylics.text;
		//text = text.Replace ('\r', "");
  //      lylics.text.Split(new char[]{'\n'});

        foreach(var line in lylics.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
        {
            var subtitle_and_timing = line.Split(',');
            StartCoroutine(DisplaySubtitle(subtitle_and_timing[0], Convert.ToSingle(subtitle_and_timing[1])));
        }
    }
}
