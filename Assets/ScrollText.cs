using UnityEngine;
using System.Collections;

public class ScrollText : MonoBehaviour {
    public float speed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= speed * Time.deltaTime * transform.up;
	}
}
