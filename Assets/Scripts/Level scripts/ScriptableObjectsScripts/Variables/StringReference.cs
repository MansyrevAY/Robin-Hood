using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[System.Serializable]
public class StringReference
{
    public bool useConstant = true;
    public string constantValue;
    public StringVariable variable;
}



[CreateAssetMenu(fileName ="String variable", menuName = "Scriptable objects/String Variable")]
public class StringVariable : ScriptableObject
{
    public string value;
}
