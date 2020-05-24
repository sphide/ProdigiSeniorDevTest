using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace ColourMatcher.Tests
{
    /*
     * The TestFactory class implements the following members:
     * Data: This property returns an IEnumerable collection of sample data. The key value pairs represent values that are passed into a query string.
     * CreateDictionary: This method accepts a key/value pair as arguments and returns a new Dictionary used to create QueryCollection to represent query string values.
     * CreateHttpRequest: This method creates an HTTP request initialized with the given query string parameters.
     * CreateLogger: Based on the logger type, this method returns a logger class used for testing. The ListLogger keeps track of logged messages available for evaluation in tests.
    */
    public class TestFactory
    {
        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>
            {
                new object[] { "imageurl", "https://pwintyimages.blob.core.windows.net/samples/stars/test-sample-black.png" },
                new object[] { "imageurl", "https://pwintyimages.blob.core.windows.net/samples/stars/test-sample-grey.png" },
                new object[] { "imageurl", "https://pwintyimages.blob.core.windows.net/samples/stars/test-sample-teal.png" },
                new object[] { "imageurl", "https://pwintyimages.blob.core.windows.net/samples/stars/test-sample-navy.png" }
            };
        }

        private static Dictionary<string, StringValues> CreateDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                { key, value }
            };
            return qs;
        }

        public static DefaultHttpRequest CreateHttpRequest(string queryStringKey, string queryStringValue)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection(CreateDictionary(queryStringKey, queryStringValue))
            };
            return request;
        }

        public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
        {
            ILogger logger;

            if (type == LoggerTypes.List)
            {
                logger = new ListLogger();
            }
            else
            {
                logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            }

            return logger;
        }
    }
}
