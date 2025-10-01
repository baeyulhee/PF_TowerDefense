using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


[Serializable]
public class LabeledButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _label;
    [SerializeField] Button _button;

    public TextMeshProUGUI Label => _label;
    public Button Button => _button;
}
