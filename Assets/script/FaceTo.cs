using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTo : MonoBehaviour {

    public GameObject skull;
    public Camera mainCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.mainCamera.transform.position = new Vector3(Mathf.Sin(Time.time)*50.0f, 5.0f, Mathf.Cos(Time.time) * 50.0f);
        this.mainCamera.transform.LookAt(skull.transform);
    }
}
