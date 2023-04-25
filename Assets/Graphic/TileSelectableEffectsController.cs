using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ISelectable))]
public class TileSelectableEffectsController : MonoBehaviour
{
    /*
    [SerializeField] private float _outlineWidth = 0.05f;
    [SerializeField] private float _outlineSpeed = 1f;
    [SerializeField] private float _glowIntensity = 1f;
    [SerializeField] private float _glowSpeed = 1f;
    [SerializeField] private Color _outlineColor = Color.white;
    [SerializeField] private Color _glowColor = Color.white;

    private readonly List<ISelectable> _selectedObjects = new List<ISelectable>();
    private Renderer _renderer;
    private Material _material;
    private float _outlineOffset;
    private float _glowOffset;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    private void Update()
    {
        foreach (var selectable in _selectedObjects)
        {
            _outlineOffset += Time.deltaTime * _outlineSpeed;
            _glowOffset += Time.deltaTime * _glowSpeed;

            _material.SetFloat("_OutlineWidth", _outlineWidth + Mathf.Sin(_outlineOffset) * _outlineWidth);
            _material.SetColor("_OutlineColor", _outlineColor);
            _material.SetFloat("_GlowIntensity", _glowIntensity + Mathf.Sin(_glowOffset) * _glowIntensity);
            _material.SetColor("_GlowColor", _glowColor);
        }
    }

    private void OnMouseDown()
    {
       
    }
    */
}
