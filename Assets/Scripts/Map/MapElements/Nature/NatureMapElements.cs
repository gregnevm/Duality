using UnityEngine;
using UnityEngine.UI;

public abstract class NatureMapElements : MapElements, IMineable
{
    [SerializeField] private Item _earnedItem;
    [SerializeField] private int _minCountEarnedItems, _maxCountEarnedItems;
    [SerializeField] private float _miningTime = 1f;

    private int _currentItemsCount;
    private float _miningProgress;
    private float _standardMiningDuration;

    private void Start()
    {
        _currentItemsCount = Random.Range(_minCountEarnedItems, _maxCountEarnedItems);
        _miningProgress = 0f;
    }

    private void OnMouseDown()
    {
        Mining();
    }

    public void Mining()
    {
        float duration = _standardMiningDuration;
        if (_currentItemsCount > 0 && _miningProgress >= 1f)
        {
            _currentItemsCount--;
            _miningProgress = 0f;

            if (_currentItemsCount == 0)
            {
                Destroy(gameObject);
            }
        }
        else if (duration > 0f)
        {
            _miningProgress += Time.deltaTime / duration;
        }
        else
        {
            _miningProgress += Time.deltaTime / _miningTime;
        }
    }
}
