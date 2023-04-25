using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTileAnimation : MonoBehaviour
{
    [SerializeField] Material _materialAnimationPrefab;
    [SerializeField] float _duration;
    [SerializeField] Color _startColor;
    [SerializeField] Color _endColor;
    
    public List<Tile> SelectedTiles { get; private set; }
    private Coroutine _animationCoroutine;

    private void Awake()
    {
        SelectedTiles = new List<Tile>();
        EventBus.OnTileSelected.AddListener(StartSelectedAnimation);
        EventBus.OnTileDeselected.AddListener(StopSelectedAnimation);
    }

    void StartSelectedAnimation(Tile tile)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }
        SelectedTiles.Add(tile);
        foreach (var Tile in SelectedTiles)
        {            
            Tile.SetBiomeColor();
        }
        _animationCoroutine = StartCoroutine(MainLoopAnimation(SelectedTiles));
    }

    void StopSelectedAnimation(Tile tile)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }
        SelectedTiles.Remove(tile);
        if (SelectedTiles.Count > 0)
        {
            _animationCoroutine = StartCoroutine(MainLoopAnimation(SelectedTiles));
        }
        else
        {
            _animationCoroutine = null;
        }
        tile.SetBiomeColor();
    }

    private IEnumerator MainLoopAnimation(List<Tile> tiles)
    {
        // Keep track of the total time the animation has been running
        float elapsedTime = 0f;

        while (tiles.Count > 0)
        {
            // Update the total elapsed time
            elapsedTime += Time.deltaTime;

            for (int i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                if (tile != null && tile.IsSelected)
                {
                    Material _tileMaterial = tile.TileMaterial;
                    Color startColor = _startColor;
                    Color endColor = _endColor;

                    // Calculate the time for this tile based on the shared timer
                    float t = elapsedTime % _duration;

                    _tileMaterial.color = Color.Lerp(startColor, endColor, t / _duration);
                }
            }

            yield return null;
        }        
    }
}
