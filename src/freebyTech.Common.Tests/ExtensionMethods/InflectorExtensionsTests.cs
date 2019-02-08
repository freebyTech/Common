using System.Collections.Generic;
using System.Linq;
using System.Net;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Transformations;
using Xunit;

namespace freebyTech.Common.Tests.Transformations
{
    public class InflectorExtensionTests
    {
        [Theory,
            InlineData("some title", "Some title"),
            InlineData("some Title", "Some title"),
            InlineData("someTitle", "Sometitle"),
            InlineData("SOMETITLE", "Sometitle"),
            InlineData("some title goes here", "Some title goes here"),
            InlineData("some TITLE", "Some title")]
        public void TestCapitlization(string test, string expected)
        {
            Assert.Equal(expected, test.Capitalize());
        }

        [Theory,
            InlineData("some_title", "some-title"),
            InlineData("some-title", "some-title"),
            InlineData("some_title_goes_here", "some-title-goes-here"),
            InlineData("some_title and_another", "some-title and-another")]
        public void TestDasherization(string test, string expected)
        {
            Assert.Equal(expected, test.Dasherize());
        }

        [Theory,
            InlineData(0, "0th"),
            InlineData(1, "1st"),
            InlineData(2, "2nd"),
            InlineData(3, "3rd"),
            InlineData(4, "4th"),
            InlineData(5, "5th"),
            InlineData(6, "6th"),
            InlineData(7, "7th"),
            InlineData(8, "8th"),
            InlineData(9, "9th"),
            InlineData(10, "10th"),
            InlineData(11, "11th"),
            InlineData(12, "12th"),
            InlineData(13, "13th"),
            InlineData(14, "14th"),
            InlineData(20, "20th"),
            InlineData(21, "21st"),
            InlineData(22, "22nd"),
            InlineData(23, "23rd"),
            InlineData(24, "24th"),
            InlineData(100, "100th"),
            InlineData(101, "101st"),
            InlineData(102, "102nd"),
            InlineData(103, "103rd"),
            InlineData(104, "104th"),
            InlineData(110, "110th"),
            InlineData(1000, "1000th"),
            InlineData(1001, "1001st")]
        public void OrdanizeNumbersTest(int number, string ordanized)
        {
            Assert.Equal(ordanized, number.Ordinalize());
        }

        [Theory,
            InlineData("customer", "customer"),
            InlineData("CUSTOMER", "cUSTOMER"),
            InlineData("CUStomer", "cUStomer"),
            InlineData("customer_name", "customerName"),
            InlineData("customer_first_name", "customerFirstName"),
            InlineData("customer_first_name_goes_here", "customerFirstNameGoesHere"),
            InlineData("customer name", "customer name")]
        public void TestCamelize(string test, string expected)
        {
            Assert.Equal(expected, test.Camelize());
        }

        [Theory,
            InlineData("customer", "Customer"),
            InlineData("CUSTOMER", "CUSTOMER"),
            InlineData("CUStomer", "CUStomer"),
            InlineData("customer_name", "CustomerName"),
            InlineData("customer_first_name", "CustomerFirstName"),
            InlineData("customer_first_name_goes_here", "CustomerFirstNameGoesHere"),
            InlineData("customer name", "Customer name")]
        public void TestPascalize(string test, string expected)
        {
            Assert.Equal(expected, test.Pascalize());
        }

        [Theory,
           MemberData(nameof(SingularAndPluralWords))]
        public void SingularizeTest(string singular, string plural)
        {
            Assert.Equal(singular, plural.Singularize());
        }

        [Theory,
           MemberData(nameof(SingularAndPluralWords))]
        public void PluralarizeTest(string singular, string plural)
        {
            Assert.Equal(plural, singular.Pluralize());
        }

        [Theory,
           MemberData(nameof(SystemTypes))]
        public void DontSingularizeDataTypesTest(string datatype)
        {
            Assert.Equal(datatype, datatype.Singularize());
        }

        public static IEnumerable<object[]> SingularAndPluralWords()
        {
            return new[]
            {
                new object[] { "search", "searches" },
                new object[] { "switch", "switches" },
                new object[] { "fix", "fixes" },
                new object[] { "box", "boxes" },
                new object[] { "process", "processes" },
                new object[] { "address", "addresses" },
                new object[] { "case", "cases" },
                new object[] { "stack", "stacks" },
                new object[] { "wish", "wishes" },
                new object[] { "fish", "fish" },
                new object[] { "category", "categories" },
                new object[] { "query", "queries" },
                new object[] { "ability", "abilities" },
                new object[] { "agency", "agencies" },
                new object[] { "movie", "movies" },
                new object[] { "archive", "archives" },
                new object[] { "index", "indices" },
                new object[] { "wife", "wives" },
                new object[] { "safe", "saves" },
                new object[] { "half", "halves" },
                new object[] { "move", "moves" },
                new object[] { "salesperson", "salespeople" },
                new object[] { "person", "people" },
                new object[] { "spokesman", "spokesmen" },
                new object[] { "man", "men" },
                new object[] { "woman", "women" },
                new object[] { "basis", "bases" },
                new object[] { "diagnosis", "diagnoses" },
                new object[] { "datum", "data" },
                new object[] { "medium", "media" },
                new object[] { "analysis", "analyses" },
                new object[] { "node_child", "node_children" },
                new object[] { "child", "children" },
                new object[] { "experience", "experiences" },
                new object[] { "day", "days" },
                new object[] { "comment", "comments" },
                new object[] { "foobar", "foobars" },
                new object[] { "newsletter", "newsletters" },
                new object[] { "old_news", "old_news" },
                new object[] { "news", "news" },
                new object[] { "series", "series" },
                new object[] { "species", "species" },
                new object[] { "quiz", "quizzes" },
                new object[] { "perspective", "perspectives" },
                new object[] { "ox", "oxen" },
                new object[] { "photo", "photos" },
                new object[] { "buffalo", "buffaloes" },
                new object[] { "tomato", "tomatoes" },
                new object[] { "dwarf", "dwarves" },
                new object[] { "elf", "elves" },
                new object[] { "information", "information" },
                new object[] { "equipment", "equipment" },
                new object[] { "bus", "buses" },
                new object[] { "status", "statuses" },
                new object[] { "status_code", "status_codes" },
                new object[] { "mouse", "mice" },
                new object[] { "louse", "lice" },
                new object[] { "house", "houses" },
                new object[] { "octopus", "octopi" },
                new object[] { "virus", "viri" },
                new object[] { "alias", "aliases" },
                new object[] { "portfolio", "portfolios" },
                new object[] { "vertex", "vertices" },
                new object[] { "matrix", "matrices" },
                new object[] { "axis", "axes" },
                new object[] { "testis", "testes" },
                new object[] { "crisis", "crises" },
                new object[] { "rice", "rice" },
                new object[] { "shoe", "shoes" },
                new object[] { "horse", "horses" },
                new object[] { "prize", "prizes" },
                new object[] { "edge", "edges" },
                new object[] { "goose", "geese" },
                new object[] { "deer", "deer" },
                new object[] { "sheep", "sheep" },
                new object[] { "wolf", "wolves" },
                new object[] { "volcano", "volcanoes" },
                new object[] { "aircraft", "aircraft" },
                new object[] { "alumna", "alumnae" },
                new object[] { "alumnus", "alumni" },
                new object[] { "fungus", "fungi" }
            };
        }

        public static IEnumerable<object[]> SystemTypes()
        {
            return new[]
            {
                new object[] { "bool" },
                new object[] { "byte" },
                new object[] { "sbyte" },
                new object[] { "byte[]" },
                new object[] { "char" },
                new object[] { "decimal" },
                new object[] { "double" },
                new object[] { "float" },
                new object[] { "int" },
                new object[] { "uint" },
                new object[] { "long" },
                new object[] { "ulong" },
                new object[] { "object" },
                new object[] { "object[]" },
                new object[] { "short" },
                new object[] { "ushort" },
                new object[] { "string" },
                new object[] { "System.Boolean" },
                new object[] { "System.Byte" },
                new object[] { "System.SByte" },
                new object[] { "System.Char" },
                new object[] { "System.Decimal" },
                new object[] { "System.Double" },
                new object[] { "System.Single" },
                new object[] { "System.Int32" },
                new object[] { "System.UInt32" },
                new object[] { "System.Int64" },
                new object[] { "System.UInt64" },
                new object[] { "System.Object" },
                new object[] { "System.Int16" },
                new object[] { "System.UInt16" },
                new object[] { "System.String" }
            };
        }
    }
}