using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlaySubtitle : MonoBehaviour
{
    public string lylicTextFileNameWithoutExtension;

    [SerializeField]
    private GameObject subtitlePrefab;
    private GameObject currentSubtitle;

    private IEnumerator DisplaySubtitle(string subtitle, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(currentSubtitle);
        currentSubtitle = Instantiate(subtitlePrefab) as GameObject;
        currentSubtitle.GetComponentInChildren<TextMesh>().text = subtitle;
    }

    public void Start()
    {
        var lylics = Resources.Load(lylicTextFileNameWithoutExtension) as TextAsset;
        lylics.text.Split(new char[]{'\n'});

        foreach(var line in lylics.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
        {
            var subtitle_and_timing = line.Split(',');
            StartCoroutine(DisplaySubtitle(subtitle_and_timing[0], Convert.ToSingle(subtitle_and_timing[1])));
        }
    }
}
