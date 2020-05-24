using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ColourMatcher.Tests
{
    /*  
     *  Taken from Microsoft docs: https://docs.microsoft.com/en-us/azure/azure-functions/functions-test-a-function
     *  
     *  Each Azure function takes an instance of ILogger to handle message logging. 
     *  Some Azure function tests either don't log messages or have no concern for how logging is implemented. 
     *  Other tests need to evaluate messages logged to determine whether a test is passing.
     *  The ListLogger class implements the ILogger interface and holds an internal list of messages for evaluation during a test.
     *  
     *  The ListLogger class implements the following members as contracted by the ILogger interface:
     *  BeginScope: Scopes add context to your logging. In this case, the test just points to the static instance on the NullScope class to allow the test to function.
     *  IsEnabled: A default value of false is provided.
     *  Log: This method uses the provided formatter function to format the message and then adds the resulting text to the Logs collection.
     *  The Logs collection is an instance of List<string> and is initialized in the constructor.
     */
    public class ListLogger : ILogger
    {
        public IList<string> Logs;

        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => false;

        public ListLogger()
        {
            this.Logs = new List<string>();
        }

        public void Log<TState>(LogLevel logLevel,
                                EventId eventId,
                                TState state,
                                Exception exception,
                                Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);
            this.Logs.Add(message);
        }
    }
}
