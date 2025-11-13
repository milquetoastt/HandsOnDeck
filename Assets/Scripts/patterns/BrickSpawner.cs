using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TimedBrickPattern
{
    public Vector3 position;
    public Vector3 scale;
    public float delay;
}

[System.Serializable]
public class BrickPattern
{
    public string patternName;
    public bool isActive = true;
    public List<TimedBrickPattern> bricks;
}

public class BrickSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public FallingBrickj brickPrefab;
    public List<BrickPattern> patterns = new List<BrickPattern>();

    // spawns a chosen pattern by index or name
    public void SpawnPattern(int patternIndex)
    {
        if (patternIndex < 0 || patternIndex >= patterns.Count)
        {
            Debug.LogWarning("Invalid pattern index: " + patternIndex);
            return;
        }

        StartCoroutine(SpawnPatternCoroutine(patterns[patternIndex]));
    }

    private IEnumerator SpawnPatternCoroutine(BrickPattern pattern)
    {
        foreach (var brick in pattern.bricks)
        {
            yield return new WaitForSeconds(brick.delay);
            SpawnSingleBrick(brick.position, brick.scale);
        }
    }

    private void SpawnSingleBrick(Vector3 position, Vector3 scale)
    {
        FallingBrickj newBrick = Instantiate(brickPrefab, position, Quaternion.identity);
        newBrick.transform.localScale = scale;
    }
    public int GetPatternCount()
    {
        int count = 0;
        foreach (var pattern in patterns)
        {
            if (pattern.isActive)
                count++;
        }
        return count;
    }
    public int ConvertActiveIndexToActualIndex(int activeIndex)
    {
        int seen = 0;
        for (int i = 0; i < patterns.Count; i++)
        {
            if (!patterns[i].isActive)
                continue;

            if (seen == activeIndex)
                return i;

            seen++;
        }

        Debug.LogError("invalid active pattern index: " + activeIndex);
        return -1;
    }
}