using System;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class EquatableSequenceState<T> : IEquatable<EquatableSequenceState<T>>
      where T : IEquatable<T>
   {
      public T current;
      public T step;
      public T stop;

      public bool Equals(EquatableSequenceState<T> other)
      {
         return current.Equals(other.current) && stop.Equals(other.stop) && step.Equals(other.step);
      }
   }
}