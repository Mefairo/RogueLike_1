using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{



    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Saving previous GUI enabled value (Сохранение предыдущего значения с включенным графическим интерфейсом)
        var previousGUIState = GUI.enabled;

        // Disabling edit for property (Отключение редактирования свойства)
        GUI.enabled = false;

        // Drawing Property (Свойство рисования)
        EditorGUI.PropertyField(position, property, label);

        // Setting old GUI enabled value (Установка старого значения с включенным графическим интерфейсом)
        GUI.enabled = previousGUIState;
    }
}
