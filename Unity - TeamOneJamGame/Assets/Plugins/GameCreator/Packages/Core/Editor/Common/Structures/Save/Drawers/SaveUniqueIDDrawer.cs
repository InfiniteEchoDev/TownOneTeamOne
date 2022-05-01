using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using GameCreator.Runtime.Common;

namespace GameCreator.Editor.Common
{
    [CustomPropertyDrawer(typeof(SaveUniqueID))]
    public class SaveUniqueIDDrawer : PropertyDrawer
    {
        private const string PATH_STYLES = EditorPaths.COMMON + "Structures/Save/StyleSheets/SaveUniqueID";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            
            StyleSheet[] styleSheets = StyleSheetUtils.Load(PATH_STYLES);
            foreach (var styleSheet in styleSheets) root.styleSheets.Add(styleSheet);

            SerializedProperty propertySave = property.FindPropertyRelative("m_Save");
            SerializedProperty propertyUniqueID = property.FindPropertyRelative("m_UniqueID");

            PropertyTool fieldSave = new PropertyTool(propertySave);
            PropertyTool fieldUniqueID = new PropertyTool(propertyUniqueID);
            
            root.AddToClassList("gc-saveuniqueid-root");
            fieldSave.AddToClassList("gc-saveuniqueid-save");
            fieldUniqueID.AddToClassList("gc-saveuniqueid-uniqueid");
            
            root.Add(fieldSave);
            root.Add(fieldUniqueID);

            return root;
        }
    }
}