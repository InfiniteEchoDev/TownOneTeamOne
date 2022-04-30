using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Variables
{
    [Title("Local List Variable")]
    [Category("Variables/Local List Variable")]
    
    [Image(typeof(IconListVariable), ColorTheme.Type.Teal)]
    [Description("Returns the Vector3 value of a Local List Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetOffsetLocalList : PropertyTypeGetOffset
    {
        [SerializeField]
        protected FieldGetLocalList m_Variable = new FieldGetLocalList(ValueVector3.TYPE_ID);

        public override Vector3 Get(Args args) => this.m_Variable.Get<Vector3>();
        public override Vector3 Get(GameObject gameObject) => this.m_Variable.Get<Vector3>();

        public override string String => this.m_Variable.ToString();
    }
}