using Xunit;
using UbiModelShared.Validation;

namespace XUnitTestModel.TestValidator
{
    public class UnitTestNameValidator
    {
        public UnitTestNameValidator()
        {
        }

        [Fact]
        public void TestValidName()
        {
            var validator = new NameValidator<string>();
            var result = validator.Check("Lucas Matos");
            Assert.True(result);
        }

        [Fact]
        public void TestValidName2()
        {
            var validator = new NameValidator<string>();
            var result = validator.Check("Fábio");
            Assert.True(result);
        }

        [Fact]
        public void TestValidName3()
        {
            var validator = new NameValidator<string>();
            var result = validator.Check("Fabio1");
            Assert.False(result);
        }

        [Fact]
        public void TestValidName4()
        {
            var validator = new NameValidator<string>();
            var result = validator.Check("FABIO");
            Assert.True(result);
        }

        [Fact]
        public void TestValidName5()
        {
            var validator = new NameValidator<string>();
            var result = validator.Check("fabio");
            Assert.True(result);
        }

        [Fact]
        public void TestValidName6()
        {
            var validator = new NameValidator<string>();
            var result = validator.Check("155a26");
            Assert.False(result);
        }

        [Fact]
        public void TestValidName7()
        {
            var validator = new NameValidator<string>();
            var result = validator.Check("FaBiO");
            Assert.True(result);
        }
    }
}
