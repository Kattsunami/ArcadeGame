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

    [SerializeField] private TextMeshProUGUI tmpRoundBlue;
    [SerializeField] private TextMeshProUGUI tmpRoundRed;
    [SerializeField] private TextMeshProUGUI tmpRoundOverall;
    [SerializeField] private TextMeshProUGUI tmpMatchPoint;

    private float maxTime;

    public void Init(float _maxTime)
    {
        maxTime = _maxTime;
        TileBehaviour.onTileTakeOver += OnTileUpdate;
    }

    public void SetUI(int _roundCount, int _roundMax)
    {
        tmpRoundOverall.text = $"{_roundCount}/{_roundMax}";
    }

    public void UpdateTimer(float _remainingTime)
    {
        fillTimer.fillAmount = _remainingTime / maxTime;
        tmpTimer.text = _remainingTime.ToString("f0");
    }

    public void OnUpdateRound(int _roundCount, int _maxRounds, int _redRounds, int _blueRounds)
    {
        tmpRoundBlue.text = _blueRounds.ToString();
        tmpRoundRed.text = _redRounds.ToString();
        tmpRoundOverall.text = $"{_roundCount}/{_maxRounds}";

        if (_redRounds == _blueRounds && _redRounds == (_maxRounds / 2))
        {
            // Tied Game, Next Point Wins
            //
            //

            tmpMatchPoint.text = "FINAL ROUND";
            tmpMatchPoint.color = new Color(0.5f, 1f, 0.5f);
        }

        else if (_redRounds == (_maxRounds / 2))
        {
            tmpMatchPoint.text = "MATCH POINT";
            tmpMatchPoint.color = new Color(1, 1, 0);

            // Match Point Text

        }

        else if (_blueRounds == (_maxRounds / 2))
        {
            tmpMatchPoint.text = "MATCH POINT";
            tmpMatchPoint.color = new Color(0, 1, 1);

            // Match Point Text

        }
    }

    public void OnTileUpdate(int _blueTiles, int _redTiles)
    {
        tmpBlue.text = _blueTiles.ToString();
        tmpRed.text = _redTiles.ToString();
    }
}
