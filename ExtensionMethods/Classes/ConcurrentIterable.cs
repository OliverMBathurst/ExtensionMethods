﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace ExtensionMethods.Classes
{
    public class ConcurrentIterable<T, C>
    {
        public ConcurrentIterable(ICollection<T> collection, ICollection<C> collectionTwo)
        {
            if (collection.Count != collectionTwo.Count)
                throw new ArgumentException("Collections are not of the same length.");
            CollectionOne = collection.ToArray();
            CollectionTwo = collectionTwo.ToArray();
        }

        public T[] CollectionOne { get; private set; }

        public C[] CollectionTwo { get; private set; }

        public int Index { get; private set; }

        public bool HasNext => Index < CollectionOne.Length;

        public bool IsEmpty => CollectionOne.Count() == 0;

        public (T, C) First => (CollectionOne.First(), CollectionTwo.First());

        public (T, C) Last => (CollectionOne.Last(), CollectionTwo.Last());

        private (T, C) CollectionsTuple => (CollectionOne[Index], CollectionTwo[Index]);

        public (T, C) Next()
        {
            (T,C) @return;

            if (Index == 0 && !IsEmpty)
                @return = First;
            else
                if (HasNext)
                    @return = CollectionsTuple;
                else
                    throw new ArgumentOutOfRangeException("");

            Index++;
            return @return;
        }

        public bool GetNext(out (T, C) result)
        {
            result = (default(T), default(C));
            if (HasNext)
            {
                result = Next();
                return true;
            }

            return false;
        }

        public bool SetIndex(int index)
        {
            if (index < 0 || index > CollectionOne.Length - 1)
                return false;

            Index = index;
            return true;
        }

        public IEnumerable<(T, C)> AsEnumerable()
        {
            for(var i = 0; i < CollectionOne.Length; i++)
                yield return (CollectionOne[i], CollectionTwo[i]);
        }
    }
}