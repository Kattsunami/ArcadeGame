using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private int state;

    public static int neutralTileCount { get; private set; } = 0;
    public static int blueTileCount { get; private set; } = 0;
    public static int redTileCount { get; private set; } = 0;

    public delegate void OnTileTakeOver(int _redTiles, int _blueTiles);
    public static event OnTileTakeOver onTileTakeOver;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Color blueColour;
    [SerializeField] private Color redColour;

    public static void ResetTiles(int _totalTiles)
    {
        neutralTileCount = _totalTiles;
        blueTileCount = 0; 
        redTileCount = 0;
    }

    public void UpdateTile(int _newValue)
    {
        if (_newValue == state) return;

        switch (_newValue)
        {
            case 1:
                sprite.color = blueColour;
                blueTileCount++;
                if (state != 0) redTileCount--;
                else neutralTileCount--;
                break;
            case 2:
                sprite.color = redColour;
                redTileCount++;
                if (state != 0) blueTileCount--;
                else neutralTileCount--;
                break;
        }

        onTileTakeOver?.Invoke(blueTileCount, redTileCount);
        state = _newValue;
    }
}