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
    public List<TimedBrickPattern> bricks;
}

public class BrickSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public FallingBrickj brickPrefab;
    public List<BrickPattern> patterns = new List<BrickPattern>();

    // Spawns a chosen pattern by index or name
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

    // Optionally get number of patterns for external scripts
    public int GetPatternCount() => patterns.Count;
}