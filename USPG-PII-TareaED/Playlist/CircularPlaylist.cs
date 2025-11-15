using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using USPG_PII_TareaED.DobleLinkList;
using USPG_PII_TareaED.Model;

namespace USPG_PII_TareaED.Playlist
{
    internal class CircularPlaylist
    {
        private DoublyLinkedNode<Song>? _current;

        public int Count { get; private set; }
        public bool IsEmpty => _current is null;

        public Song? CurrentSong => _current?.Data;

        public void AddLast(Song s)
        {
            var newNode = new DoublyLinkedNode<Song>(s);

            if (IsEmpty)
            {
                _current = newNode;
                _current.Next = _current;
                _current.Prev = _current;
            }
            else
            {
                var tail = _current!.Prev!;

                newNode.Next = _current;
                newNode.Prev = tail;

                tail.Next = newNode;
                _current.Prev = newNode;
            }
            Count++;
        }
        public void Next()
        {
            if (_current is not null)
            {
                _current = _current.Next;
            }
        }
        public void Prev()
        {
            if (_current is not null)
            {
                _current = _current.Prev;
            }
        }
        public bool RemoveById(Guid id)
        {
            if (IsEmpty) return false;

            DoublyLinkedNode<Song>? nodeToFind = _current;
            do
            {
                if (nodeToFind!.Data.Id == id)
                {
                    if (Count == 1)
                    {
                        _current = null;
                    }
                    else
                    {
                        nodeToFind.Prev!.Next = nodeToFind.Next;
                        nodeToFind.Next!.Prev = nodeToFind.Prev;

                        if (_current == nodeToFind)
                        {
                            _current = nodeToFind.Next;
                        }
                    }
                    Count--;
                    return true;
                }
                nodeToFind = nodeToFind.Next;
            }
            while (nodeToFind != _current && nodeToFind is not null);
            return false;
        }
        public void Shuffle(int seed)
        {
            if (Count <= 1) return;

            var nodes = new List<DoublyLinkedNode<Song>>();
            var iterator = _current;
            do
            {
                nodes.Add(iterator!);
                iterator = iterator!.Next;
            }
            while (iterator != _current);

            var random = new Random(seed);
            int n = nodes.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (nodes[k], nodes[n]) = (nodes[n], nodes[k]);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                node.Prev = nodes[(i + nodes.Count - 1) % nodes.Count];
                node.Next = nodes[(i + 1) % nodes.Count];
            }

            _current = nodes[0];
        }
        public string ExportToJson()
        {
            if (IsEmpty) return "[]";

            var titles = new List<string>();
            var iterator = _current;
            do
            {
                titles.Add(iterator!.Data.Title);
                iterator = iterator.Next;
            }
            while (iterator != _current);

            // Opciones para formatear el JSON (indented)
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(titles, options);
        }
    }
}
