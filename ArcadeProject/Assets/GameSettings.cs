using UnityEngine;

[CreateAssetMenu(fileName = "new Settings Asset", menuName = "Scriptable Objects/Game Settings Asset")]
public class GameSettings : ScriptableObject
{
    [Range(15, 120)] public float maxRoundTimer;
    [Range(3, 99)] public int maxRounds;
    [Tooltip("Quantity of the map required for a default victory in Domination")] [Range(0f, 1f)] public float dominationQuota = 1f;

    public WinCondition winCondition;
}

public enum WinCondition
{
    Domination, // Player owning the most of the map wins - Capturing all tiles leads to a default win
    ForceTimer, // No matter how many tiles are taken, the game continues
    NoNeutral   // Game ends once all neutral tiles within the map have been taken
}