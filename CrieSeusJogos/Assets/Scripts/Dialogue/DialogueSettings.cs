using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialog")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string dialogue;

    public List<Sentences> dialogues = new();
}

[Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;
}

[Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DialogueSettings ds = (DialogueSettings)target;

        Languages l = new();
        l.portuguese = ds.dialogue;

        Sentences s = new();
        s.profile = ds.speakerSprite;
        s.sentence = l;

        if (GUILayout.Button("Create Dialogue"))
        {
            if (string.IsNullOrWhiteSpace(ds.dialogue))
            {
                ds.dialogues.Add(s);

                ds.speakerSprite = null;
                ds.dialogue = string.Empty;
            }
        }
    }
}

#endif
