using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _visionDistance = 100f;
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _maxWeight = 50f;
    [SerializeField] float _accuracy = 50f;
    [SerializeField] int _personalInventorySlots = 5;
    [SerializeField] Transform _mainPlayerGO;
    Vector3 position;
    float _currentHealt;

    public int PersonalInventorySlots { get => _personalInventorySlots; }
    public float MaxWeight { get => _maxWeight; }
    

    private void Awake()
    {
        EventBus.OnNewMapCreated.AddListener(SetPositionToMapCenter);
        EventBus.OnPlayerVisibleDistanceRequest.AddListener(SendPlayerVisionDistance);
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    void MainAction()
    {

    }
    void SecondaryAction()
    {

    }
    void LeftClickAction()
    {

    }
    void SetPositionToMapCenter((Vector3 centerOfMap, Vector2Int tileSize) value)
    {
        _mainPlayerGO.transform.position = value.centerOfMap;
    }
    private void SendPlayerVisionDistance()
    {
        EventBus.OnPlayerVisionDistanceChanged.Invoke(_visionDistance);
    }
    private void ChangeVisionDistance(float distance)
    {
        _visionDistance = distance;
        EventBus.OnPlayerVisionDistanceChanged.Invoke(_visionDistance);
    } 
    
}
