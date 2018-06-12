using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setip : MonoBehaviour {
    public Text ip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set(string cahr)
    {
        ip.text += cahr;
    }
}
