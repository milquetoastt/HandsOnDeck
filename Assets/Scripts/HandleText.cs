using UnityEngine;
using System.IO;

public class HandleText : MonoBehaviour
{
    public static void WriteString(string nameFile, string toWrite)
    {
        string path = Path.Combine(Application.persistentDataPath, nameFile + ".txt");

        try
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(toWrite);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to write to file {path}: {e.Message}");
        }
    }

    public static string ReadString(string nameFile)
    {
        string path = Path.Combine(Application.persistentDataPath, nameFile + ".txt");

        if (!File.Exists(path))
        {
            Debug.LogWarning($"File not found, cannot read: {path}");
            return null;
        }

        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to read from file {path}: {e.Message}");
            return null;
        }
    }
}