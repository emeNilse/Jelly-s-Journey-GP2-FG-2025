using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
namespace GraphViewTools
{
    [UxmlElement]
    public partial class InspectorView : VisualElement
    {
        public InspectorView() { }
        private Editor _editor;
        public void UpdateSelection(NodeView nodeView)
        {
            Clear();
            Object.DestroyImmediate(_editor);
            _editor = Editor.CreateEditor(nodeView.Node);
            SerializedObject serializedNode = new SerializedObject(nodeView.Node);
            IMGUIContainer container = new IMGUIContainer(() => { _editor.OnInspectorGUI(); });
            Add(container);
        }
    }
}