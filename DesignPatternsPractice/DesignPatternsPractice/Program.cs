using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsPractice
{
    public class CodeElement
    {
        public string Value;
        public string Type;
        public List<CodeElement> Elements = new List<CodeElement>();
        private const int IndentSize = 2;

        public CodeElement()
        {

        }

        public CodeElement(string value, string type)
        {
            Value = value ?? throw new ArgumentNullException(paramName: nameof(value));
            Type = type ?? throw new ArgumentNullException(paramName: nameof(type));
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);
            sb.AppendLine($"public {Type} {Value}");
            sb.AppendLine("{");

            foreach (var e in Elements)
            {
                sb.AppendLine($"{i} public {e.Type} {e.Value}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class CodeBuilder
    {
        private readonly string className;
        private CodeElement root = new CodeElement();

        public CodeBuilder(string className)
        {
            this.className = className;
            root.Value = className;
            root.Type = "class";
        }

        public CodeBuilder AddField(string value, string type)
        {
            var e = new CodeElement(value, type);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new CodeElement { Value = className };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);

            Console.ReadLine();
        }
    }
}
