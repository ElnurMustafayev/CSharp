using System;
using System.Collections.Generic;
using System.Drawing;

namespace Syntax {
    public enum Quadrant { Unknown, Origin, One, Two, Three, Four, OnBorder }

    class MyClass {
        public const double AvogadroConstant = 6.022_140_857_747_474e23;

        public double X { get; set; }
        public double Y { get; set; }

        public string Name { get; set; }
        public string NormalizeName => char.ToUpper(Name[0]) + Name.Substring(1).ToLower();

        ~MyClass() => Console.WriteLine("Object was destroyed...");

        public static string Rate(int rating) => (rating /= 5) switch {
            1 => "Bad",
            2 => "Normal",
            3 => "Good",
            4 => "Very good",
            5 => "Excellent",
            0 or _ => "error has occurred",
        };
        public static Quadrant GetQuadrant(Point point) => (point.X, point.Y) switch {
            (0, 0) => Quadrant.Origin,
            var (x, y) when x > 0 && y > 0 => Quadrant.One,
            var (x, y) when x < 0 && y > 0 => Quadrant.Two,
            var (x, y) when x < 0 && y < 0 => Quadrant.Three,
            var (x, y) when x > 0 && y < 0 => Quadrant.Four,
            var (_, _) => Quadrant.OnBorder,
        };
        public static void NormalizeNameMethod(string name) {
            try {
                Console.WriteLine(MethodWithMethods(name));
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        public static string MethodWithMethods(string name) {
            if(name != null && !string.IsNullOrEmpty(name))
                return Normalize(name);

            throw WriteError("Name parameter is null...");

            static string Normalize(string name) => char.ToUpper(name[0]) + name.Substring(1);
            static Exception WriteError(string message) {
                Console.WriteLine($"Error: {message}");
                return new Exception(message);
            }
        }
        public static (char, string) CutName(string name) => (name[0], name[1..]);
    }

    static class Extensions {
        public static bool IsLetterOrSeparator(this char c) =>
            c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',';
    }

    public record Person {
        public string LastName { get; set; }        // read-only by default
        public string FirstName { get; }            // read-only by default

        public Person(string first, string last) => (FirstName, LastName) = (first, last);
    }

    class Program {
        static void Main() {
            {
                // switch-case syntax
                Console.WriteLine(MyClass.Rate(int.TryParse(Console.ReadLine(), out int result) ? result : 0));
                Console.WriteLine(MyClass.GetQuadrant(new Point(0, 0)));

                // set many
                var (x, y) = (new List<int>(), "Some text there...");
                MyClass.NormalizeNameMethod("");
            }

            {
                string lorem = "Lorem ipsum dolor sit amen";
                string text = "Some test text there";
                int index = text.IndexOf("text");

                // indexers syntax
                Console.WriteLine("Dog".ToUpper()[^1]);
                Console.WriteLine(text[index..(index + "text".Length)]);
                Console.WriteLine(lorem[..lorem.IndexOf("dolor")]);
            }

            {
                List<int> numbers = null;
                int? i = null;

                // nullcheck with set
                numbers ??= new List<int>();
                numbers.Add(i ??= 17);
                numbers.Add(i ??= 20);

                Console.WriteLine(string.Join(" ", numbers));  // 17 17
            }

            {
                // fit
                Person person = new("John", "Brown");
                List<int> list = new();
                list?.AddRange(new int[] { 10, 20, 0 });

                // anonymous classes
                var obj = new { Login = "ElnurMustafayev", Password = "SecretPassword" };
                Console.WriteLine($"{obj.Login} {obj.Password}");
                Console.WriteLine(obj.GetType());   // <>f__AnonymousType0`2[System.String,System.String]

                // anonymous delegates
                Action<string> del1 = delegate (string message) { Console.WriteLine(message); };
                Action<string> del2 = (message) => Console.WriteLine(message);

                // null check
                int? num = null;
                string str = num?.ToString();
                if(str?.Length > 5 && str is not null)
                    Console.WriteLine(str[..5]);

                // is, and, or
                Console.WriteLine('z'.IsLetterOrSeparator());
            }

            {
                (int id, object value, bool @checked) keyvalue = (1, new { Name = "Elnur", Surname = "Mustafayev" }, true);
                int id = 123;
                string message = "Hello";
                var tuple = (id, message);
                Console.WriteLine(tuple.id);
                Console.WriteLine(tuple.GetType());         // System.ValueTuple`2[System.Int32,System.String]
                Console.WriteLine(keyvalue.GetType());      // System.ValueTuple`3[System.Int32,System.Object,System.Boolean]

                Console.WriteLine(MyClass.CutName("Elnur"));    // (E, lnur)
            }
        }
    }
}