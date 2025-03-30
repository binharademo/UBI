using System;
using System.Collections.Generic;
using System.Text;
using UbiModelShared.Validation;
using Xunit;
using System.Diagnostics.CodeAnalysis;

namespace XUnitTestModel.TestValidator
{
    [ExcludeFromCodeCoverage]
    public class UnitTestNotNull
    {

        [Fact]
        public void TestIsNotNull()
        {
            var validator = new IsNotNullOrEmptyRule<string>();
            var result = validator.Check("02582691976");
            Assert.True(result);
        }


        [Fact]
        public void TestIsNull()
        {
            var validator = new IsNotNullOrEmptyRule<string>();
            var result = validator.Check("");
            Assert.False(result);
        }


        [Fact]
        public void TestIsNull2()
        {
            var validator = new IsNullOrEmptyRule<string>();
            var result = validator.Check("");
            Assert.True(result);
        }

        [Fact]
        public void TestIsNull3()
        {
            var validator = new IsNullOrEmptyRule<string>();
            var result = validator.Check("sasas");
            Assert.False(result);
        }
    }
}
