using System;
using System.Collections;
using System.Collections.Generic;

namespace ActivatorTest
{
    class Program
    {
        static void Main(string[] args)
        {

            string typeName = typeof(IAraba).ToString();

            Type t = Type.GetType(typeName);
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(t);
            var instance = Activator.CreateInstance(constructedListType);
            bool result = instance is List<IAraba>;

            (instance as IList).Add(new Ford { Name = "Fiesta" });

        }
    }


    public interface IAraba
    {
        public string Name { get; set; }
    }
    public class Ford : IAraba
    {
        public string Name { get; set; }
    }

}
