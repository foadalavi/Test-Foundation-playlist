namespace DummyLibrary.Test
{
    public class ObjecrComparerTest
    {

        [Theory]
        [MemberData(nameof(DataSource))]
        public void OnCompare_WhenPassTwoObjects_ShouldReturnTheExpectedValue(object object1, object object2, bool expectedResult)
        {
            var compare = new ObjectComparer();
            var actualResult = compare.Compare(object1, object2);
            actualResult.Should().Be(expectedResult);
        }


        public static IEnumerable<object[]> DataSource()
        {
            var object1 = new { FirstName = "Foad", LastName = "Alavi" };
            var object2 = object1;
            return new List<object[]>()
            {
                new object[] {object1,object2,true},
            };
        }
    }
}
