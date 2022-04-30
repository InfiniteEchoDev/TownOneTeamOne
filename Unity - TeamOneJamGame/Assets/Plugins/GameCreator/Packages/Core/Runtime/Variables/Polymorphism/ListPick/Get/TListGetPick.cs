using System;

namespace GameCreator.Runtime.Variables
{
    [Serializable]
    public abstract class TListGetPick : IListGetPick
    {
        public abstract int GetIndex(int count);
    }
}