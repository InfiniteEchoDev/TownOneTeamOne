using System;

namespace GameCreator.Runtime.Variables
{
    [Serializable]
    public abstract class TListSetPick : IListSetPick
    {
        public abstract int GetIndex(ListVariableRuntime list, int count);

        public abstract int GetIndex(int count);
    }
}