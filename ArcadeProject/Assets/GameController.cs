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

    private delegate void OnResetGame();
    private event OnResetGame onResetGame;

    private void Start()
    {
        onResetGame += ResetGame;
        onResetGame += ball.ResetGame;
        onResetGame?.Invoke();
    }

    private void ResetGame()
    {
        roundTimer = gameSettings != null ? gameSettings.maxRoundTimer : 60;
        uiController.Init(roundTimer);
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
        yield return new WaitForSecondsRealtime(_delay);


    }

    public Transform[,] CreateGrid()
    {
        DestroyGrid();

        Transform[,] _newGrid = new Transform[maxWidth, maxHeight];
        activeTileCount = maxWidth * maxHeight;
        
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