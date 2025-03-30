using Xunit;
using UbiModelShared.Validation;

namespace XUnitTestModel.TestValidator
{
    public class UnitTestCpfValidator
    {
        public UnitTestCpfValidator()
        {

        }
        [Fact]
        public void TestValidCpf()
        {
            var validator = new CpfValidator<string>();
            var result = validator.Check("02582691976");
            Assert.True(result);
        }

        [Fact]
        public void TestInvalidCpf()
        {
            var validator = new CpfValidator<string>();
            var result = validator.Check("12345678");
            Assert.False(result);

        }

        [Fact]
        public void TestInvalidCpfWithStrings()
        {
            var validator = new CpfValidator<string>();
            var result = validator.Check("asedfrgthyu");
            Assert.False(result);

        }
    }
}
