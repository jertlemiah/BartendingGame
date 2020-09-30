using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(order = 0, fileName = "TutorialShortcutData", menuName = "Shortcut Data")]
public class TutorialShortcutData : ScriptableObject
{
    public List<ShortcutDataRecord> shortcuts;

    [System.Serializable]
    public class ShortcutDataRecord
    {
        [SerializeField]
        private readonly string m_Name;

        [FormerlySerializedAs("m_KeyboardRecord")]
        [SerializeField]
        private Binding m_Binding;

        public string Name => m_Name;

        public Binding Binding => m_Binding;
    }

    [System.Serializable]
    public struct Binding
    {
        [SerializeField]
        private readonly KeyCode m_KeyCode;

        [SerializeField]
        private readonly EventModifiers m_Modifiers;

        public Binding(KeyCode keyCode, EventModifiers modifiers) : this()
        {
            this.m_KeyCode = keyCode;
            this.m_Modifiers = modifiers;
        }

        public KeyCode KeyCode => m_KeyCode;
         
        public EventModifiers Modifiers => m_Modifiers;

        public static Binding None => new Binding();
    }
}
