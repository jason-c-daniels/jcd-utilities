using System;
using System.Collections.Generic;

namespace Jcd.Utilities
{
   public static class Algorithm
   {
      public static void DFS<T>(T node,
         Func<T, IEnumerable<T>> edgeAccessor,
         Action<T, T> pre = null,
         Action<T, T> @in = null,
         Action<T, T> post = null,
         T parent = default(T),
         HashSet<T> visited = null)
         where T: class
      {
         if (node == null) return;
         if (visited == null) visited = new HashSet<T>();
         visited.Add(node);
         pre?.Invoke(node, parent);

         
         var edges = edgeAccessor(node);
         try
         {
            foreach (var edge in edges)
            {
               if (visited.Contains(edge)) continue;
               DFS(edge, edgeAccessor, pre, @in, post, node, visited);
               @in?.Invoke(edge, node);
            }
         }
         finally
         {
            (edges as IDisposable)?.Dispose();
         }

         post?.Invoke(node, parent);
      }

      public delegate void MappedDfsVisitor<in T, in T2>(T node, T parent, T2 mappedNode, T2 mappedParent);

      public static T2 DFSMap<T, T2>(T node,
         Func<T, IEnumerable<T>> edgeAccessor,
         Func<T, T2> map,
         MappedDfsVisitor<T, T2> pre = null,
         MappedDfsVisitor<T, T2> @in = null,
         MappedDfsVisitor<T, T2> post = null,
         T parent = default(T),
         T2 mappedParent = default(T2),
         HashSet<T> visited = null)
      {
         if (node == null) return default(T2);
         if (visited == null) visited = new HashSet<T>();
         visited.Add(node);
         var mapped = map(node);
         pre?.Invoke(node, parent, mapped, mappedParent);

         var edges = edgeAccessor(node);
         try
         {
            foreach (var edge in edges)
            {
               if (visited.Contains(edge)) continue;
               var mappedEdge=DFSMap(edge, edgeAccessor, map, pre, @in, post, node, mapped, visited);
               @in?.Invoke(edge, node, mappedEdge, mapped);
            }
         }
         finally
         {
            (edges as IDisposable)?.Dispose();
         }
         
         post?.Invoke(node, parent, mapped, mappedParent);
         return mapped;
      }
   }
}