using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventSO))]
public class GameEventEditor : Editor
{
    private GameEventSO eventSO;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        eventSO = (GameEventSO)target;

        if(GUILayout.Button("Raise event"))
        {
            eventSO.Raise();
        }
    }
}
