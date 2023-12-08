using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameController : MonoBehaviour
{
    [SerializeField] private int maxWidth;
    [SerializeField] private int maxHeight;
    [SerializeField] private Vector2 gridOffset;
    [SerializeField] private GameObject gridTilePrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private UI uiController;

    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private Ball ball;

    private int activeTileCount;

    private Transform[,] grid;

    private float roundTimer;

    private bool hasRoundEnded;

    private int roundCount = 1;
    private int redRounds = 0;
    private int blueRounds = 0;

    private delegate void OnResetGame();
    private event OnResetGame onResetGame;

    private void Start()
    {
        roundCount = 1;
        uiController.Init(roundTimer);

        onResetGame += ResetGame;
        onResetGame += ball.ResetGame;
        onResetGame?.Invoke();
    }

    private void ResetGame()
    {
        roundTimer = gameSettings != null ? gameSettings.maxRoundTimer : 60;

        SetGrid(CreateGrid());
        uiController.SetUI(roundCount, gameSettings ? gameSettings.maxRounds : 5);
        hasRoundEnded = false;
    }

    private void Update()
    {
        if (hasRoundEnded) return;

        if (roundTimer > 0)
        {
            roundTimer -= Time.deltaTime;
            uiController.UpdateTimer(roundTimer);
        }

        else
        {
            hasRoundEnded = true;
            StartCoroutine(OnRoundEnd());
        }
    }

    private IEnumerator OnRoundEnd(float _delay = 5)
    {        
        int _winner = TileBehaviour.blueTileCount > TileBehaviour.redTileCount ? 1 : TileBehaviour.blueTileCount < TileBehaviour.redTileCount ? 2 : 0;
        if (_winner == 1) blueRounds++;
        if (_winner == 2) redRounds++;
        if (_winner != 0) roundCount++;

        int _maxRounds = gameSettings ? gameSettings.maxRounds : 5;

        uiController.OnUpdateRound(roundCount, _maxRounds, redRounds, blueRounds);

        if (blueRounds == (_maxRounds / 2) + 1 || blueRounds == (_maxRounds / 2) + 1) yield return null;

        yield return new WaitForSecondsRealtime(_delay);

        onResetGame?.Invoke();
    }

    public Transform[,] CreateGrid()
    {
        DestroyGrid();

        Transform[,] _newGrid = new Transform[maxWidth, maxHeight];
        activeTileCount = maxWidth * maxHeight;

        TileBehaviour.ResetTiles(activeTileCount);

        for (int _x = 0; _x < maxWidth; _x++)
        {
            for (int _y = 0; _y < maxHeight; _y++)
            {
                Transform _tile = Instantiate(gridTilePrefab, (new Vector2(_x, _y) / 2) + gridOffset, Quaternion.identity).transform;
                _newGrid[_x, _y] = _tile;
                _tile.SetParent(gridParent);
            }
        }

        return _newGrid;
    }

    public void DestroyGrid()
    {
        for (int i = gridParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gridParent.GetChild(i).gameObject);
        }

        grid = new Transform[0, 0];
    }

    public void SetGrid(Transform[,] _newGrid)
    {
        grid = _newGrid;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(gridOffset, .25f);
    }
}

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameController _controller = (GameController)target;

        if (GUILayout.Button("Create New Grid"))
        {
            _controller.SetGrid(_controller.CreateGrid());
        }

        if (GUILayout.Button("DestroyGrid"))
        {
            _controller.DestroyGrid();
        }
    }
}