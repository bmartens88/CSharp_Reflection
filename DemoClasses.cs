using System;

namespace ReflectionSample
{
    public interface ITalk {
        void Talk(string sentence);
    }

    public class EmployeeMarkerAttribute : Attribute
    { }

    [EmployeeMarker]
    public class Employee : Person
    {
        public string Company { get; set; }
    }

    public class Alien : ITalk
    {
        public void Talk(string sentence)
        {
            // talk...
            System.Console.WriteLine($"Alien talkin...: {sentence}");
        }
    }

    public class Person : ITalk
    {
        public string Name { get; set; }
        public int age;
        private string _aPrivateField = "initial private field value";

        public Person()
        {
            System.Console.WriteLine("A person is being created.");
        }

        public Person(string name)
        {
            System.Console.WriteLine($"A person with name {name} is being created.");
            Name = name;
        }

        private Person(string name, int age)
        {
            System.Console.WriteLine($"A person with name {name} and age {age} is being created using a private constructor.");
            Name = name;
            this.age = age;
        }

        public void Talk(string sentence)
        {
            // talk...
            System.Console.WriteLine($"Talking...: {sentence}");
        }

        protected void Yell(string sentence)
        {
            // yell...
            System.Console.WriteLine($"YELLING! {sentence}");
        }

        public override string ToString()
        {
            return $"{Name} {age} {_aPrivateField}";
        }
    }
}