using System;
using ExtensionMethods.StringExtensionMethods;
using ExtensionMethods.GenericExtensionMethods;
using ExtensionMethods.EnumerableExtensionMethods;
using ExtensionMethods.BooleanExtensionMethods;
using ExtensionMethods.ListExtensionMethods;
using ExtensionMethods.ArrayExtensionMethods;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ExtensionMethods.UnitTests
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
            Assert.IsTrue(result != null && result == "test");
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

        [TestMethod]
        public void IfRepeatIsCalledWithAnNOf2_ItShouldReturnARepeatedString()
        {
            const string str = "str";
            const string result = "strstr";
            Assert.IsTrue(str.Repeat(2) == result);
        }

        [TestMethod]
        public void IfToExceptionIsCalled_ItShouldReturnAnException()
        {
            const string str = "exception 123";
            Assert.AreEqual(str, str.ToException().Message);
        }

        [TestMethod]
        public void IfFormatIsCalledWithValidArguments_ItShouldReturnAValidString()
        {
            Assert.AreEqual("str 1 2 3", "str {0} {1} {2}".Format(1, 2, 3));
        }
        #endregion

        #region BooleanExtensionMethods
        [TestMethod]
        public void IfIsIsCalledWithAnIdenticalString_ItShouldReturnTrue()
        {
            const string str = "message";
            Assert.IsTrue(str.Is("message"));
        }

        [TestMethod]
        public void IfOrIsCalledWithTrueAndFalseBooleanObjects_ItShouldReturnTrue()
        {
            const bool a = 1 == 2;
            const bool b = 1 == 1;
            Assert.IsTrue(a.Or(b));
        }

        [TestMethod]
        public void IfOrIsCalledWithFalseAndFalseBooleanObjects_ItShouldReturnFalse()
        {
            const bool a = 1 == 2;
            const bool b = 1 == 3;
            Assert.IsFalse(a.Or(b));
        }

        [TestMethod]
        public void IfAndIsCalledWithTrueAndFalseBooleanObjects_ItShouldReturnFalse()
        {
            const bool a = 1 == 1;
            const bool b = 1 == 3;
            Assert.IsFalse(a.And(b));
        }

        [TestMethod]
        public void IfAndIsCalledWithTrueAndTrueBooleanObjects_ItShouldReturnTrue()
        {
            const bool a = 1 == 1;
            const bool b = 3 == 3;
            Assert.IsTrue(a.And(b));
        }
        #endregion

        #region GenericExtensionMethods
        [TestMethod]
        public void IfAnObjectIsOfTypeT_ItShouldCallTheActionWithTheObject()
        {
            var hasCalled = false;
            Action<TestType> action = (t) => { hasCalled = true; };
            new TestType().IfType(action);
            if (!hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfAnObjectIsNotOfTypeT_ItShouldNotCallTheActionWithTheObject()
        {
            var hasCalled = false;
            Action<TestType2> action = (t) => { hasCalled = true; };
            new TestType().IfType(action);
            if (hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfAnObjectIsNotOfTypeT_ItShouldNotCallTheAction()
        {
            var hasCalled = false;
            Action action = () => { hasCalled = true; };
            new TestType().IfType<TestType2>(action);
            if (hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfAnObjectIsOfTypeT_ItShouldCallTheAction()
        {
            var hasCalled = false;
            Action action = () => { hasCalled = true; };
            new TestType().IfType<TestType>(action);
            if (!hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfIfNotNullThenIsCalledWithANullObject_ItShouldThrowAnArgumentException()
        {
            var hasCalled = false;
            TestType testType = null;
            Action<TestType> action = (t) => { hasCalled = true; };
            testType.IfNotNull(action);
            if (hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfIfNotNullThenIsCalledWithANonNullObject_ItShouldCallTheAction()
        {
            var hasCalled = false;
            var testType = new TestType();
            Action<TestType> action = (t) => { hasCalled = true; };
            testType.IfNotNull(action);
            if (!hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfIsNullableIsCalledWithANullableObject_ItShouldReturnTrue()
        {
            var obj = new TestType();
            Assert.IsTrue(obj.IsNullable());
        }

        [TestMethod]
        public void IfIsNullableIsCalledWithANonNullableObject_ItShouldReturnFalse()
        {
            int obj = 1;
            Assert.IsFalse(obj.IsNullable());
        }

        [TestMethod]
        public void IfIsNullIsCalledWithANonNullObject_ItShouldReturnFalse()
        {
            var obj = new TestType();
            Assert.IsFalse(obj.IsNull());
        }

        [TestMethod]
        public void IfIsNullIsCalledWithANullObject_ItShouldReturnTrue()
        {
            TestType obj = null;
            Assert.IsTrue(obj.IsNull());
        }

        [TestMethod]
        public void IfIsInIsCalledWithACollectionItIsIn_ItShouldReturnTrue()
        {
            var obj = new TestType();
            var list = new List<TestType>() { obj };            
            Assert.IsTrue(obj.IsIn(list));
        }

        [TestMethod]
        public void IfIsInIsCalledWithACollectionItIsNotIn_ItShouldReturnFalse()
        {
            var obj = new TestType();
            var list = new List<TestType>();
            Assert.IsFalse(obj.IsIn(list));
        }

        [TestMethod]
        public void IfIsInIsCalledWithAnArrayItIsIn_ItShouldReturnTrue()
        {
            var obj = new TestType();
            var obj2 = new TestType();
            Assert.IsTrue(obj.IsIn(obj, obj2));
        }

        [TestMethod]
        public void IfIsInIsCalledWithAnArrayItIsNotIn_ItShouldReturnFalse()
        {
            var obj = new TestType();
            var obj2 = new TestType();
            Assert.IsFalse(obj.IsIn(obj2));
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void IfThrowIfIsCalledWithAFunctionThatReturnsTrue_ItShouldThrowException()
        {
            var obj = new TestType
            {
                TestProperty = true
            };
            obj.ThrowIf(x => x.TestProperty);
        }

        [TestMethod]
        public void IfThrowIfIsCalledWithAFunctionThatReturnsFalse_ItShouldNotThrowException()
        {
            var obj = new TestType
            {
                TestProperty = true
            };
            obj.ThrowIf(x => !x.TestProperty);
        }

        [TestMethod]
        public void IfThrowIfIsCalledWithAFunctionThatReturnsTrue_ItShouldThrowTheGivenException()
        {
            var passed = false;
            var e = new ArgumentException();

            var obj = new TestType
            {
                TestProperty = true
            };

            try
            {
                obj.ThrowIf(x => x.TestProperty, e);
            }
            catch (ArgumentException) { passed = true; }

            if (!passed)
                Assert.Fail();
        }

        [TestMethod]
        public void IfThrowIfIsCalledWithAFunctionThatReturnsFalse_ItShouldNotThrowTheGivenException()
        {
            var passed = true;
            var e = new ArgumentException();

            var obj = new TestType
            {
                TestProperty = true
            };

            try
            {
                obj.ThrowIf(x => !x.TestProperty, e);
            }
            catch (ArgumentException) { passed = false; }

            if (!passed)
                Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void IfThrowIfEqualsIsCalledWithASameObject_ItShouldThrowAnException()
        {
            const bool a = true;
            const bool b = true;

            a.ThrowIfEquals(b);
        }

        [TestMethod]
        public void IfThrowIfEqualsIsCalledWithADifferentObject_ItShouldNotThrowAnException()
        {
            const bool a = true;
            const bool b = false;

            a.ThrowIfEquals(b);
        }

        [TestMethod]
        public void IfThrowIfEqualsIsCalledWithADifferentObject_ItShouldNotThrowTheGivenException()
        {
            var passed = true;
            const bool a = true;
            const bool b = false;
            var e = new ArgumentException();

            try
            {
                a.ThrowIfEquals(b, e);
            }
            catch (ArgumentException) { passed = false; }

            if (!passed)
                Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void IfThrowIfEqualsIsCalledWithASameObject_ItShouldThrowTheGivenException()
        {
            const bool a = true;
            const bool b = true;
            a.ThrowIfEquals(b, new ArgumentException());
        }

        [TestMethod]
        public void IfDeepCloneIsCalledWithAnObject_ItShouldReturnAClonedObject()
        {
            var a = new TestType();
            var result = a.DeepClone();
            Assert.IsFalse(ReferenceEquals(a, result));
        }

        [TestMethod]
        public void IfParseIsCalledWithAnObjectThatCanBeParsed_ItShouldReturnAParsedObject()
        {
            const string str = "123";
            var integer = str.Parse<string, int>();
            Assert.AreEqual(integer, 123);
        }

        [TestMethod]
        public void IfParseIsCalledWithAnObjectThatCannotBeParsed_ItShouldThrowAnException()
        {
            var passed = false;
            try
            {
                "1q2d3".Parse<string, int>();
            }
            catch(Exception ex)
            {
                if(ex.InnerException.GetType() == typeof(FormatException))
                {
                    passed = true;
                }
            }
            if (!passed)
                Assert.Fail();
        }

        [TestMethod]
        public void IfTryDisposeIsCalledOnADisposableObject_ItShouldNotThrowAnException()
        {
            var disposable = new DisposableTestType();
            disposable.TryDispose();
            Assert.IsTrue(disposable.HasBeenDisposed);
        }

        [TestMethod]
        public void IfNullIfDefaultIsCalledOnADefaultObject_ItShouldReturnANullObject()
        {
            Assert.AreEqual(null, default(TestType).NullIfDefault());
        }

        [TestMethod]
        public void IfNullIfDefaultIsCalledOnANonDefaultObject_ItShouldReturnTheObject()
        {
            var test = new TestType { TestProperty = true };
            Assert.AreEqual(test, test.NullIfDefault());
        }

        [TestMethod]
        public void IfIsDefaultIsCalledOnANonDefaultObject_ItShouldReturnFalse()
        {
            var test = new TestType { TestProperty = true };
            Assert.IsFalse(test.IsDefault());
        }

        [TestMethod]
        public void IfIsDefaultIsCalledOnADefaultObject_ItShouldReturnTrue()
        {
            Assert.IsTrue(default(TestType).IsDefault());
        }

        [TestMethod]
        public void IfBoxIsCalledOnAnObject_ItShouldReturnTheGenericObjectEquivalent()
        {
            var testType = new TestType();
            var result = testType.Box();

            if(!(result is TestType))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void IfUnboxIsCalledOnAnObject_ItShouldReturnAnObjectOfTheOriginalType()
        {
            var testType = new TestType {
                TestProperty = true
            };

            var boxed = testType.Box();
            var unboxed = boxed.Unbox<TestType>();

            if (!unboxed.TestProperty)
            {
                Assert.Fail();
            }
        }
        #endregion

        #region EnumExtensionMethods
        [TestMethod]
        public void WhenToDictionaryIsCalledWithAnEnumType_ItShouldReturnADictionaryOfAllEnumValues()
        {
            var result = EnumExtensionMethods.EnumExtensionMethods.ToDictionary<TestEnum>();
            Assert.IsTrue(result[0] == TestEnum.EnumValue1);
            Assert.IsTrue(result[1] == TestEnum.EnumValue2);
        }

        [TestMethod]
        public void WhenToDictionaryIsCalledWithANonEnumType_ItShouldReturnAnEmptyDictionary()
        {
            Assert.IsTrue(EnumExtensionMethods.EnumExtensionMethods.ToDictionary<int>().IsEmpty());
        }
        #endregion

        #region ListExtensionMethods
        [TestMethod]
        public void IfIsDistinctIsCalledWithAListOfOneElement_ItShouldReturnTrue()
        {
            Assert.IsTrue(new List<int> { 5 }.IsDistinct());
        }

        [TestMethod]
        public void IfIsDistinctIsCalledWithAListOfZeroElements_ItShouldReturnTrue()
        {
            Assert.IsTrue(new List<int> { }.IsDistinct());
        }

        [TestMethod]
        public void IfIsDistinctIsCalledWithAListOfSameElements_ItShouldReturnFalse()
        {
            Assert.IsFalse(new List<int> { 5, 5, 5, 5, 5 }.IsDistinct());
        }

        [TestMethod]
        public void IfIsDistinctIsCalledWithAListOfDifferentElements_ItShouldReturnTrue()
        {
            Assert.IsTrue(new List<int> { 5, 6, 7, 8, 9 }.IsDistinct());
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElementLargerThanAllElements_ItShouldAddTheElementAtTheRightPositionOfTheSortedList()
        {
            var list = new List<int> { 5, 6, 7, 8, 9 };
            list.InsertSorted(10);
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(10, list.ElementAt(5));
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElement_ItShouldAddTheElementAtTheRightPositionOfTheSortedList()
        {
            var list = new List<int> { 5, 6, 7, 9, 10 };
            list.InsertSorted(8);            
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(8, list.ElementAt(3));
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElementAndAComparator_ItShouldAddTheElementAtTheRightPositionOfTheSortedList()
        {
            var list = new List<int> { 5, 6, 7, 9, 10 };
            list.InsertSorted(8, new Comparison<int>((x, y) => { return x.CompareTo(y); }));
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(8, list.ElementAt(3));
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElementAndAComparator_ItShouldAddTheElementAtTheRightPositionOfTheEmptyList()
        {
            var list = new List<int>();
            list.InsertSorted(8, new Comparison<int>((x, y) => { return x.CompareTo(y); }));
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(8, list.First());
        }

        [TestMethod]
        public void IfInsertWhereIsCalledWithAnElementAndAPredicate_ItShouldInsertTheElementAtTheRightPositionOfTheList()
        {
            IList<int> list = new List<int> { 5, 5, 6, 7, 8, 9 };
            list = list.InsertWhere(8, x => x == 5);
            Assert.AreEqual(7, list.Count);
            Assert.AreEqual(8, list.First());
        }

        [TestMethod]
        public void IfInsertWhereIsCalledWithAnElementAndAPredicateAndMultipleInsertsIsTrue_ItShouldInsertTheElementAtTheRightPositionsOfTheList()
        {
            IList<int> list = new List<int> { 5, 5, 6, 7, 8, 9 };
            var result = list.InsertWhere(8, x => x == 5, true);
            Assert.IsTrue(result.SequenceEqual(new List<int> { 8, 5, 8, 5, 6, 7, 8, 9}));
        }
        #endregion

        #region ArrayExtensionMethods
        [TestMethod]
        public void IfLeftRotateIsCalledOnAnArray_ItShouldLeftRotateTheArray()
        {
            var arr = new int[] { 1, 2, 3, 4, 5 };
            arr.LeftRotate();
            Assert.IsTrue(arr.SequenceEqual(new int[] { 2, 3, 4, 5, 1 }));
        }

        [TestMethod]
        public void IfLeftRotateIsCalledOnAnArrayWithTwoElements_ItShouldLeftRotateTheArray()
        {
            var arr = new int[] { 2, 3 };
            arr.LeftRotate();
            Assert.IsTrue(arr.SequenceEqual(new int[] { 3, 2 }));
        }

        [TestMethod]
        public void IfLeftRotateIsCalledOnAnArrayWithOneElement_ItShouldLeftRotateTheArray()
        {
            var arr = new int[] { 3 };
            arr.LeftRotate();
            Assert.IsTrue(arr.SequenceEqual(new int[] { 3 }));
        }

        [TestMethod]
        public void IfRightRotateIsCalledOnAnArray_ItShouldRightRotateTheArray()
        {
            var arr = new int[] { 1, 2, 3, 4, 5 };
            arr.RightRotate();
            Assert.IsTrue(arr.SequenceEqual(new int[] { 5, 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void IfRightRotateIsCalledOnAnArrayWithTwoElements_ItShouldRightRotateTheArray()
        {
            var arr = new int[] { 2, 3 };
            arr.RightRotate();
            Assert.IsTrue(arr.SequenceEqual(new int[] { 3, 2 }));
        }

        [TestMethod]
        public void IfRightRotateIsCalledOnAnArrayWithOneElement_ItShouldRightRotateTheArray()
        {
            var arr = new int[] { 3 };
            arr.RightRotate();
            Assert.IsTrue(arr.SequenceEqual(new int[] { 3 }));
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElementLargerThanAllElements_ItShouldAddTheElementAtTheRightPositionOfTheSortedArray()
        {
            var array = new int[] { 5, 6, 7, 8, 9 };
            array = array.InsertSorted(10);
            Assert.AreEqual(6, array.Length);
            Assert.AreEqual(10, array[5]);
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElement_ItShouldAddTheElementAtTheRightPositionOfTheSortedArray()
        {
            var array = new int[] { 5, 6, 7, 9, 10 };
            array = array.InsertSorted(8);
            Assert.AreEqual(6, array.Length);
            Assert.AreEqual(8, array[3]);
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElement_ItShouldAddTheElementAtTheRightPositionOfTheEmptyArray()
        {
            var array = new int[] { }.InsertSorted(8);
            Assert.AreEqual(1, array.Length);
            Assert.AreEqual(8, array[0]);
        }
        #endregion

        #region EnumerableExtensionMethods
        #endregion

        private enum TestEnum
        {
            EnumValue1,
            EnumValue2 
        }

        private enum BlankEnum { }

        private class TestType
        {
            public TestType() { }

            public bool TestProperty { get; set; } = false;
        }

        private class TestType2
        {
            public TestType2() { }
        }

        private class DisposableTestType : IDisposable
        {
            public void Dispose() => HasBeenDisposed = true;
            public bool HasBeenDisposed { get; set; } = false;
        }
    }
}