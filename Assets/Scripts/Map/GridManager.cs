using UnityEngine;
public class GridManager : MonoBehaviour
{
    [SerializeField] private GridDrawerService drawerService;
    [SerializeField] private MeshFilter gridMeshFilter;

    private Tile _centerOfGrid;
    private Vector2Int _tileSize = new Vector2Int(10, 10);
    private Vector3 _centerOfMap;
    private float _visionDistance = 100f;
    private bool _isGridEnabled;

    private void Awake()
    {
        EventBus.OnShowGridOnMap.AddListener(ShowGrid);
        EventBus.OnHideGridOnMap.AddListener(HideGrid);
        EventBus.OnNewMapCreated.AddListener(SetSizeOfTileAndMapcenter);
        EventBus.OnPlayerVisionDistanceChanged.AddListener(SetVisionDistance);
    }

    public void ShowGrid()
    {
        EventBus.OnPlayerVisibleDistanceRequest.Invoke();
        EventBus.PlayerTileLocationRequest.Invoke();

        DrawGridAroundCenterTile();
    }

    public void HideGrid()
    {
        gridMeshFilter.mesh = null;
    }

    public void DestroyGrid()
    {
        HideGrid();
        _centerOfGrid = null;
    }

    public void DrawGridAroundCenterTile()
    {
        drawerService.DrawGizmosGrid(_tileSize, _centerOfMap);        
    }

    public void GridSwitcher()
    {
        if (_isGridEnabled)
        {
            HideGrid();
            _isGridEnabled = false;
        }
        else
        {
            ShowGrid();
            _isGridEnabled = true;
        }
    }

    private void SetSizeOfTileAndMapcenter((Vector3 centerOfMap, Vector2Int size) value)
    {
        _tileSize = value.size;
        _centerOfMap = value.centerOfMap;
    }

    private void SetVisionDistance(float distance)
    {
        _visionDistance = distance;
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            ShowGrid();
        }
    }
}
