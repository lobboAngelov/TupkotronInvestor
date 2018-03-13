using System;
using System.Threading.Tasks;

namespace Tupkachbot
{
    public class Program
    {
        public static Task Main(string[] args) => new Task(Console.WriteLine);
    }
}