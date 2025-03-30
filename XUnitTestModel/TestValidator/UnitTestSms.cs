using UbiModelShared.Validation;
using Xunit;
using System.Diagnostics.CodeAnalysis;


namespace XUnitTestModel.TestValidator
{
    public class UnitTestSms
    {
        [Fact]
        public void UnitTestSmsMaiorQue200()
        {
            var validator = new SMSMessageValidator<string>();
            var result = validator.Check("02582691976");
            Assert.False(result);
        }

        [Fact]
        public void UnitTestSmsMaiorQue200v1()
        {
            var validator = new SMSMessageValidator<string>();
            var result = validator.Check("1 1 1 1 1 1 1 1 1 1 1 1 1 1 11 1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11 1 1 1 1 1 1 1 1 1 1 1 1 1 1 11 ");
            Assert.True(result);
        }

        [Fact]
        public void UnitTestSmsMenorQue200()
        {
            var validator = new SMSMessageValidator<string>();
            var result = validator.Check("02582691976");
            Assert.True(result);

        }

        [Fact]
        public void UnitTestSmsMenorQue200v2()
        {
            var validator = new SMSMessageValidator<string>();
            var result = validator.Check("1 1 1 1 1 1 1 1 1 1 1 1 1 1 11 1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11  1 1 1 1 1 1 1 1 1 1 1 1 1 1 11 1 1 1 1 1 1 1 1 1 1 1 1 1 1 11 ");
            Assert.False(result);

        }
    }
  
}
