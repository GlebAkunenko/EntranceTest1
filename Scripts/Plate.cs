using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    [SerializeField] private Item.Type type;

    //возможно это усложнение, но мне показалось неудобным указывать каждое поле, в котором необходмо выводить значение.
    [SerializeField] private UnityIntEvent onCountChange;

    private int count = 0;
    private int Count
    {
        get => count;
        set
        {
            count = value;
            onCountChange.Invoke(count);
        }
    }

    public Item.Type Type { get => type; set => type = value; }

    public void AddItem()
    {
        Count++;
        LevelController.self.Progress++;
    }

    public void ResetCounter()
    {
        Count = 0;
    }

}

[System.Serializable]
public class UnityIntEvent : UnityEvent<int> { }