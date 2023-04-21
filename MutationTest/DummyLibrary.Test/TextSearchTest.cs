using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyLibrary.Test
{
    public class TextSearchTest
    {

        [Fact]
        public void OnSearch_WhenPassAString_ShouldReturnTheExpectedValue()
        {
            var search = new TextSearch();
            var expectedResult = new List<string>() 
            { 
                "06-34491867" 
            };
            var actualResult = search.GetPhoneNumber("This is a dummy phone number.\r\n06-344918679");
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
