using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using freebyTech.Common.ExtensionMethods;
using Xunit;

namespace freebyTech.Common.Tests.ExtensionMethods
{
    public class UriExtensionsTests
    {
        [Theory,
         MemberData(nameof(ComplexUriTests))]
        public void CompareAbsoluteToAppended(string absoluteUri, string baseUri, params string[] paths)
        {
            Assert.Equal(new Uri(absoluteUri).AbsoluteUri, (new Uri(baseUri).Append(paths)).AbsoluteUri, true);
        }

        public static IEnumerable<object[]> ComplexUriTests => new[]
        {
            new object[] {
                "http://www.freebyTech.com/uri1/uri2/uri3",
                "http://www.freebyTech.com",
                new [] { "uri1", "uri2", "URI3" }
            },
            new object[] {
                "http://www.freebyTech.com/uri1 /uri2/uri3",
                "http://www.freebyTech.com",
                new [] { "uri1 ", "/uri2/", "///URI3" }
            },
            new object[] {
                "http://www.freebyTech.com/uri1 /uri2/uri3",
                "http://www.freebyTech.com",
                new [] { "uri1 ", "/uri2/URI3" }
            },
            new object[] {
                "http://www.freebyTech.com/uri1 /uri2/uri3",
                "http://www.freebyTech.com",
                new [] { "/uri1 /", "/uri2/", "/URI3/" }
            },
            new object[] {
                "http://www.freebyTech.com/uri1/uri2/uri3",
                "http://www.freebyTech.com/",
                new [] { "/uri1/uri2/URI3/" }
            }
        };
    }
}
