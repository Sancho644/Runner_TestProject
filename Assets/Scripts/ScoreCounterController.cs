using System;

public static class ScoreCounterController
{
    public static int CurrentScore;
    public static Action OnChanged; 

    public static void SetValue(int value)
    {
        CurrentScore += value;
        OnChanged?.Invoke();
    }

    public static void SetCount(int value)
    {
        CurrentScore = value;
    }
}