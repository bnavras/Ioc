using System;
using System.Collections.Generic;

namespace IocExample
{
    public class ArgumentsBinder
    {
        private readonly Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dep;
        private readonly Type _key;
        public ArgumentsBinder(Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dep, Type key)
        {
            this.dep = dep;
            this._key = key;
        }

        public ArgumentsBinder WithConstructorArgument(string name, object value)
        {
            dep[_key].Item2[name] = value;
            return this;
        }
    }
}