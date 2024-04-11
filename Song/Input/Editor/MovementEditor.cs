using Song.Input;
using Song.Input.Module;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Movement))]
public class MovementEditor : Editor
{
    private Movement _movement;
    private int _controllerTypeIndex = 0;
    private string[] _controllerType = { "任意", "键盘", "触屏" };

    private void OnEnable()
    {
        _movement = (Movement)target;
        _controllerTypeIndex = (int)_movement.controllerType;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        GUILayout.BeginHorizontal();
        GUILayout.Label("控制器(controllerType)");
        GUILayout.FlexibleSpace();
        _controllerTypeIndex = EditorGUILayout.Popup(_controllerTypeIndex, _controllerType);
        _movement.controllerType = (ControllerType)_controllerTypeIndex;
        GUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        _movement.player = EditorGUILayout.ObjectField("受控物体(player)", _movement.player, typeof(GameObject), true) as GameObject;
        
        switch (_movement.controllerType)
        {
            case ControllerType.Keyboard:
                DrawKeyboardControls();
                break;
            case ControllerType.Touch:
                DrawTouchControls();
                break;
            case ControllerType.Any:
                DrawKeyboardControls();
                DrawTouchControls();
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(_movement);
        }
    }

    private void DrawKeyboardControls()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("键盘输入变量", EditorStyles.boldLabel);
        _movement.up = EditorGUILayout.TextField("向前(up)", _movement.up);
        _movement.down = EditorGUILayout.TextField("向后(down)", _movement.down);
        _movement.left = EditorGUILayout.TextField("向左(left)", _movement.left);
        _movement.right = EditorGUILayout.TextField("向右(right)", _movement.right);
    }

    private void DrawTouchControls()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("触摸屏输入变量", EditorStyles.boldLabel);
        _movement.touchArea = EditorGUILayout.ObjectField("触摸范围(touchArea)", _movement.touchArea, typeof(GameObject), true) as GameObject;
        _movement.touchController = EditorGUILayout.ObjectField("控制触摸按键(touchController)", _movement.touchController, typeof(GameObject), true) as GameObject;
    }
}
