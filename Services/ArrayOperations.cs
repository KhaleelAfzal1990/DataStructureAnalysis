// Imports the System.Collections.Generic namespace so we can use List<int>
using System.Collections.Generic;

// Everything in this file lives inside the DSALabsApi.Services namespace.
// This MUST match the using statement in Program.cs and the controllers.
namespace DSALabsApi.Services
{
    /// <summary>
    /// Fixed-size array operations (Tasks 01–04).
    /// Registered as a Singleton so state persists across HTTP requests.
    /// </summary>
    // 'public' = accessible from other files (controllers, Program.cs)
    // 'class'  = a reference type that holds data + methods
    public class ArrayOperations
    {
        // ---------- FIELDS (private data) ----------

        // The actual array. 'readonly' = the reference can't be reassigned after
        // the constructor runs (contents can still change, but _arr itself always
        // points to the same array).
        private readonly int[] _arr;

        // Tracks how many slots are currently in use.
        // Starts at 0 (empty). Always <= Capacity.
        private int _size;

        // Public read-only property exposing Capacity to the outside world.
        // 'get;' only (no 'set;') → external code can read but not change it.
        public int Capacity { get; }

        // ---------- CONSTRUCTOR ----------

        // Runs when 'new ArrayOperations(15)' is called.
        // 'capacity = 15' → optional parameter; if omitted, defaults to 15.
        public ArrayOperations(int capacity = 15)
        {
            Capacity = capacity;              // store capacity (immutable)
            _arr = new int[capacity];         // allocate the backing array
            _size = 0;                        // nothing is in use yet
        }

        // ---------- PROPERTIES ----------

        // Expression-bodied property → equivalent to:
        //   public int Size { get { return _size; } }
        // External code can read the current number of elements.
        public int Size => _size;

        // Returns a COPY of the in-use portion of the array ([0 .. _size-1]).
        // We return a copy (not _arr itself) so callers can't accidentally
        // modify our internal state — this is encapsulation.
        public int[] Snapshot()
        {
            var copy = new int[_size];           // allocate exactly _size ints
            System.Array.Copy(_arr, copy, _size); // copy first _size elements
            return copy;                          // hand the copy back
        }

        // ================= TASK 01 =================

        // Insert at end. Time complexity: O(1) amortized.
        public bool InsertAtEnd(int value)
        {
            // If array is full → refuse (overflow)
            if (_size >= Capacity) return false;

            // Place value at index '_size', THEN increment _size.
            // '_size++' is post-increment → uses old value first, then adds 1.
            _arr[_size++] = value;

            return true; // success
        }

        // Insert at beginning. Time complexity: O(n) because every
        // existing element must shift one slot to the right.
        public bool InsertAtBeginning(int value)
        {
            if (_size >= Capacity) return false;

            // Shift elements from right to left:
            // i = _size  → copy from _arr[_size-1] into _arr[_size]
            // i-- continues down to i=1.
            // After loop, slot 0 is free.
            for (int i = _size; i > 0; i--) _arr[i] = _arr[i - 1];

            _arr[0] = value; // put new value at the front
            _size++;         // one more element
            return true;
        }

        // ================= TASK 02 =================

        // Insert at arbitrary position.
        // Returns a TUPLE (bool ok, string msg) so caller knows result + reason.
        public (bool ok, string msg) InsertAtPosition(int pos, int value)
        {
            if (_size >= Capacity)
                return (false, "Overflow: array is full.");

            // Valid positions are 0 .. _size inclusive:
            //   pos = 0       → insert at front
            //   pos = _size   → insert at end (same as InsertAtEnd)
            //   pos > _size   → invalid (would leave a gap)
            if (pos < 0 || pos > _size)
                return (false, $"Invalid position. Valid range: 0 to {_size}.");

            // Shift elements right, starting from the end, to make a gap at 'pos'.
            // $"" is string interpolation — embeds _size inside the message.
            for (int i = _size; i > pos; i--) _arr[i] = _arr[i - 1];

            _arr[pos] = value;  // place new value in the gap
            _size++;            // array grew by one
            return (true, "Inserted.");
        }

        // Delete element at arbitrary position.
        public (bool ok, string msg) DeleteAtPosition(int pos)
        {
            // Can't delete from empty array
            if (_size == 0)
                return (false, "Underflow: array is empty.");

            // Valid positions for DELETE are 0 .. _size-1 (strictly less than _size)
            if (pos < 0 || pos >= _size)
                return (false, $"Invalid position. Valid range: 0 to {_size - 1}.");

            // Shift everything AFTER pos one slot left to close the gap.
            for (int i = pos; i < _size - 1; i++) _arr[i] = _arr[i + 1];

            _size--; // array shrank by one (old last element is now "garbage")
            return (true, "Deleted.");
        }

        // Delete from end. Time complexity: O(1) — just decrement _size.
        public bool DeleteFromEnd()
        {
            if (_size == 0) return false;
            _size--;        // old value at _arr[_size] is now ignored
            return true;
        }

        // ================= TASK 04 =================

        // Delete from beginning. Time complexity: O(n) — shift everything left.
        public bool DeleteFromBeginning()
        {
            if (_size == 0) return false;

            // Copy each element one slot to the left.
            for (int i = 0; i < _size - 1; i++) _arr[i] = _arr[i + 1];

            _size--;
            return true;
        }

        // ================= TASK 03 =================

        // Update value at position. Time complexity: O(1) — direct index write.
        public bool UpdateAtPosition(int pos, int newValue)
        {
            // Strictly must be inside the in-use range
            if (pos < 0 || pos >= _size) return false;
            _arr[pos] = newValue;
            return true;
        }

        // Linear search → returns FIRST index of key, or -1 if not found.
        // Time complexity: O(n) worst case.
        public int LinearSearch(int key)
        {
            for (int i = 0; i < _size; i++)
                if (_arr[i] == key) return i;   // found → return index
            return -1;                          // not found
        }

        // Bonus: return ALL indices where 'key' appears as a List<int>.
        // Still O(n), but collects every occurrence instead of stopping early.
        public List<int> LinearSearchAll(int key)
        {
            var list = new List<int>();         // empty list
            for (int i = 0; i < _size; i++)
                if (_arr[i] == key) list.Add(i); // append index
            return list;                         // empty list if none found
        }

        // ================= HELPER =================

        // Seed helper for demos. 'params int[]' means callers can pass
        // any number of ints: Seed(10, 20, 30) OR Seed(new[]{10,20,30}).
        public void Seed(params int[] values)
        {
            foreach (var v in values)           // 'var' infers int from array
            {
                if (_size < Capacity) _arr[_size++] = v; // add if room
                else break;                              // stop if full
            }
        }
    }
}