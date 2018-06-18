using Jcd.Utilities.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jcd.Utilities.Test.Generators
{
   public class CaptureAndTransitionGeneratorTests
   {
      /// <summary>
      /// A helper class that counts from start to end by step.
      /// </summary>
      public class CounterGenerator : CaptureAndTransitionGenerator<CounterGenerator.State, int>
      {
         public class State {
            public int current;
         }
         public CounterGenerator(int start, int end, int step) : base(new State { current = start }, (State s, out bool @continue)=> {
            // capture current state.
            var result = s.current;
            // move to the next state.
            s.current += step;
            // compare to see if this is a terminal state.
            @continue = s.current <= end;
            // return the current state.
            return result;
         })
         {
         }
      }
      /// <summary>
      /// A fake generator class intended to trigger base constructor exceptions.
      /// </summary>
      public class BadGenerator : CaptureAndTransitionGenerator<BadGenerator.State, int>
      {
         public class State
         {
            public int current;
         }
         public BadGenerator(int start, int end, int step) : base(new State { current = start }, null)
         {
         }
      }

      /// <summary>
      /// Validate that Constructor throws ArgumentNullException when given a null for the transition function.
      /// </summary>
      [Fact]
      public void Constructor_WhenGivenNullForTransitionFunction_ThrowsArgumentNullException()
      {
         Assert.Throws<ArgumentNullException>(() => {
            var bad = new BadGenerator(1, 2, 1);
         });
      }

      /// <summary>
      /// Validate that Constructor throws no exception when given a valid transition function.
      /// </summary>
      [Fact]
      public void Constructor_WhenGivenValidTransitionFunction_ThrowsNoException()
      {
         var good = new CounterGenerator(1, 5, 1);
      }

      /// <summary>
      /// Validate that GetEnumerator returns an enumerator for valid generator.
      /// </summary>
      [Fact]
      public void GetEnumerator_ForValidGenerator_ReturnsAnEnumerator()
      {
         var good = new CounterGenerator(1, 5, 1);
         Assert.NotNull(good.GetEnumerator());
      }

      /// <summary>
      /// Validate that GetEnumerator returns an enumerator for a valid generator.
      /// </summary>
      [Fact]
      public void BaseInterfaceGetEnumerator_ForValidGenerator_ReturnsAnEnumerator()
      {
         var good = new CounterGenerator(1, 5, 1);
         var baseEnumerable = good as IEnumerable;
         Assert.NotNull(baseEnumerable.GetEnumerator());
      }

      /// <summary>
      /// Validate that we enumerate all generated elements successfully when given valid data.
      /// </summary>
      [Fact]
      public void Enumerate_WhenGivenValidData_AllGeneratedElementsSuccessfully()
      {
         int i = 0;
         int lastj=-1;

         foreach (var j in new CounterGenerator(1, 5, 1))
         {
            i++;
            lastj = j;
         }

         Assert.Equal(i, lastj);
      }

      /// <summary>
      /// Validate that we enumerate all generated elements successfully when given valid data and casting to IEnumerable.
      /// </summary>
      [Fact]
      public void BaseEnumerable_Enumerate_WhenGivenValidData_AllGeneratedElementsSuccessfully()
      {
         int i = 0;
         int lastj = -1;
         var enumerable = new CounterGenerator(1, 5, 1) as IEnumerable;

         foreach (int j in enumerable)
         {
            i++;
            lastj = j;
         }

         Assert.Equal(i, lastj);
      }
   }
}
