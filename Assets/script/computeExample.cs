using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computeExample : MonoBehaviour {

    public ComputeShader shader;

	// Use this for initialization
	void Start () {
        ComputeBuffer result = new ComputeBuffer(4, sizeof(int));
        shader.SetBuffer(0, "Result", result);

        shader.Dispatch(0, 1, 1, 1);

        int[] data = new int[4];

        result.GetData(data);

        for (int i = 0; i < 4; i++)
            Debug.Log(data[i]);

        result.Release();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
