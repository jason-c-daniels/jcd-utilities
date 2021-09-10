using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcd.Utilities.Visitors
{
   public abstract class BfsVisitor<TNode>
   {
      protected Action<TNode, TNode> VisitEnqueueAction;
      protected Action<TNode, TNode> VisitDequeueAction;
      protected Action<TNode, TNode> VisitInOrderAction;
      protected Func<TNode, TNode, bool> SkipPredicate;
      protected BfsVisitor(
         Action<TNode, TNode> visitEnqueueAction=null,
         Action<TNode, TNode> visitDequeueAction=null,
         Action<TNode, TNode> visitInOrderAction=null,
         Func<TNode, TNode, bool> skipPredicate=null
         )
      {
         VisitDequeueAction = visitDequeueAction;
         VisitEnqueueAction = visitEnqueueAction;
         VisitInOrderAction = visitInOrderAction;
         SkipPredicate = skipPredicate;
      }
      public void Traverse(TNode root)
      {
         if (root == null) return;

         //Since queue is a interface
         var queue = new Queue<TNode>();
         var visited = new HashSet<TNode>
         {
            root
         };
         var pr = default(TNode);
         //Adds to end of queue
         VisitInOrderAction?.Invoke(root, pr);
         VisitEnqueueAction?.Invoke(root, pr);
         queue.Enqueue(root);
         // TODO: adjust this so we know when we know the parent node. (use a stack?)
         while (queue.Any())
         {
            var r = queue.Dequeue();
            VisitDequeueAction?.Invoke(r,pr);

            foreach (var n in EnumerateChildren(r))
            {
               if (SkipPredicate?.Invoke(n,r) ?? false) continue;
               // visit each child with the in-order action regardless if it's already been previously enqueued.
               VisitInOrderAction?.Invoke(n,r);
               if (!visited.Contains(n))
               {
                  VisitEnqueueAction?.Invoke(n,r);
                  queue.Enqueue(n);
                  visited.Add(n);
               }
            }

            pr = r;
         }
      }

      public abstract IEnumerable<TNode> EnumerateChildren(TNode node);
   }
}
