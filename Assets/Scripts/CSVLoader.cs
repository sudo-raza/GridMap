using UnityEngine;
using System.IO; // Note: This may not be necessary unless you're using it elsewhere in your script.
using UnityEditor; // Be cautious about using UnityEditor in a runtime script, it's generally for editor scripts.
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using System.Text.RegularExpressions; 

public class CSVLoader : MonoBehaviour
{
    public TextAsset[] csvTextAssets; // Array to store additional CSV TextAssets if needed

    // Singleton instance
    public static CSVLoader Instance;
    public TextAsset loadedCSV;
    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            // If no instance exists, this becomes the singleton instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // If an instance already exists that is not this, destroy this object
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        LoadCSVTextAssets();
    }

    public void LoadCSVTextAssets()
    {
        string resourcesPath = "Maps"; // The name of your subfolder within the Resources folder

        // Load all TextAssets from the specified path within the Resources folder
        csvTextAssets = Resources.LoadAll<TextAsset>(resourcesPath);

        // Optionally, print the names of the loaded TextAssets to verify
        foreach (TextAsset textAsset in csvTextAssets)
        {
            Debug.Log(textAsset.name);
        }
    }
    public void changeCSVFile(string name)
    {
        Debug.Log("!!!!!!!!!!!!!!" + name);
        loadedCSV = FindCSVByName(name.Replace(" ", ""));
    }
    TextAsset FindCSVByName(string name) //string name
    {
        name = RemoveAllWhitespace(name);
        Debug.Log("name == " + name);
        foreach (TextAsset csv in csvTextAssets)
        {
            Debug.Log(csv.name);
            if (csv.name == name)
                return csv;
        }
        Debug.LogWarning("CSV with name " + name + " not found.");
        return null;
    }
    public static string RemoveAllWhitespace(string input)
    {
        // This regex pattern matches any whitespace character.
        // \s matches spaces, tabs, new lines, carriage returns, etc.
        return Regex.Replace(input, @"\s+", "");
    }
}
