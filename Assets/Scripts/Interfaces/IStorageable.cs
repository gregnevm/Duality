using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  interface IStorageable
{
    public void OpenStorage();
    public void GetFromStorage();
    public void SaveToStorage();

}
