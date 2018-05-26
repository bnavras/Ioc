using System;
using System.Collections.Generic;

namespace IocExample
{
    public class ClassBinder
    {
        private readonly Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dependences;
        private readonly Type bindingType;
        public ClassBinder(Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dependences, 
                                                                            Type bindingType)
        {
            this.dependences = dependences;
            this.bindingType = bindingType;
        }
        public ArgumentsBinder To<TType>() where TType : class
        {
            dependences[bindingType] = new Tuple<Type, Dictionary<string, object>>(
                typeof(TType),
                new Dictionary<string, object>());

            return new ArgumentsBinder(dependences, bindingType);
        }
        public ArgumentsBinder ToSelf()
        {
            dependences[bindingType] = new Tuple<Type, Dictionary<string, object>>(
                bindingType, new Dictionary<string, object>());

            return new ArgumentsBinder(dependences, bindingType);
        }

    }
}