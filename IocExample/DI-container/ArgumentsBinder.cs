using System;
using System.Collections.Generic;

namespace IocExample
{
    public class ArgumentsBinder
    {
        private readonly Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dependences;
        private readonly Type bindingType;
        public ArgumentsBinder(Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dependences, 
                                                                                Type bindingType)
        {
            this.dependences = dependences;
            this.bindingType = bindingType;
        }

        public ArgumentsBinder WithConstructorArgument(string name, object value)
        {
            dependences[bindingType].Item2[name] = value;
            return this;
        }
    }
}