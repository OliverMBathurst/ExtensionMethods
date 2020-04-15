using ExtensionMethods.EnumerableExtensionMethods;
using ExtensionMethods.StringExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionMethods.ExtensionMethodsTests
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        #region StringExtensionMethods 
        [TestMethod]
        public void IfAStringIsNotDistinct_DistinctShouldReturnFalse()
        {
            Assert.IsFalse("abcc".IsDistinct());
        }

        [TestMethod]
        public void IfAStringIsDistinct_DistinctShouldReturnTrue()
        {
            Assert.IsTrue("abc".IsDistinct());
        }

        [TestMethod]
        public void IfAStringCanBeALong_ToLongShouldReturnAValidLong()
        {
            var result = "45755452222222".ToLong();
            Assert.IsTrue(result != null && result == 45755452222222L);
        }

        [TestMethod]
        public void IfAStringCannotBeALong_ToLongShouldReturnANullableLong()
        {
            Assert.IsTrue("eeee".ToLong() == null);
        }

        [TestMethod]
        public void IfASubstringIsCalledWithAStartAndEndIndex_AValidSubstringShouldBeReturned()
        {
            var result = "teststring".SubString(0, 4);
            Assert.IsTrue(result != null && result == "tests");
        }

        [TestMethod]
        public void IfDefaultIfEmptyIsCalledWithAnEmptyString_ItShouldReturnTheDefaultString()
        {
            const string replacementValue = "replacement";
            Assert.IsTrue("".DefaultIfEmpty(replacementValue) == replacementValue);
        }

        [TestMethod]
        public void IfDefaultIfEmptyIsCalledWithANonEmptyString_ItShouldReturnTheString()
        {
            const string str = "str";
            Assert.IsTrue(str.DefaultIfEmpty("replacement") == str);
        }
        #endregion
    }
}
