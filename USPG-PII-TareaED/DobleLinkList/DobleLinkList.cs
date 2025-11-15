using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USPG_PII_TareaED.DobleLinkList
{
    internal class DoublyLinkedList<T>
    {
        private DoublyLinkedNode<T>? head, tail;
        public int Count { get; private set; }
        public bool IsEmpty => head is null;

        public void AddFirst(T item)
        {
            var n = new DoublyLinkedNode<T>(item) { Next = head };
            if (head is not null) head.Prev = n; else tail = n;
            head = n; Count++;
        }

        public void AddLast(T item)
        {
            var n = new DoublyLinkedNode<T>(item) { Prev = tail };
            if (tail is not null) tail.Next = n; else head = n;
            tail = n; Count++;
        }

        public bool Remove(Predicate<T> match)
        {
            for (var c = head; c is not null; c = c.Next)
            {
                if (!match(c.Data)) continue;
                if (c.Prev is not null) c.Prev.Next = c.Next; else head = c.Next;
                if (c.Next is not null) c.Next.Prev = c.Prev; else tail = c.Prev;
                Count--; return true;
            }
            return false;
        }

        public string DumpForward()
        {
            var sb = new System.Text.StringBuilder().Append('[');
            for (var c = head; c is not null; c = c.Next) { sb.Append(c.Data); if (c.Next is not null) sb.Append(", "); }
            return sb.Append(']').ToString();
        }

        public string DumpBackward()
        {
            var sb = new System.Text.StringBuilder().Append('[');
            for (var c = tail; c is not null; c = c.Prev) { sb.Append(c.Data); if (c.Prev is not null) sb.Append(", "); }
            return sb.Append(']').ToString();
        }
    }
}
