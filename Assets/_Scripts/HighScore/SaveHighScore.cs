using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveHighScore
{
    private static float maxScore = 0f;
    private static float lastScore = 0f;

    public static float GetLastScore() {  return lastScore; }
    public static void SetLastScore(float score) {  lastScore = score; if (lastScore > maxScore) maxScore = lastScore;  }
    public static float GetMaxScore() { return maxScore; }

}
