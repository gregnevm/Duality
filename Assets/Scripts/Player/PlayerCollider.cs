using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private Tile _playerLocatedTile;
    public Tile PlayerLocatedTile { get { return _playerLocatedTile; } }

    private void Awake()
    {
        EventBus.PlayerTileLocationRequest.AddListener(LocationSend);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Tile collidedTile = collision.gameObject.GetComponent<Tile>();
        if (collidedTile != null)
        {
            _playerLocatedTile = collidedTile;
            Debug.Log("Tile Collider");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Tile collidedTile = other.gameObject.GetComponent<Tile>();
        if (collidedTile != null)
        {
            _playerLocatedTile = collidedTile;
            Debug.Log("Tile Collider");
        }
    }
    private void LocationSend()
    {
        if (_playerLocatedTile != null)
        {
            EventBus.OnTilePlayerLocationSend.Invoke(PlayerLocatedTile);
        }
    }
}
