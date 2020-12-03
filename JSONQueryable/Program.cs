using System;

namespace JSONQueryable
{
    class Program
    {
        static void Main(string[] args)
        {
            string filter = args[0];

            string jsonObject = args[1];

            bool isMatch = Predicate.IsMatch(jsonObject, filter);

            Console.WriteLine(isMatch);
            Console.ReadLine();     
        }
    }
}
