using System;
using System.Collections.Generic;

namespace IocExample
{
    public class ClassBinder
    {
        private readonly Dictionary<Type, Tuple<Type, Dictionary<string, object>>> _dep;
        private readonly Type _key;
        public ClassBinder(Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dep, Type key)
        {
            this._dep = dep;
            this._key = key;
        }
        public ArgumentsBinder To<TType>() where TType : class
        {
            _dep[_key] = new Tuple<Type, Dictionary<string, object>>(
                typeof(TType),
                new Dictionary<string, object>());

            return new ArgumentsBinder(_dep, _key);
        }
        public ArgumentsBinder ToSelf()
        {
            _dep[_key] = new Tuple<Type, Dictionary<string, object>>(
                _key, new Dictionary<string, object>());

            return new ArgumentsBinder(_dep, _key);
        }

    }
}