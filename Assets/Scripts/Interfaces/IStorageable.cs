using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  interface IStorageable
{
    public Transform GetTransform();
    public void OpenStorage();
    public bool AcceptItem(Item item);
    public bool DropItem(Item item);
    public bool SendMyItem(IStorageable storage);

}
