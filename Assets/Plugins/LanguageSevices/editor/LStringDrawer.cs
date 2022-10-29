namespace LanguageSevices
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(LString))]
    public class LStringDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = "[textTag] " + label.text;
            label.tooltip = ((TooltipAttribute)attribute)?.tooltip;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_textTag"), label);
        }
    }
}
