using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    public UnityEvent<int> OnScoreChanged;

    void Awake()
    {
        score = 0;
    }

    void Start()
    {
        OnScoreChanged.Invoke(score);
    }

    public void AddScore(int amount)
    {
        score += amount;

        OnScoreChanged.Invoke(score);
    }
}
