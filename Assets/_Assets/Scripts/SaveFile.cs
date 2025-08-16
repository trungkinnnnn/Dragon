
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveFile : MonoBehaviour
{
    private string _nameScene = "SampleScene 2";
    private string pathData = "/data.txt";
    private string path;
   
    private void Start()
    {
        path = Application.dataPath + pathData;
        if(File.Exists(path))
        {
            string text = File.ReadAllText(path);
            Debug.Log(text);    
        }
    }

    public void ActionSaveData()
    {
        Debug.Log("SaveFile");
        string data = "hello";
        File.WriteAllText(path, data);
    }

    public void SkipScene()
    {
        Debug.Log("Skip Scene");
        SceneManager.LoadScene(_nameScene);
    }
}
