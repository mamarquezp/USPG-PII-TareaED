using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USPG_PII_TareaED.DobleLinkList;

namespace USPG_PII_TareaED.DobleLinkList
{
    internal class HistoryList<T> where T : class 
    {
        private DoublyLinkedNode<T>? head;
        private DoublyLinkedNode<T>? _current;
        public int Count { get; private set; }

        public int Capacity { get; set; } = 100; 

        public bool CanUndo => _current?.Prev != null;

        public bool CanRedo => _current?.Next != null;
        public T? CurrentState
        {
            get { return _current?.Data; }
        }

        public HistoryList(T initialState)
        {
            head = _current = new DoublyLinkedNode<T>(initialState);
            Count = 1;
        }
        public void Push(T value)
        {
            var newNode = new DoublyLinkedNode<T>(value);
            if (_current.Next != null)
            {
                var temp = _current.Next;
                while (temp != null)
                {
                    Count--;
                    temp = temp.Next;
                }
            }

            newNode.Prev = _current;
            _current.Next = newNode;
            _current = newNode;
            Count++;
            while (Count > (Capacity + 1) && head != null) //limita cantidad de Undo
            {
                head = head.Next;

                if (head != null)
                {
                    head.Prev = null;
                }
                Count--;
            }
        }


public T UndoOrDefault(T defaultValue)
        {
            if (!CanUndo)
            {
                return defaultValue;
            }
            _current = _current.Prev;
            return _current.Data;
        }

        public T RedoOrDefault(T defaultValue)
        {
            if (!CanRedo)
            {
                return defaultValue;
            }
            _current = _current.Next;
            return _current.Data;
        }
    }
}
