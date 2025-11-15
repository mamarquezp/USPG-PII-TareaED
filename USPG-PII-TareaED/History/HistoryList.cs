using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USPG_PII_TareaED.DobleLinkList; 

namespace USPG_PII_TareaED.History
{
    internal class HistoryList<T> where T : class 
    {
        private DoublyLinkedNode<T>? _current;
        public int Count { get; private set; }
        public int Capacity { get; set; } = 5; // Límite de historial

        public bool CanUndo => _current?.Prev != null;

        public bool CanRedo => _current?.Next != null;
        public HistoryList(T initialState)
        {
            _current = new DoublyLinkedNode<T>(initialState);
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
