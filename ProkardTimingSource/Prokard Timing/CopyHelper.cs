using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Rentix
{
    using CopyingState = System.Collections.Generic.Dictionary<object, object>;



    public sealed class CopyHelper
    {

        private static readonly Type array_type = typeof(Array);

        private static readonly MethodInfo memberwise_clone = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

        private static void MakeArrayRowDeepCopy(CopyingState state, Array array, int[] indices, int rank)
        {
            int next_rank = rank + 1;
            int upper_bound = array.GetUpperBound(rank);

            while (indices[rank] <= upper_bound)
            {
                object value = array.GetValue(indices);
                if (!object.ReferenceEquals(value, null))
                    array.SetValue(CreateDeepCopyInternal(state, value), indices);

                if (next_rank < array.Rank)
                    MakeArrayRowDeepCopy(state, array, indices, next_rank);

                indices[rank] += 1;
            }
            indices[rank] = array.GetLowerBound(rank);
        }

        private static Array CreateArrayDeepCopy(CopyingState state, Array array)
        {
            Array result = (Array)array.Clone();
            int[] indices = new int[result.Rank];
            for (int rank = 0; rank < indices.Length; ++rank)
                indices[rank] = result.GetLowerBound(rank);
            MakeArrayRowDeepCopy(state, result, indices, 0);
            return result;
        }

        private static object CreateShallowCopyInternal(object o)
        {
            if (o is Array)
                return (o as Array).Clone();
            else if (o is string)
                return string.Copy((string)o);
            return memberwise_clone.Invoke(o, null);
        }

        public static T CreateShallowCopy<T>(T o)
        {
            object input = (object)o;
            if (object.ReferenceEquals(input, null))
                return o;
            return (T)CreateShallowCopyInternal(input);
        }

        private static object CreateDeepCopyInternal(CopyingState state, object o)
        {
            object exist_object;
            if (state.TryGetValue(o, out exist_object))
                return exist_object;

            if (o is Array)
            {
                object array_copy = CreateArrayDeepCopy(state, (Array)o);
                state[o] = array_copy;
                return array_copy;
            }
            else if (o is string)
            {
                object string_copy = string.Copy((string)o);
                state[o] = string_copy;
                return string_copy;
            }
            else
            {
                Type o_type = o.GetType();
                if (o_type.IsPrimitive)
                    return o;
                object copy = memberwise_clone.Invoke(o, null);
                state[o] = copy;
                foreach (FieldInfo f in o_type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    object original = f.GetValue(o);
                    if (!object.ReferenceEquals(original, null))
                        f.SetValue(copy, CreateDeepCopyInternal(state, original));
                }
                return copy;
            }
        }

        public static T CreateDeepCopy<T>(T o)
        {
            object input = (object)o;
            if (object.ReferenceEquals(o, null))
            {
                return o;
            }
            return (T)CreateDeepCopyInternal(new CopyingState(), input);
        }

    }

}
