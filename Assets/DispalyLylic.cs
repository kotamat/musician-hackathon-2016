using UnityEngine;
using System.Collections;

public class DispalyLylic : MonoBehaviour {

    public GameObject characterPrefab;
    public GameObject[] markers;
    private Vector3[] characterPositons;

    public string lylic = "Hello, world!";

	// Use this for initialization
	void Start () {
        var difference = markers[1].transform.position - markers[0].transform.position;
        var distance = difference.magnitude;
        var lengthOfOneSection = distance / (lylic.Length + 1);
        var direction = difference.normalized;

        characterPositons = new Vector3[lylic.Length];
        for(int i = 0; i < characterPositons.Length; ++i)
        {
            characterPositons[i] = direction * (i + 1) * lengthOfOneSection + markers[0].transform.position;
            Instantiate(characterPrefab, characterPositons[i], Quaternion.identity);
            //var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = characterPositons[i];
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
