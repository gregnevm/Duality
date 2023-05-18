using UnityEngine;
using UnityEngine.UI;

public abstract class NatureMapElements : MapElements, IMineable
{
    [SerializeField] private Item _earnedItem;
    [SerializeField] private int _minCountEarnedItems, _maxCountEarnedItems;

    private int _currentItemsCount;
    private bool _isMousePressed = false;
    private bool _isMinningCompleted = false;

  
    private void Awake()
    {
        _currentItemsCount = Random.Range(_minCountEarnedItems, _maxCountEarnedItems);
    }

    private void OnMouseDown()
    {
        _isMousePressed = true;
    }

    private void OnMouseUp()
    {
        if (_isMousePressed)
        {
            _isMousePressed = false;
            Mining();
        }
    }

    public void Mining()
    {        
        if (_currentItemsCount > 0)
        {
            _currentItemsCount--;
            _earnedItem.DropFromMinning(gameObject.transform);
            Debug.Log("Success");
        }
        else
        {
            FinishMining();
        }
    }

    private void FinishMining()
    {
        Debug.Log("Mining finished");
        _isMinningCompleted = true;
    }
}
