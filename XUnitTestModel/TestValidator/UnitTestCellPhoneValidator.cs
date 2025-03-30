using Xunit;
using UbiModelShared.Validation;

namespace XUnitTestModel.TestValidator
{
    public class UnitTestCellPhoneValidator
    {
        public UnitTestCellPhoneValidator()
        {
        }

        [Fact]
        public void TestValidCellphoneOK()
        {
            var validator = new CellphoneValidator<string>();
            var result = validator.Check("41998003687");
            Assert.True(result);
        }

        [Fact]
        public void TestValidCellphoneWithMask()
        {
            var validator = new CellphoneValidator<string>();
            var result = validator.Check("(41) 9 9800 3687");
            Assert.True(result);
        }

        [Fact]
        public void TestInvalidCellphone()
        {
            var validator = new CellphoneValidator<string>();
            var result = validator.Check("1234@#1@#5678");
            Assert.True(result);
        }

        [Fact]
        public void TestInvalidCellphone1()
        {
            var validator = new CellphoneValidator<string>();
            var result = validator.Check("123456789    ");
            Assert.True(result);
        }

        [Fact]
        public void TestInvalidCellphone2()
        {
            var validator = new CellphoneValidator<string>();
            var result = validator.Check("0123456789*/-*8*/");
            Assert.False(result);
        }

        [Fact]
        public void TestInalidCellphone3()
        {
            var validator = new CellphoneValidator<string>();
            var result = validator.Check("12345671234123123123123123");
            Assert.False(result);
        }
    }
}
