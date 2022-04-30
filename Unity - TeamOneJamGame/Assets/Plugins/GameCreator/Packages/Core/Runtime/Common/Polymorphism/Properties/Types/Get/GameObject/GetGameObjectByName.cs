using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCreator.Runtime.Common
{
    [Title("Find by Name")]
    [Category("Game Objects/Find by Name")]
    
    [Image(typeof(IconSearch), ColorTheme.Type.Yellow)]
    [Description("Searches the scene for a Game Object with a specific name")]

    [Serializable] [HideLabelsInEditor]
    public class GetGameObjectByName : PropertyTypeGetGameObject
    {
        [SerializeField] protected PropertyGetString m_Name = new PropertyGetString("");

        public override GameObject Get(Args args)
        {
            string name = this.m_Name.Get(args);
            return GameObject.Find(name);
        }

        public override string String => this.m_Name.ToString();

        public override GameObject SceneReference => GameObject.Find(this.m_Name.ToString());
    }
}