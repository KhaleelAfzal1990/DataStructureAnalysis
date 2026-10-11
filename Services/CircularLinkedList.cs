namespace DSALabsApi.Services
{
    /// <summary>
    /// Circular Singly Linked List — Task 06.
    /// The last node's Next points back to the head.
    /// </summary>
    public class CircularLinkedList
    {
        private class Node
        {
            public int Data;
            public Node? Next;
            public Node(int data) { Data = data; }
        }

        private Node? _head;
        private int _size;

        public int Size => _size;

        // ---------- Task 06: Traverse (safe from infinite loop) ----------
        public int[] Traverse()
        {
            var result = new System.Collections.Generic.List<int>();
            if (_head == null) return result.ToArray();

            var curr = _head;
            do
            {
                result.Add(curr!.Data);
                curr = curr.Next;
            } while (curr != _head);  // stop when we loop back

            return result.ToArray();
        }

        // ---------- Task 06: Insert at beginning ----------
        public void InsertAtBeginning(int value)
        {
            var node = new Node(value);
            if (_head == null)
            {
                _head = node;
                node.Next = node;   // points to itself
            }
            else
            {
                // Find last node (its Next is _head)
                var last = _head;
                while (last.Next != _head) last = last.Next!;
                node.Next = _head;
                last.Next = node;
                _head = node;
            }
            _size++;
        }

        // ---------- Task 06: Insert at end ----------
        public void InsertAtEnd(int value)
        {
            var node = new Node(value);
            if (_head == null)
            {
                _head = node;
                node.Next = node;
            }
            else
            {
                var last = _head;
                while (last.Next != _head) last = last.Next!;
                last.Next = node;
                node.Next = _head;
            }
            _size++;
        }

        // ---------- Task 06: Delete from beginning ----------
        public (bool ok, string msg) DeleteFromBeginning()
        {
            if (_head == null) return (false, "Underflow: list is empty.");
            if (_head.Next == _head) { _head = null; _size = 0; return (true, "Removed only node."); }

            var last = _head;
            while (last.Next != _head) last = last.Next!;
            _head = _head.Next;
            last.Next = _head;
            _size--;
            return (true, "Removed first node.");
        }

        // ---------- Task 06: Delete from end ----------
        public (bool ok, string msg) DeleteFromEnd()
        {
            if (_head == null) return (false, "Underflow: list is empty.");
            if (_head.Next == _head) { _head = null; _size = 0; return (true, "Removed only node."); }

            var curr = _head;
            while (curr.Next!.Next != _head) curr = curr.Next;
            curr.Next = _head;   // drop the last node
            _size--;
            return (true, "Removed last node.");
        }

        // ---------- Task 06: Search ----------
        public int Search(int key)
        {
            if (_head == null) return -1;
            var curr = _head;
            int idx = 0;
            do
            {
                if (curr!.Data == key) return idx;
                curr = curr.Next;
                idx++;
            } while (curr != _head);
            return -1;
        }

        public void Clear() { _head = null; _size = 0; }
    }
}