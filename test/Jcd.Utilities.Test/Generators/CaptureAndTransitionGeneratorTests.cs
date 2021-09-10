using System;
using System.Collections;
using Jcd.Utilities.Generators;
using Xunit;

namespace Jcd.Utilities.Test.Generators
{
   public class CaptureAndTransitionGeneratorTests
   {
      /// <inheritdoc />
      /// <summary>
      ///    A helper class that counts from start to end by step.
      /// </summary>
      public class CounterGenerator : CaptureAndTransitionGenerator<CounterGenerator.State, int>
      {
         public CounterGenerator(int start, int end, int step)
            : base(new State {Current = start},
                   (State s, out bool @continue) =>
                   {
                      // capture current state.
                      var result = s.Current;

                      // move to the next state.
                      s.Current += step;

                      // compare to see if this is a terminal state.
                      @continue = s.Current <= end;

                      // return the current state.
                      return result;
                   })
         {
         }

         public class State
         {
            public int Current;
         }
      }

      /// <inheritdoc />
      /// <summary>
      ///    A fake generator class intended to trigger base constructor exceptions.
      /// </summary>
      public class BadGenerator : CaptureAndTransitionGenerator<BadGenerator.State, int>
      {
         // ReSharper disable once NotAccessedField.Local
         private readonly int _end;

         // ReSharper disable once NotAccessedField.Local
         private readonly int _step;

         public BadGenerator(int start, int end, int step)
            : base(new State {Current = start}, null)
         {
            _end = end;
            _step = step;
         }

         public class State
         {
            public int Current;
         }
      }

      /// <summary>
      ///    Validate that we enumerate all generated elements successfully when given valid data and casting to IEnumerable.
      /// </summary>
      [Fact]
      public void BaseEnumerable_Enumerate_WhenGivenValidData_AllGeneratedElementsSuccessfully()
      {
         var i = 0;
         var lastJ = -1;
         var enumerable = new CounterGenerator(1, 5, 1) as IEnumerable;

         foreach (int j in enumerable)
         {
            i++;
            lastJ = j;
         }

         Assert.Equal(i, lastJ);
      }

      /// <summary>
      ///    Validate that GetEnumerator returns an enumerator for a valid generator.
      /// </summary>
      [Fact]
      public void BaseInterfaceGetEnumerator_ForValidGenerator_ReturnsAnEnumerator()
      {
         var good = new CounterGenerator(1, 5, 1);
         var baseEnumerable = (IEnumerable) good;
         Assert.NotNull(baseEnumerable.GetEnumerator());
      }

      /// <summary>
      ///    Validate that Constructor throws ArgumentNullException when given a null for the transition function.
      /// </summary>
      [Fact]
      public void Constructor_WhenGivenNullForTransitionFunction_ThrowsArgumentNullException()
      {
         Assert.Throws<ArgumentNullException>(() =>
                                              {
                                                 var dummy = new BadGenerator(1, 2, 1);
                                              });
      }

      /// <summary>
      ///    Validate that Constructor throws no exception when given a valid transition function.
      /// </summary>
      [Fact]
      public void Constructor_WhenGivenValidTransitionFunction_ThrowsNoException()
      {
         var unused = new CounterGenerator(1, 5, 1);
      }

      /// <summary>
      ///    Validate that we enumerate all generated elements successfully when given valid data.
      /// </summary>
      [Fact]
      public void Enumerate_WhenGivenValidData_AllGeneratedElementsSuccessfully()
      {
         var i = 0;
         var lastJ = -1;

         foreach (var j in new CounterGenerator(1, 5, 1))
         {
            i++;
            lastJ = j;
         }

         Assert.Equal(i, lastJ);
      }

      /// <summary>
      ///    Validate that GetEnumerator returns an enumerator for valid generator.
      /// </summary>
      [Fact]
      public void GetEnumerator_ForValidGenerator_ReturnsAnEnumerator()
      {
         var good = new CounterGenerator(1, 5, 1);
         Assert.NotNull(good.GetEnumerator());
      }
   }
}
