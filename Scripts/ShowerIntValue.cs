using UnityEngine;
using UnityEngine.UI;

public class ShowerIntValue : MonoBehaviour
{
    [SerializeField] private Text text;

    public void ShowValue(int value)
    {
        text.text = value.ToString();
    }
}
