using UnityEngine;
using System.IO;

public class HandleText : MonoBehaviour
{
    private const string DEFAULT_CONTENT = "0";

    public static void WriteString(string nameFile, string toWrite)
    {
        string path = Path.Combine(Application.persistentDataPath, nameFile + ".txt");
        string directoryPath = Path.GetDirectoryName(path);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

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

    public static string ReadOrCreateString(string nameFile)
    {
        string content = ReadString(nameFile);

        // If content is null, the file did not exist or failed to read.
        if (content == null)
        {
            // 1. Create the file with default content
            WriteString(nameFile, DEFAULT_CONTENT);

            // 2. Log what happened
            Debug.LogWarning($"File '{nameFile}.txt' not found. Created new file with default content: {DEFAULT_CONTENT}");

            // 3. Return the default content to the caller
            return DEFAULT_CONTENT;
        }

        // If content was successfully read, return it.
        return content;
    }
}