using System.Collections;
using System.Collections.Generic;
using UnetLLAPISample;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class TextureSender : MonoBehaviour
{
    public LLAPINetworkManager NetworkManager;
    public float Interval = 0.1f;
    Type textureType;
    Texture2D texture2D;
    RenderTexture renderTexture;
    private WebCamTexture webcamtex;
    public static int hight =1280 ,weight =720;
    void Start()
    {
        webcamtex = new WebCamTexture(weight,hight, 30);   //コンストラクタ

        Renderer _renderer = GetComponent<Renderer>();  //Planeオブジェクトのレンダラ
        _renderer.material.mainTexture = webcamtex;    //mainTextureにWebCamTextureを指定
        webcamtex.Play();  //ウェブカムを作動させる

        StartCoroutine(SendTextureLoop());
    }

    void Update()
    {
    }

    IEnumerator SendTexture()
    {
        var texture = GetComponent<Renderer>().material.mainTexture;
        textureType = texture.GetType();


        if (textureType == typeof(Texture2D) )
        {
            texture2D = texture as Texture2D;
        }
        else if (textureType == typeof(RenderTexture))
        {
            renderTexture = texture as RenderTexture;

        }
        else if(textureType == typeof(WebCamTexture))
        {
            Texture mainTexture = texture;
            texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGB24, false);

            RenderTexture currentRT = RenderTexture.active;

            renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
            // mainTexture のピクセル情報を renderTexture にコピー
            Graphics.Blit(mainTexture, renderTexture);
            //yield return null;
            // renderTexture のピクセル情報を元に texture2D のピクセル情報を作成
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();

            //Color[] pixels = texture2D.GetPixels();

            //RenderTexture.active = currentRT;
            //MonoBehaviour.Destroy(renderTexture);
        }

        if (textureType == typeof(RenderTexture) )
        {
            RenderTexture currentActiveRT = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture2D = new Texture2D(renderTexture.width, renderTexture.height);
            texture2D.ReadPixels(new Rect(0, 0, texture2D.width, texture2D.height), 0, 0);
        }
        yield return null;
        var pngTexture = texture2D.EncodeToJPG(75);
        NetworkManager.SendPacketData(pngTexture, QosType.UnreliableFragmented);
        MonoBehaviour.Destroy(texture2D);
        MonoBehaviour.Destroy(renderTexture);
        //DestroyImmediate(texture2D);
        //DestroyImmediate(renderTexture);

    }

    IEnumerator SendTextureLoop()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            StartCoroutine( SendTexture());
            yield return new WaitForSeconds(Interval);
        }
    }
}
