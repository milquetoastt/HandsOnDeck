using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
partial class Heart : VisualElement
{
    public Texture2D FilledHeart;
    public Texture2D EmptyHeart;
    bool _isFilled;
    Texture2D _currentHeart;

    [UxmlAttribute]
    public bool IsFilled
    {
        get => _isFilled;
        set
        {
            _isFilled = value;
            if (IsFilled)
                currentHeart = FilledHeart;
            else
                currentHeart = EmptyHeart;
        }
    }

    [UxmlAttribute]
    private Texture2D currentHeart
    {
        get => _currentHeart;
        set
        {
            _currentHeart = value;
            style.backgroundImage = _currentHeart;
        }
    }
}
