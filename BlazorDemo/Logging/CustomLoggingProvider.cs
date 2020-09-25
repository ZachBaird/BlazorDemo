using Microsoft.Extensions.Logging;
using System;

namespace BlazorDemo.Logging
{
    public class CustomLoggingProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public class Logger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                throw new NotImplementedException();
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                throw new NotImplementedException();
            }
        }
    }
}
