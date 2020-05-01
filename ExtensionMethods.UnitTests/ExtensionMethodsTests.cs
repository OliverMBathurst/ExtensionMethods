using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtensionMethods.StringExtensionMethods;
using ExtensionMethods.GenericExtensionMethods;
using ExtensionMethods.EnumerableExtensionMethods;
using ExtensionMethods.BooleanExtensionMethods;
using ExtensionMethods.ListExtensionMethods;
using ExtensionMethods.ArrayExtensionMethods;
using ExtensionMethods.RandomExtensionMethods;
using ExtensionMethods.IntegerExtensionMethods;
using ExtensionMethods.DictionaryExtensionMethods;

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
            Action<TestType> action = (t) => hasCalled = true;
            new TestType().IfType(action);
            if (!hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfAnObjectIsNotOfTypeT_ItShouldNotCallTheActionWithTheObject()
        {
            var hasCalled = false;
            Action<TestType2> action = (t) => hasCalled = true;
            new TestType().IfType(action);
            if (hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfAnObjectIsNotOfTypeT_ItShouldNotCallTheAction()
        {
            var hasCalled = false;
            Action action = () => hasCalled = true;
            new TestType().IfType<TestType2>(action);
            if (hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfAnObjectIsOfTypeT_ItShouldCallTheAction()
        {
            var hasCalled = false;
            Action action = () => hasCalled = true;
            new TestType().IfType<TestType>(action);
            if (!hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfIfNotNullThenIsCalledWithANullObject_ItShouldThrowAnArgumentException()
        {
            var hasCalled = false;
            TestType testType = null;
            Action<TestType> action = (t) => hasCalled = true;
            testType.IfNotNull(action);
            if (hasCalled)
                Assert.Fail();
        }

        [TestMethod]
        public void IfIfNotNullThenIsCalledWithANonNullObject_ItShouldCallTheAction()
        {
            var hasCalled = false;
            var testType = new TestType();
            Action<TestType> action = (t) => hasCalled = true;
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
        public void IfIsNullableIsCalledWithANullType_ItShouldReturnTrue()
        {
            Type type = null;
            Assert.IsTrue(type.IsNullable());
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
        public void IfDeepCloneIsCalledWithANonSerializableObject_ItShouldReturnTheDefaultObject()
        {
            var a = new TestTypeNonSerializable();
            var result = a.DeepClone();
            Assert.AreEqual(default, result);
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
        public void IfTryDisposeIsCalledOnANonDisposableObject_ItShouldNotThrowAnException()
        {
            new int().TryDispose();
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

        [TestMethod]
        public void IfRepeatIsCalledNTimes_ItShouldPerformTheActionNTimes()
        {
            var obj = new CountReferenceClass();
            obj.Repeat((x) => x.Increment(), 5);
            Assert.AreEqual(5, obj.Count);
        }

        [TestMethod]
        public void IfRepeatIsCalled0Times_ItShouldPerformTheAction0Times()
        {
            var obj = new CountReferenceClass();
            obj.Repeat((x) => x.Increment(), 0);
            Assert.AreEqual(0, obj.Count);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfRepeatIsCalledWithANullObject_ItShouldThrowAnException()
        {
            CountReferenceClass obj = null;
            obj.Repeat((x) => x.Increment(), 0);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfGetBytesIsCalledWithANullObject_ItShouldThrowAnException()
        {
            TestType obj = null;
            obj.GetBytes();
        }

        [TestMethod]
        public void IfGetBytesIsCalledWithAValidObject_ItShouldReturnTheCorrectBytes()
        {
            var @string = "test123";
            var result = @string.GetBytes();
            Assert.AreEqual(31, result.Length);
            Assert.IsTrue(result.Is<byte>(0, 1, 0, 0, 0, 255, 255, 255, 255, 1, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 0, 0, 7, 116, 101, 115, 116, 49, 50, 51, 11));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfToMD5HashIsCalledWithANullObject_ItShouldThrowAnException()
        {
            TestType obj = null;
            obj.ToMD5Hash();
        }

        [TestMethod]
        public void IfToMD5HashIsCalledWithANonNullObject_ItShouldReturnTheMD5Hash()
        {
            Assert.AreEqual("16-E5-F3-3C-2B-BB-D2-80-51-E9-EF-B4-0C-1A-72-A3", "test string".ToMD5Hash());
        }

        #endregion

        #region EnumExtensionMethods
        [TestMethod]
        public void WhenToDictionaryIsCalledWithAnEnumType_ItShouldReturnADictionaryOfAllEnumValues()
        {
            var result = EnumExtensionMethods.EnumExtensionMethods.ToDictionary<TestEnum>();
            Assert.IsTrue(result["EnumValue1"] == TestEnum.EnumValue1);
            Assert.IsTrue(result["EnumValue2"] == TestEnum.EnumValue2);
        }

        [TestMethod]
        public void WhenToDictionaryIsCalledWithANonEnumType_ItShouldReturnAnEmptyDictionary()
        {
            Assert.IsTrue(EnumExtensionMethods.EnumExtensionMethods.ToDictionary<int>().IsEmpty());
        }
        #endregion

        #region ListExtensionMethods
        [TestMethod]
        public void IfForIsCalledWithAnActionOfT_ItShouldCallThatActionForEveryElementOfTheList()
        {
            var list = new List<int>();
            new List<int> { 1, 2, 3, 4, 5 }.For((element) => list.Add(element));
            Assert.IsTrue(list.Is(0, 1, 2, 3, 4));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfInt_ItShouldCallThatActionForEveryIndexOfTheList()
        {
            var list = new List<int>();
            new List<int> { 1, 2, 3, 4, 5 }.For((i) => list.Add(i));
            Assert.IsTrue(list.Is(0, 1, 2, 3, 4));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTAndInt_ItShouldCallThatActionForEveryElementOfTheList()
        {
            var dict = new Dictionary<int, int>();
            new List<int> { 1, 2, 3, 4, 5 }.For((element, index) => dict.Add(index, element));
            Assert.IsTrue(dict.Is((0, 1), (1, 2), (2, 3), (3, 4), (4, 5)));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTArrayAndInt_ItShouldCallThatActionForEveryElementOfTheList()
        {
            var list = new List<int>();
            new List<int> { 1, 2, 3, 4, 5 }.For((l, i) => list.Add(l.Count + i));
            Assert.IsTrue(list.Is(5, 6, 7, 8, 9));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfT_ItShouldCallThatActionForEveryElementOfTheArray_AndReturnTheList()
        {
            var list = new List<TestType>();
            var returned = new List<TestType> { new TestType { TestProperty = true } }.ForAndReturn((element) => list.Add(element));
            Assert.IsTrue(list.First().TestProperty);
            Assert.IsTrue(returned.First().TestProperty);
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfInt_ItShouldCallThatActionForEveryIndexOfTheArray_AndReturnTheList()
        {
            var list = new List<int>();
            var returned = new List<int> { 1, 2, 3, 4, 5 }.ForAndReturn((element) => list.Add(element));
            Assert.IsTrue(list.Is(0, 1, 2, 3, 4));
            Assert.IsTrue(returned.Is(1, 2, 3, 4, 5));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTAndInt_ItShouldCallThatActionForEveryElementOfTheArray_AndReturnTheList()
        {
            var list = new List<(int index, TestType value)>();
            var returned = new List<TestType> { new TestType { TestProperty = true } }
                .ForAndReturn((element, index) => list.Add((index, element)));
            Assert.IsTrue(list.First().index == 0);
            Assert.IsTrue(list.First().value.TestProperty);
            Assert.IsTrue(returned.First().TestProperty);
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTArrayAndInt_ItShouldCallThatActionForEveryElementOfTheArray_AndReturnTheList()
        {
            var list = new List<int>();
            var returned = new List<int> { 1, 2, 3, 4, 5 }
                .ForAndReturn((arr, i) => {
                    if (arr.Is(1, 2, 3, 4, 5))
                        list.Add(i);
                });
            Assert.IsTrue(list.Is(0, 1, 2, 3, 4));
            Assert.IsTrue(returned.Is(1, 2, 3, 4, 5));
        }

        [TestMethod]
        public void IfIsDistinctIsCalledWithAListOfOneElement_ItShouldReturnTrue()
        {
            Assert.IsTrue(new List<int> { 5 }.IsDistinct());
        }

        [TestMethod]
        public void IfIsDistinctIsCalledWithAListOfZeroElements_ItShouldReturnTrue()
        {
            Assert.IsTrue(new List<int>().IsDistinct());
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
        public void IfInsertSortedIsCalledWithAnElementAndAComparator_ItShouldAddTheElementAtTheRightPositionOfTheSortedList()
        {
            var list = new List<int> { 5, 6, 7, 9, 10 };
            list.InsertSorted(8, new Comparison<int>((x, y) => x.CompareTo(y)));
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(8, list.ElementAt(3));
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElementAndAComparator_ItShouldAddTheElementAtTheRightPositionOfTheEmptyList()
        {
            var list = new List<int>();
            list.InsertSorted(8, new Comparison<int>((x, y) => x.CompareTo(y)));
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
            var list = new List<int> { 5, 5, 6, 7, 8, 9 };
            var result = list.InsertWhere(8, x => x == 5, true);
            Assert.IsTrue(result.SequenceEqual(new List<int> { 8, 5, 8, 5, 6, 7, 8, 9}));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertWhereIsCalledWithANullList_ItShouldThrowAnException()
        {
            List<int> list = null;
            list.InsertWhere(8, x => x == 5, true);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertWhereIsCalledWithANullElement_ItShouldThrowAnException()
        {
            new List<TestType>().InsertWhere(null, x => x.TestProperty, true);
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAComparison_ItShouldInsertAccordingly()
        {
            var list = new List<TestType> { new TestType { TestProperty = true } };
            list.InsertSorted(new TestType(), new Comparison<TestType>((x, y) => x.CompareTo(y)));
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list[1].TestProperty);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertSortedIsCalledWithANullItem_ItShouldThrowAnException()
        {
            var list = new List<TestType>();
            list.InsertSorted(null, new Comparison<TestType>((x, y) => x.CompareTo(y)));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertSortedIsCalledWithANullList_ItShouldThrowAnException()
        {
            List<TestType> list = null;
            list.InsertSorted(new TestType(), new Comparison<TestType>((x, y) => x.CompareTo(y)));
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAComparer_ItShouldInsertAccordingly()
        {
            var list = new List<TestType> { new TestType { TestProperty = true } };
            list.InsertSorted(new TestType(), new ComparerClass<TestType>());
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list[1].TestProperty);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertSortedIsCalledWithANullItemAndAComparer_ItShouldThrowAnException()
        {
            var list = new List<TestType>();
            list.InsertSorted(null, new ComparerClass<TestType>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertSortedIsCalledWithANullListAndAComparer_ItShouldThrowAnException()
        {
            List<TestType> list = null;
            list.InsertSorted(new TestType(), new ComparerClass<TestType>());
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAValidComparerAndANumberLargerThanTheRestInTheCollection_ItShouldAddTheNumberToTheEndOfTheCollection()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6 };
            list.InsertSorted(10, new ComparerClass<int>());
            Assert.AreEqual(7, list.Count);
            Assert.AreEqual(10, list.Last());
        }

        [TestMethod]
        public void IfAddNIsCalledWithANumberN_ItShouldAddNElementsToTheList()
        {
            var list = new List<TestType>();
            list.AddN(3);
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void IfAddNIsCalledWithANumberNOf0_ItShouldAdd0ElementsToTheList()
        {
            var list = new List<TestType>();
            list.AddN(0);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertSortedIsCalledOnANullList_ItShouldThrowAnArgumentNullException()
        {
            List<int> list = null;
            list.InsertSorted(1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertSortedIsCalledOnAListWithANullArgument_ItShouldThrowAnArgumentNullException()
        {
            new List<TestType>().InsertSorted(null);
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElementLargerThanAllElements_ItShouldAddTheElementAtTheRightPositionOfTheSortedList()
        {
            var list = new List<int> { 5, 6, 7, 8, 9 };
            list.InsertSorted(10);
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(10, list[5]);
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElement_ItShouldAddTheElementAtTheRightPositionOfTheSortedList()
        {
            var list = new List<int> { 5, 6, 7, 9, 10 };
            list.InsertSorted(8);
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(8, list[3]);
        }

        [TestMethod]
        public void IfInsertSortedIsCalledWithAnElement_ItShouldAddTheElementAtTheRightPositionOfTheEmptyList()
        {
            var list = new List<int>();
            list.InsertSorted(8);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(8, list[0]);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertSortedIsCalledWithANullElement_ItShouldThrowAnException()
        {
            var list = new List<TestType>();
            list.InsertSorted(null);
        }

        [TestMethod]
        public void IfFillIsCalledOnAList_ItShouldFillTheListWithTheProvidedObject()
        {
            var list = new List<int> { 1, 54, 76, 23, 99 };
            list.Fill(1);
            var list2 = new List<int>();
            list2.Fill(1);
            Assert.IsTrue(list.Is(1, 1, 1, 1, 1));
            Assert.IsTrue(list2.Is());
        }

        [TestMethod]
        public void IfChainableFillIsCalledOnAList_ItShouldFillTheListWithTheProvidedObjectAndReturnTheList()
        {
            var list = new List<int> { 1, 54, 76, 23, 99 }.ChainableFill(1);
            var list2 = new List<int>().ChainableFill(1);
            Assert.IsTrue(list.Is(1, 1, 1, 1, 1));
            Assert.IsTrue(list2.Is());
        }
        #endregion

        #region ArrayExtensionMethods
        [TestMethod]
        public void IfForIsCalledWithAnActionOfT_ItShouldCallThatActionForEveryElementOfTheArray()
        {
            var list = new List<int>();
            new int[] { 1, 2, 3, 4, 5 }.For((element) => list.Add(element));
            Assert.IsTrue(list.Is(0, 1, 2, 3, 4));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfInt_ItShouldCallThatActionForEveryIndexOfTheArray()
        {
            var list = new List<int>();
            new int[] { 1, 2, 3, 4, 5 }.For((i) => list.Add(i));
            Assert.IsTrue(list.Is(0, 1, 2, 3, 4));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTAndInt_ItShouldCallThatActionForEveryElementOfTheArray()
        {
            var dict = new Dictionary<int, int>();
            new int[] { 1, 2, 3, 4, 5 }.For((element, index) => dict.Add(index, element));
            Assert.IsTrue(dict.Is((0, 1), (1, 2), (2, 3), (3, 4), (4, 5)));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTArrayAndInt_ItShouldCallThatActionForEveryElementOfTheArray()
        {
            var list = new List<int>();
            new int[] { 1, 2, 3, 4, 5 }.For((arr, i) => list.Add(arr.Length + i));
            Assert.IsTrue(list.Is(5, 6, 7, 8, 9));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfT_ItShouldCallThatActionForEveryElementOfTheArray_AndReturnTheArray()
        {
            var list = new List<TestType>();
            var returned = new TestType[] { new TestType { TestProperty = true } }.ForAndReturn((element) => list.Add(element));
            Assert.IsTrue(list.First().TestProperty);
            Assert.IsTrue(returned.First().TestProperty);
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfInt_ItShouldCallThatActionForEveryIndexOfTheArray_AndReturnTheArray()
        {
            var list = new List<int>();
            var returned = new int[] { 1, 2, 3, 4, 5 }.ForAndReturn((element) => list.Add(element));
            Assert.IsTrue(list.Is(0, 1, 2, 3, 4));
            Assert.IsTrue(returned.Is(1, 2, 3, 4, 5));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTAndInt_ItShouldCallThatActionForEveryElementOfTheArray_AndReturnTheArray()
        {
            var list = new List<(int index, TestType value)>();
            var returned = new TestType[] { new TestType { TestProperty = true } }
                .ForAndReturn((element, index) => list.Add((index, element)));
            Assert.IsTrue(list.First().index == 0);
            Assert.IsTrue(list.First().value.TestProperty);
            Assert.IsTrue(returned.First().TestProperty);
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTArrayAndInt_ItShouldCallThatActionForEveryElementOfTheArray_AndReturnTheArray()
        {
            var list = new List<int>();
            var returned = new int[] { 1, 2, 3, 4, 5 }
                .ForAndReturn((arr, i) => {
                    if (arr.Is(1, 2, 3, 4, 5))
                        list.Add(i);
                });
            Assert.IsTrue(list.Is(0, 1, 2, 3, 4));
            Assert.IsTrue(returned.Is(1, 2, 3, 4, 5));
        }

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
        public void IfAllTheSameIsCalledWithAnArrayOfSameElements_ItShouldReturnTrue()
        {
            Assert.IsTrue(new int[] { 1, 1, 1 }.AreAllTheSame());
        }

        [TestMethod]
        public void IfAllTheSameIsCalledWithAnArrayOfDifferentElements_ItShouldReturnFalse()
        {
            Assert.IsFalse(new int[] { 1, 2, 3 }.AreAllTheSame());
        }

        [TestMethod]
        public void IfAllTheSameIsCalledOnAnEmptyArray_ItShouldReturnTrue()
        {
            Assert.IsTrue(new int[] { }.AreAllTheSame());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfLeftRotateIsCalledOnANullArray_ItShouldThrowAnArgumentNullException()
        {
            int[] arr = null;
            arr.LeftRotate();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfRightRotateIsCalledOnANullArray_ItShouldThrowAnArgumentNullException()
        {
            int[] arr = null;
            arr.RightRotate();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfAreAllTheSameIsCalledOnANullArray_ItShouldThrowAnArgumentNullException()
        {
            int[] arr = null;
            arr.AreAllTheSame();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertSortedIsCalledOnANullArray_ItShouldThrowAnArgumentNullException()
        {
            int[] arr = null;
            arr.InsertSorted(1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfInsertSortedIsCalledOnAnArrayWithANullArgument_ItShouldThrowAnArgumentNullException()
        {
            new TestType[] { }.InsertSorted(null);
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

        [TestMethod]
        public void IfFillIsCalledOnAnArray_ItShouldFillTheArrayWithTheProvidedObject()
        {
            var array = new int[] { 1, 54, 76, 23, 99 };
            array.Fill(1);
            var array2 = new int[0];
            array2.Fill(1);
            Assert.IsTrue(array.Is(1, 1, 1, 1, 1));
            Assert.IsTrue(array2.Is());
        }

        [TestMethod]
        public void IfChainableFillIsCalledOnAnArray_ItShouldFillTheArrayWithTheProvidedObjectAndReturnTheArray()
        {
            var array = new int[] { 1, 54, 76, 23, 99 }.ChainableFill(1);
            var array2 = new int[0].ChainableFill(1);
            Assert.IsTrue(array.Is(1, 1, 1, 1, 1));
            Assert.IsTrue(array2.Is());
        }
        #endregion

        #region RandomExtensionMethods
        [TestMethod]
        public void IfGetRandomElementOfIsCalledWithParams_ItShouldReturnARandomElement()
        {
            var result = new Random().RandomElementOf(1, 2, 3);
            Assert.IsTrue(result.IsIn(1, 2, 3));
        }
        #endregion

        #region IntegerExtensionMethods
        [TestMethod]
        public void IfForFrom0IsCalled_ItShouldPerformAnActionOnEveryNumberFrom0ToN()
        {
            var list = new List<int>();
            5.ForFromZero((i) => list.Add(i));
            Assert.IsTrue(list.Is(0, 1, 2, 3, 4));
        }

        [TestMethod]
        public void IfForFrom0IsCalledOnANegativeNumber_ItShouldPerformAnActionOnEveryNumberFrom0ToN()
        {
            var list = new List<int>();
            (-4).ForFromZero((i) => list.Add(i));
            Assert.IsTrue(list.Is(0, -1, -2, -3));
        }

        [TestMethod]
        public void IfForToIsCalledWithAToNumber_ItShouldPerformAnActionOnEveryNumberInBetween()
        {
            var list = new List<int>();
            12.ForTo(6, (i) => list.Add(i));
            Assert.IsTrue(list.Is(12, 11, 10, 9, 8, 7));
        }

        [TestMethod]
        public void IfToIsCalledWithAToNumber_ItShouldReturnAllIntegersInThatRange()
        {
            var arr = 1.To(16);
            for(var i = 1; i < 16; i++)
                Assert.IsTrue(arr[i - 1] == i);

            Assert.AreEqual(15, arr.Length);
        }

        [TestMethod]
        public void IfToIsCalledWithAToNumberLessThanTheNumber_ItShouldReturnAllIntegersInThatRange()
        {
            var arr = 16.To(1);
            for (var i = 0; i < arr.Length; i++)
                Assert.IsTrue(arr[i] == 16 - i);
        }

        [TestMethod]
        public void IfToIsCalledWithAToNumberEqualToTheInitialNumber_ItShouldReturnAnEmptyArray()
        {
            Assert.AreEqual(0, 1.To(1).Length);
        }

        [TestMethod]
        public void IfIsInRangeIsCalledWithAToNumberBetweenTheTwoBounds_ItShouldReturnTrue()
        {
            Assert.IsTrue(1.IsInRange(0, 10));
        }

        [TestMethod]
        public void IfIsInRangeIsCalledWithAToNumberNotBetweenTheTwoBounds_ItShouldReturnFalse()
        {
            Assert.IsFalse(1.IsInRange(3, 10));
        }
        #endregion

        #region EnumerableExtensionMethods
        [TestMethod]
        public void IfForIsCalledWithAnActionOfT_ItShouldCallThatActionForEveryElementOfTheEnumerable()
        {
            var hashSet = new HashSet<int>();
            new HashSet<int> { 1, 2, 3, 4, 5 }.For((element) => hashSet.Add(element));
            Assert.IsTrue(hashSet.Is(0, 1, 2, 3, 4));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfInt_ItShouldCallThatActionForEveryIndexOfTheEnumerable()
        {
            var hashSet = new HashSet<int>();
            new List<int> { 1, 2, 3, 4, 5 }.For((i) => hashSet.Add(i));
            Assert.IsTrue(hashSet.Is(0, 1, 2, 3, 4));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTAndInt_ItShouldCallThatActionForEveryElementOfTheEnumerable()
        {
            var dict = new Dictionary<int, int>();
            new HashSet<int> { 1, 2, 3, 4, 5 }.For((element, index) => dict.Add(index, element));
            Assert.IsTrue(dict.Is((0, 1), (1, 2), (2, 3), (3, 4), (4, 5)));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTArrayAndInt_ItShouldCallThatActionForEveryElementOfTheEnumerable()
        {
            var hashSet = new HashSet<int>();
            new HashSet<int> { 1, 2, 3, 4, 5 }.For((l, i) => hashSet.Add(l.Count() + i));
            Assert.IsTrue(hashSet.Is(5, 6, 7, 8, 9));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfT_ItShouldCallThatActionForEveryElementOfTheArray_AndReturnTheEnumerable()
        {
            var hashSet = new HashSet<TestType>();
            var returned = new HashSet<TestType> { new TestType { TestProperty = true } }.ForAndReturn((element) => hashSet.Add(element));
            Assert.IsTrue(hashSet.First().TestProperty);
            Assert.IsTrue(returned.First().TestProperty);
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfInt_ItShouldCallThatActionForEveryIndexOfTheArray_AndReturnTheEnumerable()
        {
            var hashSet = new HashSet<int>();
            var returned = new HashSet<int> { 1, 2, 3, 4, 5 }.ForAndReturn((element) => hashSet.Add(element));
            Assert.IsTrue(hashSet.Is(0, 1, 2, 3, 4));
            Assert.IsTrue(returned.Is(1, 2, 3, 4, 5));
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTAndInt_ItShouldCallThatActionForEveryElementOfTheArray_AndReturnTheEnumerable()
        {
            var hashSet = new HashSet<(int index, TestType value)>();
            var returned = new HashSet<TestType> { new TestType { TestProperty = true } }
                .ForAndReturn((element, index) => hashSet.Add((index, element)));
            Assert.IsTrue(hashSet.First().index == 0);
            Assert.IsTrue(hashSet.First().value.TestProperty);
            Assert.IsTrue(returned.First().TestProperty);
        }

        [TestMethod]
        public void IfForIsCalledWithAnActionOfTArrayAndInt_ItShouldCallThatActionForEveryElementOfTheArray_AndReturnTheEnumerable()
        {
            var hashSet = new HashSet<int>();
            var returned = new HashSet<int> { 1, 2, 3, 4, 5 }
                .ForAndReturn((arr, i) => {
                    if (arr.Is(1, 2, 3, 4, 5))
                        hashSet.Add(i);
                });
            Assert.IsTrue(hashSet.Is(0, 1, 2, 3, 4));
            Assert.IsTrue(returned.Is(1, 2, 3, 4, 5));
        }

        [TestMethod]
        public void IfGetIsCalledWithAnIndex_ItShouldReturnTheElementAtThatIndex()
        {
            Assert.AreEqual(2, new List<int> { 1, 2, 3, 4 }.Get(1));
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfGetIsCalledWithAnInvalidIndex_ItShouldThrowAnError()
        {
            Assert.AreEqual(2, new List<int> { 1, 2, 3, 4 }.Get(8));
        }

        [TestMethod]
        public void IfGetIsCalledWithAValidElement_ItShouldReturnThatElement()
        {
            var element = new TestType { TestProperty = true };
            var list = new List<TestType> { element, new TestType(), new TestType() };

            Assert.AreEqual(element, list.Get(element));
        }

        [TestMethod]
        public void IfGetIsCalledWithAnElementThatDoesNotExist_ItShouldReturnThatElement()
        {
            var element = new TestType { TestProperty = true };
            var list = new List<TestType> { new TestType(), new TestType() };

            Assert.AreEqual(default, list.Get(element));
        }

        [TestMethod]
        public void IfGetAllIsCalledWithAnElementThatExists_ItShouldReturnAllInstancesOfThatElementInTheEnumerable()
        {
            var element = new TestType { TestProperty = true };
            var result = new List<TestType> { element, element, element }.GetAll(element);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.AreAllTheSame());
        }

        [TestMethod]
        public void IfRemoveIsCalledWithAnElementThatDoesExist_ItShouldRemoveThatElementAndReturnTheEnumerable()
        {
            var element = new TestType { TestProperty = true };
            var list = new List<TestType> { new TestType(), new TestType(), element };
            var resultsList = list.RemoveAtIndex(2);

            Assert.IsFalse(resultsList.Contains(element));
            Assert.AreEqual(2, resultsList.Count());
        }

        [TestMethod]
        public void IfRemoveAllIsCalledWithAnElementThatDoesExist_ItShouldRemoveAllMatchingElementsAndReturnTheEnumerable()
        {
            var element = new TestType { TestProperty = true };
            var resultsList = new List<TestType> { new TestType(), element, element }.RemoveAll(element);

            Assert.IsFalse(resultsList.Contains(element));
            Assert.AreEqual(1, resultsList.Count());
        }

        [TestMethod]
        public void IfRemoveAllIsCalledWithAnElementThatDoesNotExist_ItShouldReturnTheEnumerableWithTheSameElements()
        {
            var element = new TestType { TestProperty = true };
            var resultsList = new List<TestType> { new TestType() }.RemoveAll(element);

            Assert.IsFalse(resultsList.Contains(element));
            Assert.AreEqual(1, resultsList.Count());
        }

        [TestMethod]
        public void IfIsIsCalledWithAParamsArrayWithElementsThatDoNotExistInTheEnumerable_ItShouldReturnFalse()
        {
            Assert.IsFalse(new List<int> { 1, 2, 3, 4, 5 }.Is(1, 2, 4, 3, 5));
        }

        [TestMethod]
        public void IfIsIsCalledWithAParamsArrayOfADifferentLength_ItShouldReturnFalse()
        {
            Assert.IsFalse(new List<int> { 1, 2, 3 }.Is(1, 2));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IfAreAllTheSameIsCalledOnANullEnumerable_ItShouldThrowAnException()
        {
            IList<int> list = null;
            list.AreAllTheSame();
        }

        [TestMethod]
        public void IfAreAllTheSameIsCalledOnAnEnumerableOfLength1Or0_ItShouldReturnTrue()
        {
            Assert.IsTrue(new List<int> { 1 }.AreAllTheSame());
            Assert.IsTrue(new List<int>().AreAllTheSame());
        }

        [TestMethod]
        public void IfAreAllTheSameIsCalledOnAnEnumerableWithAtLeastOneDifferentElement_ItShouldReturnFalse()
        {
            Assert.IsFalse(new List<int> { 1, 1, 1, 2, 1, 1 }.AreAllTheSame());
        }

        [TestMethod]
        public void IfMultiSplitIsCalledOnAnEnumerableWithAnArrayOfFunctions_ItShouldPutEveryMatchingElementIntoTheAppropriateLists()
        {
            var enumerable = new List<int> { 0, 101, 201, 301, 401, 501 };
            var result = enumerable.MultiSplit(
                x => x >= 0,
                x => x > 100,
                x => x > 200,
                x => x > 300,
                x => x > 400,
                x => x > 500);

            Assert.IsTrue(result.ElementAt(0).Count == 6 && result.ElementAt(0).Is(0, 101, 201, 301, 401, 501));
            Assert.IsTrue(result.ElementAt(1).Count == 5 && result.ElementAt(1).Is(101, 201, 301, 401, 501));
            Assert.IsTrue(result.ElementAt(2).Count == 4 && result.ElementAt(2).Is(201, 301, 401, 501));
            Assert.IsTrue(result.ElementAt(3).Count == 3 && result.ElementAt(3).Is(301, 401, 501));
            Assert.IsTrue(result.ElementAt(4).Count == 2 && result.ElementAt(4).Is(401, 501));
            Assert.IsTrue(result.ElementAt(5).Count == 1 && result.ElementAt(5).Is(501));
        }

        [TestMethod]
        public void IfReplaceAllIsCalledOnAnEnumerableWithMatchingElements_ItShouldReplaceAllMatchingElementsWithTheSuppliedObject()
        {
            var result = new List<int> { 1, 1, 2, 2, 3, 3 }.ReplaceAll(x => x == 2, 99);
            Assert.IsTrue(result.Is(1, 1, 99, 99, 3, 3));
        }

        [TestMethod]
        public void IfAllIndexesOfIsCalled_ItShouldReturnAllIndexesOfTheSuppliedObject()
        {
            Assert.IsTrue(new List<int> { 1, 4, 2, 6, 3, 1 }.AllIndexesOf(1).Is(0, 5));
        }

        [TestMethod]
        public void IfReplaceIsCalled_ItShouldReplaceTheFirstMatchingElement()
        {
            Assert.IsTrue(new List<int> { 1, 4, 2, 6, 3, 1 }.Replace(1, 99).Is(99, 4, 2, 6, 3, 1));
            Assert.IsTrue(new List<int> { 1, 4, 2, 6, 3, 1 }.Replace(5, 99).Is(1, 4, 2, 6, 3, 1));
        }

        [TestMethod]
        public void IfReplaceAllIsCalled_ItShouldReplaceAllMatchingElements()
        {
            Assert.IsTrue(new List<int> { 1, 4, 2, 6, 3, 1 }.ReplaceAll(1, 99).Is(99, 4, 2, 6, 3, 99));
            Assert.IsTrue(new List<int> { 1, 4, 2, 6, 3, 1 }.ReplaceAll(5, 99).Is(1, 4, 2, 6, 3, 1));
        }

        [TestMethod]
        public void IfRemoveWhileIsCalled_ItShouldRemoveAllElementsUntilTheFunctionReturnsFalse()
        {
            Assert.IsTrue(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }.RemoveWhile(x => x < 5).Is(5, 6, 7, 8));
            Assert.IsTrue(new List<int> { 1, 1, 1, 1, 1 }.RemoveWhile(x => x == 1).Is());
            Assert.IsTrue(new List<int> { 1, 2, 3, 4, 5 }.RemoveWhile(x => x > 1).Is(1, 2, 3, 4, 5));
        }

        [TestMethod]
        public void IfRemoveAllIsCalledWithAFunction_ItShouldRemoveAllMatchingElements()
        {
            Assert.IsTrue(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }.RemoveAll<int>(x => x > 3).Is(1, 2, 3));
            Assert.IsTrue(new List<int> { 1, 1, 1, 1, 1 }.RemoveAll<int>(x => x == 1).Is());
            Assert.IsTrue(new List<int> { 1, 2, 3, 4, 5 }.RemoveAll<int>(x => x == 0).Is(1, 2, 3, 4, 5));
        }

        [TestMethod]
        public void IfWithoutElementsIsCalledWithAnArrayOfElements_ItShouldExcludeAllMatchingElements()
        {
            Assert.IsTrue(new List<int> { 1, 2, 3, 4, 5 }.WithoutElements(2, 3).Is(1, 4, 5));
            Assert.IsTrue(new List<int> { 1, 2, 3, 4 }.WithoutElements(99, 100).Is(1, 2, 3, 4));
            Assert.IsTrue(new List<int>().WithoutElements(1).Is());
        }

        [TestMethod]
        public void IfSplitIsCalledWithANumberN_ItShouldSplitTheEnumerableIntoEnumerablesOfLengthN()
        {
            var array = new int[100];
            array.Fill(1);
            var result = array.Split(10);
            Assert.IsTrue(result.Count() == 10);
            Assert.IsTrue(result.All(x => x.Count == 10));
        }

        [TestMethod]
        public void IfSplitIsCalledWithANumberThatIsNotExactlyDivisibleByTheLengthOfTheEnumerable_TheLastListShouldContainTheRemainingElements()
        {
            var array = new int[38];
            array.Fill(1);
            var result = array.Split(10);
            Assert.IsTrue(result.Count() == 4);
            Assert.IsTrue(result.Take(3).All(x => x.Count == 10));
            Assert.IsTrue(result.Last().Count == 8);
            Assert.IsFalse(result.All(x => x.Count == 10));
        }

        [TestMethod]
        public void IfFillIsCalledOnAnEnumerable_ItShouldFillTheEnumerableWithTheProvidedObject()
        {
            var list = new List<int> { 1, 54, 76, 23, 99 }.FillWith(1);
            var list2 = new List<int>().FillWith(1);
            Assert.IsTrue(list.Is(1, 1, 1, 1, 1));
            Assert.IsTrue(list2.Is());
        }

        [TestMethod]
        public void IfForEachIsCalledWithAnAction_ItShouldPerformTheActionOnEveryElement()
        {
            var list = new List<TestType> { new TestType(), new TestType() };
            list.ForEach((t) => t.TestProperty = true);
            Assert.IsTrue(list.All(x => x.TestProperty));
            Assert.AreEqual(2, list.Count);

            var countable = new CountReferenceClass();
            1.To(5).ForEach((t) => { for (var i = 0; i < t; i++) { countable.Increment(); } });
            Assert.AreEqual(10, countable.Count);
        }

        [TestMethod]
        public void IfFirstOrNullIsCalledWithAPredicateThatMatchesAnElement_ItShouldReturnTheElement()
        {
            var result = new List<TestType> { new TestType(), new TestType { TestProperty = true }, new TestType() }.FirstOrNull(x => x.TestProperty);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IfFirstOrNullIsCalledWithAPredicateThatDoesNotMatchAnElement_ItShouldReturnNull()
        {
            var result = new List<TestType> { new TestType { TestProperty = true }, new TestType { TestProperty = true }, new TestType { TestProperty = true } }.FirstOrNull(x => !x.TestProperty);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IfAddNIsCalled_ItShouldAddNObjectsToTheList()
        {
            var stack = new Stack<TestType>();
            var result = stack.AddN(10);
            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public void IfAddIsCalled_ItShouldAddTheGivenObjectAndReturnTheEnumerable()
        {
            var list = new List<TestType>().Add<TestType>(new TestType { TestProperty = true });            
            Assert.AreEqual(1, list.Count());
            Assert.IsTrue(list.All(x => x.TestProperty));
        }

        [TestMethod]
        public void IfRandomIsCalled_ItShouldReturnARandomElementFromTheEnumerable()
        {
            Assert.IsTrue(new List<int> { 1, 2, 3, 4, 5 }.Random().IsIn(1, 2, 3, 4, 5));
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfRandomIsCalledOnAnEmptyEnumerable_ItShouldThrowAnException()
        {
            new List<int>().Random();
        }

        [TestMethod]
        public void IfShuffleIsCalledOnAnEnumerableOfSize0Or1_ItShouldReturnTheEnumerable()
        {
            var list = new List<int>();
            var result = list.Shuffle();
            Assert.IsTrue(ReferenceEquals(list, result));

            var list2 = new List<int> { 1 };
            var result2 = list2.Shuffle();
            Assert.IsTrue(ReferenceEquals(list2, result2));
        }

        [TestMethod]
        public void IfShuffleIsCalledOnAnEnumerable_ItShouldProduceAValidShuffle()
        {
            Assert.IsFalse(new List<int> { 1, 2, 3, 4, 5 }.Shuffle().Is(1, 2, 3, 4, 5));
        }

        [TestMethod]
        public void IfWherePreviousIsCalledOnAnEnumerable_ItShouldTakeElementsWhileTheFunctionReturnsTrue()
        {
            Assert.IsTrue(new List<int> { 1, 2, 3, 4, 5 }.WherePrevious(x => x < 4).Is(2, 3, 4));
            Assert.IsTrue(new List<int> { 3, 4, 5 }.WherePrevious(x => x < 3).Is());
            Assert.IsTrue(new List<int>().WherePrevious(x => x < 5).Is());
        }

        [TestMethod]
        public void IfRandomWhereIsCalledOnAnEnumerable_ItShouldReturnARandomElementThatMatchesThePredicate()
        {
            Assert.IsTrue(new List<int> { 1, 2, 3, 4, 5 }.RandomWhere(x => x < 4).IsIn(1, 2, 3));
            Assert.IsTrue(new List<int> { 3, 4, 5 }.RandomWhere(x => x >= 5).Is(5));            
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfRandomWhereIsCalledOnAnEnumerableOfSize0_ItShouldThrowAnException()
        {
            new List<int>().RandomWhere(x => x > 4);
        }

        [TestMethod]
        public void IfIsNullOrEmptyIsCalledOnANullOrEmptyEnumerable_ItShouldReturnTrueElseFalse()
        {
            IList<int> list = null;
            Assert.IsFalse(new List<int> { 1, 2, 3, 4, 5 }.IsNullOrEmpty());
            Assert.IsTrue(new List<int>().IsNullOrEmpty());
            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [TestMethod]
        public void IfRemoveIsCalledOnAnEnumerable_ItShouldRemoveTheProvidedObjectFromTheEnumerable()
        {
            Assert.IsTrue(new int[] { 1, 2, 3, 4, 5 }.Remove(1).Is(2, 3, 4, 5));
            Assert.IsTrue(new int[] { 1, 2, 3, 4, 5 }.Remove(99).Is(1, 2, 3, 4, 5));
            Assert.IsTrue(new int[0].Remove(99).Is());
        }

        [TestMethod]
        public void IfBetweenValuesIsCalledOnAnEnumerable_ItShouldReturnAllElementsInTheGivenRange()
        {
            Assert.IsTrue(new int[] { 1, 2, 3, 4, 5 }.BetweenValues(0, 6).Is(1, 2, 3, 4, 5));
            Assert.IsTrue(new int[] { 1, 2, 3, 4, 5 }.BetweenValues(1, 5).Is(2, 3, 4));
            Assert.IsTrue(new int[] { 1, 2, 3, 4, 5 }.BetweenValues(99, 105).Is());
            Assert.IsTrue(new int[0].BetweenValues(99, 109).Is());
        }

        [TestMethod]
        public void IfBetweenValuesInclusiveIsCalledOnAnEnumerable_ItShouldReturnAllElementsInTheGivenRange()
        {
            Assert.IsTrue(new int[] { 1, 2, 3, 4, 5 }.BetweenValuesInclusive(1, 5).Is(1, 2, 3, 4, 5));
            Assert.IsTrue(new int[] { 1, 2, 3, 4, 5 }.BetweenValuesInclusive(2, 3).Is(2, 3));
            Assert.IsTrue(new int[] { 1, 2, 3, 4, 5 }.BetweenValuesInclusive(99, 105).Is());
            Assert.IsTrue(new int[0].BetweenValuesInclusive(99, 109).Is());
        }

        [TestMethod]
        public void IfForIsCalledWithAnAction_ItShouldPerformThatActionOnEveryIndexOfTheEnumerable()
        {
            var enumerable = new List<int> { 1, 2, 3, 4, 5 };
            var indexes = new List<int>();
            enumerable.For((i) => indexes.Add(i));
            Assert.IsTrue(indexes.Is(0, 1, 2, 3, 4));
        }

        [TestMethod]
        public void IfForAndReturnIsCalledWithAnAction_ItShouldPerformThatActionOnEveryIndexOfTheEnumerable_AndReturnTheEnumerable()
        {
            var enumerable = new List<int> { 1, 2, 3, 4, 5 };
            var result = enumerable.ForAndReturn((i) => { });
            Assert.IsTrue(enumerable.SequenceEqual(result));
        }
        #endregion

        #region DictionaryExtensionMethods
        [TestMethod]
        public void IfIsIsCalledWithAMismatchedParamArray_ItShouldReturnFalse()
        {
            var dict = new Dictionary<int, string>
            {
                [0] = "1",
                [1] = "2"
            };
            Assert.IsFalse(dict.Is((0, "1"), (1, "fail")));
        }

        [TestMethod]
        public void IfIsIsCalledWithAnEquivalentParamArray_ItShouldReturnTrue()
        {
            var dict = new Dictionary<int, string>
            {
                [0] = "1",
                [1] = "2"
            };
            Assert.IsTrue(dict.Is((0, "1"), (1, "2")));
        }

        [TestMethod]
        public void IfAsReadOnlyIsCalled_ItShouldReturnTheReadOnlyDictionaryEquivalent()
        {
            var dict = new Dictionary<int, string> {
                [0] = "Value1",
                [1] = "Value2"
            };
            var dict2 = dict.AsReadOnly();
            Assert.AreEqual("Value1", dict2[0]);
            Assert.AreEqual("Value2", dict2[1]);
        }
        #endregion

        private enum TestEnum
        {
            EnumValue1,
            EnumValue2 
        }

        private enum BlankEnum { }

        [Serializable]
        private class TestType : IComparable<TestType>
        {
            public TestType() { }

            public bool TestProperty { get; set; } = false;

            public int CompareTo(TestType other) => TestProperty.CompareTo(other.TestProperty);
        }

        private class TestTypeNonSerializable
        {
            public TestTypeNonSerializable() { }

            public bool TestProperty { get; set; } = false;

            public int CompareTo(TestType other) => TestProperty.CompareTo(other.TestProperty);
        }

        private class CountReferenceClass
        {
            public CountReferenceClass() { }

            public void Increment() => Count++;

            public int Count { get; set; } = 0;
        }

        private class TestType2
        {
            public TestType2() { }
        }

        private class ComparerClass<T> : IComparer<T> where T: IComparable<T>
        {
            public int Compare(T x, T y) => x.CompareTo(y);
        }

        private class DisposableTestType : IDisposable
        {
            public void Dispose() => HasBeenDisposed = true;
            public bool HasBeenDisposed { get; set; } = false;
        }
    }
}