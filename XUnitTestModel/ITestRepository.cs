using Xunit;


namespace XUnitTestModel
{
    public interface ITestRepository
    {
        [Fact]
        void AddTest();

        [Fact]
        void UpdateTest();

        [Fact]
        void ReturnAllTest();

        [Fact]
        void ReturnByIdTest();

    }
}
