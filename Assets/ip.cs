using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string addr_ip;
        try
        {
            //ホスト名を取得
            string hostname = System.Net.Dns.GetHostName();

            //ホスト名からIPアドレスを取得
            System.Net.IPAddress[] addr_arr = System.Net.Dns.GetHostAddresses(hostname);

            //探す
            addr_ip = "";
            foreach (System.Net.IPAddress addr in addr_arr)
            {
                string addr_str = addr.ToString();

                //IPv4 && localhostでない
                if (addr_str.IndexOf(".") > 0 && !addr_str.StartsWith("127."))
                {
                    addr_ip = addr_str;
                    break;
                }
            }
        }
        catch (Exception)
        {
            addr_ip = "";
        }

        var t = GetComponent<Text>();
        t.text = addr_ip;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
