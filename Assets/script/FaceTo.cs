using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTo : MonoBehaviour {

    public GameObject sphere;
    public Camera mainCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.mainCamera.transform.position = new Vector3(Mathf.Sin(Time.time)*3.0f, 0.0f, Mathf.Cos(Time.time) * 3.0f);
        this.mainCamera.transform.LookAt(sphere.transform);
    }
}
