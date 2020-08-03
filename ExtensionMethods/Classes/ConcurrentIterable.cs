using System;
using System.Linq;
using System.Collections.Generic;

namespace ExtensionMethods.Classes
{
    public class ConcurrentIterable<T, T2>
    {
        public ConcurrentIterable(IEnumerable<T> collection, IEnumerable<T2> collectionTwo)
        {
            if (collection.Count() != collectionTwo.Count())
                throw new ArgumentException("Collections are not of the same length.");
            CollectionOne = collection.ToArray();
            CollectionTwo = collectionTwo.ToArray();
        }

        public T[] CollectionOne { get; }

        public T2[] CollectionTwo { get; }

        public int Index { get; private set; }

        public bool HasNext => Index < CollectionOne.Length;

        public bool HasPrevious => Index > 0;

        public bool IsEmpty => !CollectionOne.Any();

        public (T, T2) First => (CollectionOne.First(), CollectionTwo.First());

        public (T, T2) Last => (CollectionOne.Last(), CollectionTwo.Last());

        public (T, T2) Previous => (CollectionOne[Index - 1], CollectionTwo[Index - 1]);

        public (T, T2) Next()
        {
            (T, T2) @return;

            if (Index == 0 && !IsEmpty)
                @return = First;
            else
                if (HasNext)
                    @return = CollectionsTuple;
                else
                    throw new ArgumentOutOfRangeException();

            Index++;
            return @return;
        }

        public bool GetNext(out (T, T2) result)
        {
            result = (default, default);
            try
            {
                result = Next();
                return true;
            }
            catch (ArgumentOutOfRangeException) { }

            return false;
        }

        public bool GetPrevious(out (T, T2) result)
        {
            result = (default, default);
            if (!HasPrevious) return false;

            result = Previous;
            return true;
        }

        public bool SetIndex(int index)
        {
            if (index < 0 || index > CollectionOne.Length - 1)
                return false;

            Index = index;
            return true;
        }

        public IEnumerable<(T, T2)> AsEnumerable() => CollectionOne.Select((t, i) => (t, CollectionTwo[i]));

        private (T, T2) CollectionsTuple => (CollectionOne[Index], CollectionTwo[Index]);
    }
}