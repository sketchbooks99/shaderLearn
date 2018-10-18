using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computeTexture : MonoBehaviour {
    public GameObject plane;
    public ComputeShader shader;

    RenderTexture render_A;

    int kernelIndex_A;

    struct ThreadSize {
        public int x;
        public int y;
        public int z;

        public ThreadSize(uint x, uint y, uint z)
        {
            this.x = (int)x;
            this.y = (int)y;
            this.z = (int)z;
        }
    }

    ThreadSize kernelThreadSize_A;

	// Use this for initialization
	void Start () {
        // enableRandomWriteを有効にして
        // 書き込み可能な状態のテクスチャを生成することが重要
        this.render_A = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGB32);
        this.render_A.enableRandomWrite = true;
        this.render_A.Create();

        // カーネルのインデックスとサイズを保存
        this.kernelIndex_A = this.shader.FindKernel("CSMain");

        uint threadSizeX, threadSizeY, threadSizeZ;

        this.shader.GetKernelThreadGroupSizes(this.kernelIndex_A, out threadSizeX, out threadSizeY, out threadSizeZ);
        this.kernelThreadSize_A = new ThreadSize(threadSizeX, threadSizeY, threadSizeZ);

        this.shader.SetTexture(this.kernelIndex_A, "textureBuffer", this.render_A);
	}
	
	// Update is called once per frame
	void Update () {
        // DispatchメソッドでCompute Shaderを実行
        // グループ数は "解像度 / スレッド数" で実行
        // -> 全てのピクセルが処理される

        // 例...512 / 8 = 64 で各グループは64ピクセルずつ平行に処理する
        // グループスレッドは 64 / 8 = 8 ピクセルずつ平行に処理する

        this.shader.Dispatch(this.kernelIndex_A, this.render_A.width / this.kernelThreadSize_A.x,
                             this.render_A.height / this.kernelThreadSize_A.y,
                             this.kernelThreadSize_A.z);

        plane.GetComponent<Renderer>().material.mainTexture = this.render_A;
	}
}
