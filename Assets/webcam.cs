using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class webcam : MonoBehaviour {

    private WebCamTexture webcamtex;
    // Use this for initialization
    void Start () {
        webcamtex = new WebCamTexture(640,480,30);   //コンストラクタ

        Renderer _renderer = GetComponent<Renderer>();  //Planeオブジェクトのレンダラ
        _renderer.material.mainTexture = webcamtex;    //mainTextureにWebCamTextureを指定
        webcamtex.Play();  //ウェブカムを作動させる
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
