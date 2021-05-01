using System;
using System.Reflection;

namespace ReflectionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var personType = typeof(Person);
            var personConstructors = personType.GetConstructors(
              BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            );

            foreach (var constructor in personConstructors)
            {
                System.Console.WriteLine(constructor);
            }

            var privatePersonConstructor = personType.GetConstructor(
              BindingFlags.Instance | BindingFlags.NonPublic,
              null,
              new Type[] { typeof(string), typeof(int) },
              null
            );

            System.Console.WriteLine(privatePersonConstructor);

            var person1 = personConstructors[0].Invoke(null);

            var person2 = personConstructors[1].Invoke(new object[] { "Kevin" });

            var person3 = personConstructors[2].Invoke(new object[] { "Kevin", 39 });

            var person4 = Activator.CreateInstance("ReflectionSample", "ReflectionSample.Person").Unwrap();

            var person5 = Activator.CreateInstance("ReflectionSample",
              "ReflectionSample.Person",
              true,
              BindingFlags.Instance | BindingFlags.Public,
              null,
              new object[] { "Kevin" },
              null,
              null);

            var personTypeFromString = Type.GetType("ReflectionSample.Person");
            var person6 = Activator.CreateInstance(personTypeFromString,
              new object[] { "Kevin" });

            var person7 = Activator.CreateInstance("ReflectionSample",
              "ReflectionSample.Person",
              true,
              BindingFlags.Instance | BindingFlags.NonPublic,
              null,
              new object[] { "Kevin", 39 },
              null,
              null);

            var assembly = Assembly.GetExecutingAssembly();
            var person8 = assembly.CreateInstance("ReflectionSample.Person");

            Console.ReadLine();
        }

        public void InspectingMetadata()
        {
            string name = "Kevin";
            // var stringType = name.GetType();
            var stringType = typeof(string);
            System.Console.WriteLine(stringType);

            var currentAssembly = Assembly.GetExecutingAssembly();
            var typesFromCurrentAssembly = currentAssembly.GetTypes();
            foreach (var type in typesFromCurrentAssembly)
            {
                System.Console.WriteLine(type.Name);
            }

            var oneTypeFromCurrentAssembly = currentAssembly.GetType("ReflectionSample.Person");
            System.Console.WriteLine(oneTypeFromCurrentAssembly.Name);

            var externalAssembly = Assembly.Load("System.Text.Json");
            var typesFromExternalAssembly = externalAssembly.GetTypes();
            var oneTypeFromExternalAssembly = externalAssembly.GetType("System.Text.Json.JsonProperty");

            var modulesFromModuleFromExternalAssembly = externalAssembly.GetModules();
            var oneModuleFromExternalAssembly = externalAssembly.GetModule("System.Text.Json.dll");

            var typesFromModuleFromExternalLibrary = oneModuleFromExternalAssembly.GetTypes();
            var oneTypeFromModuleFromExternalAssembly =
                oneModuleFromExternalAssembly.GetType("System.Text.Json.JsonProperty");

            // foreach(var constructor in oneTypeFromCurrentAssembly.GetConstructors())
            // {
            //     System.Console.WriteLine(constructor);
            // }

            foreach (var method in oneTypeFromCurrentAssembly.GetMethods(BindingFlags.Public | BindingFlags.NonPublic))
            {
                System.Console.WriteLine($"{method}, public: {method.IsPublic}");
            }

            foreach (var field in oneTypeFromCurrentAssembly.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                System.Console.WriteLine(field);
            }
        }
    }
}
