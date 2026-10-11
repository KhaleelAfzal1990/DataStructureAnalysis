namespace DSALabsApi.Services
{
    /// <summary>
    /// Doubly Linked List — Tasks 04, 05.
    /// </summary>
    public class DoublyLinkedList
    {
        private class Node
        {
            public int Data;
            public Node? Prev;
            public Node? Next;
            public Node(int data) { Data = data; }
        }

        private Node? _head;
        private Node? _tail;
        private int _size;

        public int Size => _size;

        // ---------- Task 04: Traverse forward ----------
        public int[] TraverseForward()
        {
            var list = new System.Collections.Generic.List<int>();
            var curr = _head;
            while (curr != null) { list.Add(curr.Data); curr = curr.Next; }
            return list.ToArray();
        }

        // ---------- Task 04: Traverse backward ----------
        public int[] TraverseBackward()
        {
            var list = new System.Collections.Generic.List<int>();
            var curr = _tail;
            while (curr != null) { list.Add(curr.Data); curr = curr.Prev; }
            return list.ToArray();
        }

        // ---------- Task 04: Insert at beginning ----------
        public void InsertAtBeginning(int value)
        {
            var node = new Node(value);
            if (_head == null) { _head = _tail = node; }
            else
            {
                node.Next = _head;
                _head.Prev = node;
                _head = node;
            }
            _size++;
        }

        // ---------- Task 04: Insert at end ----------
        public void InsertAtEnd(int value)
        {
            var node = new Node(value);
            if (_tail == null) { _head = _tail = node; }
            else
            {
                node.Prev = _tail;
                _tail.Next = node;
                _tail = node;
            }
            _size++;
        }

        // ---------- Task 05: Insert at position ----------
        public (bool ok, string msg) InsertAtPosition(int pos, int value)
        {
            if (pos < 0 || pos > _size)
                return (false, $"Invalid position. Valid range: 0 to {_size}.");

            if (pos == 0) { InsertAtBeginning(value); return (true, "Inserted at 0."); }
            if (pos == _size) { InsertAtEnd(value); return (true, $"Inserted at {pos}."); }

            var curr = _head;
            for (int i = 0; i < pos; i++) curr = curr!.Next;

            var node = new Node(value);
            node.Prev = curr!.Prev;
            node.Next = curr;
            curr.Prev!.Next = node;
            curr.Prev = node;
            _size++;
            return (true, $"Inserted {value} at position {pos}.");
        }

        // ---------- Task 05: Delete from beginning ----------
        public (bool ok, string msg) DeleteFromBeginning()
        {
            if (_head == null) return (false, "Underflow: list is empty.");
            if (_head == _tail) { _head = _tail = null; }
            else
            {
                _head = _head.Next;
                _head!.Prev = null;
            }
            _size--;
            return (true, "Removed first node.");
        }

        // ---------- Task 05: Delete from end ----------
        public (bool ok, string msg) DeleteFromEnd()
        {
            if (_tail == null) return (false, "Underflow: list is empty.");
            if (_head == _tail) { _head = _tail = null; }
            else
            {
                _tail = _tail.Prev;
                _tail!.Next = null;
            }
            _size--;
            return (true, "Removed last node.");
        }

        // ---------- Task 05: Delete at position ----------
        public (bool ok, string msg) DeleteAtPosition(int pos)
        {
            if (_head == null) return (false, "Underflow: list is empty.");
            if (pos < 0 || pos >= _size)
                return (false, $"Invalid position. Valid range: 0 to {_size - 1}.");
            if (pos == 0) return DeleteFromBeginning();
            if (pos == _size - 1) return DeleteFromEnd();

            var curr = _head;
            for (int i = 0; i < pos; i++) curr = curr!.Next;

            curr!.Prev!.Next = curr.Next;
            curr.Next!.Prev = curr.Prev;
            _size--;
            return (true, $"Removed node at position {pos}.");
        }

        // ---------- Task 05: Search ----------
        public int Search(int key)
        {
            var curr = _head;
            int idx = 0;
            while (curr != null)
            {
                if (curr.Data == key) return idx;
                curr = curr.Next;
                idx++;
            }
            return -1;
        }

        // ---------- Task 05: Update at position ----------
        public (bool ok, string msg) UpdateAtPosition(int pos, int newValue)
        {
            if (pos < 0 || pos >= _size)
                return (false, $"Invalid position. Valid range: 0 to {_size - 1}.");
            var curr = _head;
            for (int i = 0; i < pos; i++) curr = curr!.Next;
            curr!.Data = newValue;
            return (true, $"Updated position {pos} to {newValue}.");
        }

        public void Clear() { _head = _tail = null; _size = 0; }
    }
}