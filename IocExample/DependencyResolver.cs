using System;
using System.Collections.Generic;
using System.Reflection;

namespace IocExample
{
    class DependencyResolver
    {
        private readonly Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dep;

        public DependencyResolver()
        {
            dep = new Dictionary<Type, Tuple<Type, Dictionary<string, object>>>();
        }

        public TType Get<TType>() where TType : class
        {
            return (TType) Get(typeof(TType));
        }

    public object Get(Type type) 
        {
            var constructorInfo = Utils.GetSingleConstructor(dep[type].Item1);
            var parameters = constructorInfo.GetParameters();

            if (parameters.Length == 0)
                return Utils.CreateInstance(dep[type].Item1);

            List<object> arguments = new List<object>();
            foreach (ParameterInfo parameter in parameters)
            {
                arguments.Add(dep[type].Item2.ContainsKey(parameter.Name)
                    ? dep[type].Item2[parameter.Name]
                    : Get(parameter.ParameterType));
            }

            return Utils.CreateInstance(dep[type].Item1, arguments);
        }

        public ClassBinder Bind<TType>() where TType : class
        {
            return new ClassBinder(dep, typeof(TType));
        }
    }

    public class ClassBinder
    {
        private readonly Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dep;
        private readonly Type key;
        public ClassBinder(Dictionary<Type, Tuple<Type, Dictionary<string, object>>> dep, Type key)
        {
            this.dep = dep;
            this.key = key;
        }
        public ArgumentsBinder To<TType>() where TType : class
        {
            dep[key] = new Tuple<Type, Dictionary<string, object>>(
                typeof(TType), 
                new Dictionary<string, object>());

            return new ArgumentsBinder(dep, key);
        }
        public ArgumentsBinder ToSelf()
        {
            dep[key] = new Tuple<Type, Dictionary<string, object>>(
                key, new Dictionary<string, object>());

            return new ArgumentsBinder(dep, key);
        }

    }

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

