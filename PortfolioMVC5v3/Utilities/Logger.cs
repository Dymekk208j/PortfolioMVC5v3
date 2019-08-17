using System;

namespace PortfolioMVC5v3.Utilities
{
    public static class Logger
    {
        public static void Log(Exception exception)
        {
            var ex = exception;
            Console.WriteLine(ex);
        }
    }
}