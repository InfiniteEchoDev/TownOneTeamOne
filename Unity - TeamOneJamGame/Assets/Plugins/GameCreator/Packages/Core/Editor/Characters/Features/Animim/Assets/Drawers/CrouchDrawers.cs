using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using UnityEditor;

namespace GameCreator.Editor.Characters
{
    [CustomPropertyDrawer(typeof(CrouchSingle))]
    public class CrouchSingleDrawers : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Crouch: Single";
    }
    
    [CustomPropertyDrawer(typeof(Crouch8Points))]
    public class Crouch8PointsDrawers : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Crouch: Circular 8 Points";
    }
    
    [CustomPropertyDrawer(typeof(Crouch16Points))]
    public class Crouch16PointsDrawers : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Crouch: Circular 16 Points";
    }
}