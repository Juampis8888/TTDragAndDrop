using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class ConsultManager : MonoBehaviour
{   
    public static ConsultManager instance;

    public string FileName;

    private string PathJson;

    [SerializeField]
    private Root ListStudent = new Root();

    [SerializeField]
    private string Json;

    [SerializeField]
    private bool IsChange;

    void Awake()
    {   
        if(instance == null)
        {   
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        PathJson = Path.Combine(Application.streamingAssetsPath, FileName);
        if (File.Exists(PathJson))
        {
            StreamReader streamReader = new StreamReader(PathJson, Encoding.UTF8);
            Json = streamReader.ReadToEnd();
            Debug.Log(Json);
            ListStudent = JsonUtility.FromJson<Root>(Json);
            streamReader.Close();
        }
    }

    private void Update()
    {
        
        if (File.Exists(PathJson))
        {
            StreamReader streamReader = new StreamReader(PathJson, Encoding.UTF8);
            string Newjson = streamReader.ReadToEnd();
            if(Newjson != Json)
            {
                ListStudent = JsonUtility.FromJson<Root>(Newjson);
                IsChange = true;
                Json = Newjson;
            }
            streamReader.Close();
        }
    }

    public bool GetIsChange()
    {
        return IsChange;
    }

    public void SetIsChange(bool value)
    {
        IsChange = value;
    }

    public Root GetListStudent()
    {
        return ListStudent;
    }
}
