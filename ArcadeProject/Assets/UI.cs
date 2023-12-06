using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpBlue;
    [SerializeField] private TextMeshProUGUI tmpRed;
    [SerializeField] private TextMeshProUGUI tmpTimer;
    [SerializeField] private Image fillTimer;

    private float maxTime;

    public void Init(float _maxTime)
    {
        maxTime = _maxTime;

        TileBehaviour.onTileTakeOver += OnTileUpdate;
    }

    public void UpdateTimer(float _remainingTime)
    {
        fillTimer.fillAmount = _remainingTime / maxTime;
        tmpTimer.text = _remainingTime.ToString("f0");
    }

    public void OnUpdateRound()
    {

    }

    public void OnTileUpdate(int _blueTiles, int _redTiles)
    {
        tmpBlue.text = _blueTiles.ToString();
        tmpRed.text = _redTiles.ToString();
    }
}
