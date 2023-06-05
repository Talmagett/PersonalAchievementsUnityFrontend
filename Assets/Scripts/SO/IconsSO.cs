using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "Icon", menuName = "SO/CreateIcon")]
public class IconsSO : ScriptableObject
{
    [SerializeField] private string _label;
    [PreviewField(Alignment = ObjectFieldAlignment.Left, Height = 100)]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _iconId;
    private void OnValidate()
    {
        _label = name;
    }
    public string Label => _label;
    public Sprite Sprite => _sprite;
    public int IconId => _iconId;
}