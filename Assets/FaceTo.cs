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
        mainCamera.transform.LookAt(sphere.transform);
    }
}
