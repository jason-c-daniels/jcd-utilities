using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Xml;
using Jcd.Utilities.Formatting;
using Jcd.Utilities.Generators;
using Jcd.Utilities.Reflection;
using Jcd.Utilities.Samples.ConsoleApp.Generators;
using Newtonsoft.Json;

using Jcd.Utilities.Formatting;
using Jcd.Utilities.Generators;
using Jcd.Utilities.Samples.ConsoleApp.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp
{
   // ReSharper disable once ClassNeverInstantiated.Global
   internal class Node
   {
      public int V;
      public HashSet<Node> C=new HashSet<Node>();
      public HashSet<Node> P =new HashSet<Node>();

      public Node(int v)
      {
         V = v;
      }

      public void AddChild(Node child)
      {
         if (child == null) return;
         if (ReferenceEquals(this, child)) return;
         if (P.Contains(child)) return;
         if (C.Contains(child)) return;
         C.Add(child);
         child.AddParent(this);
      }

      public void AddParent(Node parent)
      {
         if (parent == null) return;
         if (ReferenceEquals(this, parent)) return;
         if (C.Contains(parent)) return;
         if (P.Contains(parent)) return;
         P.Add(parent);
      }

      public static Node MakeRandomCyclicGraph(int nodeCount)
      {
         var l = new List<Node>();
         // generate the nodes.
         for(int i=0;i<nodeCount;i++)
         {
            l.Add(new Node(i));
         }

         var cl = new List<Node>(l);
         // randomly connect the nodes.
         var rnd=new Random();
         while (cl.Any())
         {
            foreach (var node in cl)
            {
               var n = rnd.Next(0, nodeCount - 1);
               l[n].AddChild(node);
            }

            cl.RemoveAll(n => n.P.Any());
         }
         // return the first node.
         return l[0];
      }
   }
   public enum OpinionType
   {
      ILikeLlamas,
      IDontMindLlamas,
      DonkeysRule,
      WhatTimeIsIt
   }
   public class Foo
   {
      public string Name { get; set; } = "A Name";
      public string Address { get; set; } = "An Address";
      public OpinionType Opinion { get; set; } = OpinionType.ILikeLlamas;
   }
   internal class Program
   {
      private static void Main()
      {
         Console.WriteLine("Hello World!");
         var i = long.MinValue;
         var encoder = IntegerEncoders.Hexadecimal;
         var parsed = encoder.ParseInt64(encoder.Format(i));
         Console.WriteLine($"{encoder.Format(i)} == {Convert.ToString(parsed, encoder.Base)} ({Convert.ToString(parsed, 10)})");

         foreach (var j in new Int32SequenceGenerator(10, -20, -1))
         {
            Console.Write($"{j},");
         }

         Console.WriteLine();

         var numberGenerator = new
            CaptureAndTransitionGenerator<Int16SequenceState, short>(
                                                                     new Int16SequenceState
                                                                     {
                                                                        current = 100,
                                                                        stop = 0,
                                                                        step = -1
                                                                     },
                                                                     (Int16SequenceState state, out bool @continue) =>
                                                                     {
                                                                        var result = state.current;
                                                                        state.current += state.step;

                                                                        @continue =
                                                                           ((state.step < 0) &&
                                                                            (state.current >= state.stop)
                                                                           ) // handle counting down.
                                                                         ||
                                                                           ((state.step > 0) &&
                                                                            (state.current <=
                                                                             state.stop)); // handle counting up.

                                                                        return result;
                                                                     });

         /*         var aaa = new { Aoo = "a", Zoo = 5, ConsoleColor.DarkBlue};
                  var bbb = new { Boo = "1", Yoo = 5, ConsoleColor.Cyan, R=new object(), B=new object() };
                  var ddd = new { Doo = "2", Woo = 5, ConsoleColor.DarkRed, R = new object(), B = new object() };
                  var eee = new { Eoo = "d", Xoo = 5, ConsoleColor.Green, R = new object(), B = new object() };
                  var props = aaa.GetType().EnumerateProperties().ToList();
                  var props1 = props.ToPropertyInfoValuePairs(aaa).ToList();
                  var props2 = props1.ToNameValuePairs().ToList();
                  var dt = IntegerEncoders.Base32Crockford.ToExpandoObject();
                  object a=null, 
                         b = null,
                         c = null, 
                         d1 = null,
                         e1 = null;
                  var dt2 = new { a, b, c= new { d1, e1 }}.ToExpandoObject();
                  dt = aaa.DfsD<ExpandoObject>();
                  var serializer = JsonReaderWriterFactory.CreateJsonReader(Stream.Null, XmlDictionaryReaderQuotas.Max);
                  */


         // generate a graph with cycles. (parent and parent...)
         var web = Node.MakeRandomCyclicGraph(20000);
         var dt = web.DfsD<ExpandoObject>();
         var webstr2 = JsonConvert.SerializeObject(dt, new JsonSerializerSettings
         {
            PreserveReferencesHandling = PreserveReferencesHandling.All
         });
         var webstr = JsonConvert.SerializeObject(web,new JsonSerializerSettings
         {
            PreserveReferencesHandling = PreserveReferencesHandling.All
         });
         //for (i = 1; i < 10000; i++)
         //{

         //}

         return;
         //aaa = new Foo { Opinion = OpinionType.IDontMindLlamas };
         //dt = aaa.ToDictionaryTree();

         int xx = 0;

         foreach (var k in numberGenerator)
         {
            Console.Write($"{k} ");
         }

         Console.WriteLine();
         Console.WriteLine();
         var enc = IntegerEncoders.Base32Crockford;
         Console.WriteLine("Go!");

         foreach (var l in new NaiiveFibonacciSequence(1, 15000))
         {
            //Console.Write("x");
            var e = enc.Format(l);
            var d = enc.ParseBigInteger(e);

            if (l != d) Console.WriteLine($"ERROR: {l} -> {e} -> {d}");
         }

         Console.WriteLine();
         Console.WriteLine("Go! Go! Go!");
         var signedNum = int.MaxValue.ToString();
         var sw = new Stopwatch();
         sw.Reset();
         const int iterations = 10000;

         for (var q = 0; q < iterations; q++)
         {
            sw.Start();
            parsed = int.Parse(signedNum);
            sw.Stop();
            Thread.Sleep(0);
            var ix = parsed;
         }

         var ip = sw.ElapsedMilliseconds;
         sw.Reset();

         for (var q = 0; q < iterations; q++)
         {
            sw.Start();
            IntegerEncoders.Decimal.ParseInt32(signedNum);
            sw.Stop();
            Thread.Sleep(0);
         }

         var elapsedDecimalParse = sw.ElapsedMilliseconds;
         Console.WriteLine($"{signedNum} : int.Parse: {ip}   Decimal.ParseInt32: {elapsedDecimalParse}");
         Console.ReadKey();
      }
   }
}
