using Xunit;

namespace homework_1
{
    public class TransportTests
    {
        [Theory]
        [InlineData(5, "B")]
        [InlineData(5, "A")]
        [InlineData(5, "A", "B")]
        [InlineData(7, "A", "B", "B")]
        [InlineData(29, "A", "A", "B", "A", "B", "B", "A", "B")]
        [InlineData(29, "A", "A", "A", "A", "B", "B", "B", "B")]
        [InlineData(49, "B", "B", "B", "B", "A", "A", "A", "A")]
        public void Run_InlineData_TimeIsCorrect(int time, params string[] locations)
        {
            var sut = new Transport();

            var result = sut.Execute(locations);
            
            Assert.Equal(time, result);
        }
    }
}