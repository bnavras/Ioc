using System;
using System.Collections.Generic;
using System.Reflection;

namespace IocExample
{
    public class DependencyResolver
    {
        private readonly Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dependences;

        public DependencyResolver()
        {
            dependences = new Dictionary<Type, Tuple<Type, Dictionary<string, object>>>();
        }
        public ClassBinder Bind<TType>() where TType : class
        {
            return new ClassBinder(dependences, typeof(TType));
        }
        public TType Get<TType>() where TType : class
        {
            return (TType) Get(typeof(TType));
        }

        public object Get(Type type) 
        {
            var constructorInfo = Utils.GetSingleConstructor(dependences[type].Item1);
            var parameters = constructorInfo.GetParameters();

            if (parameters.Length == 0)
                return Utils.CreateInstance(dependences[type].Item1);

            List<object> arguments = new List<object>();
            foreach (ParameterInfo parameter in parameters)
            {
                arguments.Add(dependences[type].Item2.ContainsKey(parameter.Name)
                    ? dependences[type].Item2[parameter.Name]
                    : Get(parameter.ParameterType));
            }

            return Utils.CreateInstance(dependences[type].Item1, arguments);
        }
    }
}

