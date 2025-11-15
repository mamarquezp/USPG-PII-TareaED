using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USPG_PII_TareaED.DobleLinkList
{
    internal class DoublyLinkedNode<T>
    {
        public T Data;
        public DoublyLinkedNode<T>? Next;
        public DoublyLinkedNode<T>? Prev;
        public DoublyLinkedNode(T d)
        {
            Data = d;
        }
    }
}
