using System;
using Xunit;
using System.Diagnostics.CodeAnalysis;
using UbiModelShared.Validation;

namespace XUnitTestModel.TestValidator
{

    [ExcludeFromCodeCoverage]
    public class UnitTestEmailValidator
    {
     
        public UnitTestEmailValidator()
        {

        }


        [Fact]
        public void TestValidEmail()
        {
            var validator = new EmailValidator<string>();
            var result = validator.Check("binhara@gmail.com");
            Assert.True(result);
        }


        [Fact]
        public void TestValidEmail1()
        {
            var validator = new EmailValidator<string>();
            var result = validator.Check("binhara@azuris.com.br");
            Assert.True(result);
        }

        [Fact]
        public void TestValidEmail3()
        {
            var validator = new EmailValidator<string>();
            var result = validator.Check("binhara@azuris.net");
            Assert.True(result);
        }

        [Fact]
        public void TestInvalidEmail()
        {
            var validator = new EmailValidator<string>();
            var result = validator.Check("12345678");
            Assert.False(result);
        }

        [Fact]
        public void TestInvalidEmail2()
        {
            var validator = new EmailValidator<string>();
            var result = validator.Check("binhara@azuris.comb.r.ar");
            Assert.False(result);
        }

        [Fact]
        public void TestInvalidEmail3()
        {
            var validator = new EmailValidator< string > ();
            var result = validator.Check("binhara@azuris");
            Assert.False(result);
        }

        [Fact]
        public void TestInvalidEmail4()
        {
            var validator = new EmailValidator<string>();
            var result = validator.Check("binhara @ azuris/s");
            Assert.False(result);
        }
    }
}
