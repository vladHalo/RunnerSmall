using UnityEngine;

public class RequiredFieldAttribute : PropertyAttribute
{
    public Color color;

    public RequiredFieldAttribute(FieldColor Fieldcolor = FieldColor.Red)
    {
        switch (Fieldcolor)
        {
            case FieldColor.Red:
                color = Color.red;
                break;
            case FieldColor.Green:
                color = Color.green;
                break;
            case FieldColor.Blue:
                color = Color.blue;
                break;
            case FieldColor.Magenta:
                color = Color.magenta;
                break;
            case FieldColor.Cyan:
                color = Color.cyan;
                break;
            case FieldColor.Yellow:
                color = Color.yellow;
                break;
            case FieldColor.Orange:
                color = new Color(1, 0.5f, 0);
                break;
            case FieldColor.Gray:
                color = Color.gray;
                break;
            default:
                color = Color.red;
                break;
        }
    }
}

public enum FieldColor
{
    Red,
    Green,
    Blue,
    Magenta,
    Cyan,
    Yellow,
    Orange,
    Gray
}

#if UNITY_EDITOR
[UnityEditor.CustomPropertyDrawer(typeof(RequiredFieldAttribute))]
public class RequiredFieldDrawer : UnityEditor.PropertyDrawer
{
    public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        RequiredFieldAttribute rf = attribute as RequiredFieldAttribute;

        if (property.objectReferenceValue == null)
        {
            var oldColor = GUI.color;

            GUI.color = rf.color;
            UnityEditor.EditorGUI.PropertyField(position, property, label);
            GUI.color = oldColor;
        }
        else
        {
            UnityEditor.EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif