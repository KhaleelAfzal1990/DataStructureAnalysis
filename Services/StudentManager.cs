// Imports System.Collections.Generic so we can write List<int> if needed.
// (Not actually used in this file, but harmless to keep — matches the
// pattern from ArrayOperations.cs and avoids removing usings when expanding.)
using System.Collections.Generic;

// All classes in this file belong to the DSALabsApi.Services namespace.
// This MUST match the 'using ArrayLabApi.Services;' line at the top of Program.cs.
namespace DSALabsApi.Services
{
    /// <summary>
    /// Task 05: Student roll number manager with fixed capacity 20.
    /// </summary>
    // 'public' → visible from other files (controllers, Program.cs).
    // 'class'  → reference type with state (rolls + size) and behavior (methods).
    public class StudentManager
    {
        // ---------- FIELDS (private data) ----------

        // 'const' = compile-time constant. Value (20) is baked in and can
        // never change. Naming convention: PascalCase for const fields.
        private const int Capacity = 20;

        // The backing array. 'readonly' = the reference can't be reassigned
        // after initialization, but the array's contents can still change.
        // Using an inline initializer: '= new int[Capacity]' allocates 20 slots.
        private readonly int[] _rolls = new int[Capacity];

        // Tracks how many students are currently enrolled.
        // Starts at 0. Always <= Capacity. '_' prefix = private field convention.
        private int _size = 0;

        // ---------- PROPERTIES ----------

        // Expression-bodied property → shorthand for a get-only property.
        // External code reads the current number of enrolled students.
        public int Size => _size;

        // ---------- SNAPSHOT (safe read) ----------

        // Returns a COPY of the used portion [0 .. _size-1] of the backing array.
        // Returning a copy (not '_rolls' itself) prevents external code from
        // mutating our internal state — this is encapsulation.
        public int[] Snapshot()
        {
            var copy = new int[_size];               // allocate exactly _size slots
            System.Array.Copy(_rolls, copy, _size);  // copy first _size elements
            return copy;                             // return the copy
        }

        // ---------- ADD (Task 05: insert new roll at end) ----------

        // Adds a new roll number at the end of the list.
        // Returns a tuple (bool ok, string msg) so the controller can build
        // the correct HTTP response (200 OK vs 400 Bad Request).
        public (bool ok, string msg) Add(int roll)
        {
            // If we've hit 20 students → class is full (overflow case)
            if (_size >= Capacity)
                return (false, "Class full! Cannot add more students.");

            // Place roll at index '_size', then increment _size.
            // '_size++' is post-increment: uses old _size as index, then +1.
            _rolls[_size++] = roll;

            // $"" is string interpolation — embeds the value of 'roll' inside the text.
            return (true, $"Student {roll} enrolled.");
        }

        // ---------- FIND (internal helper — linear search) ----------

        // Returns the INDEX of 'roll' in _rolls, or -1 if not present.
        // Time complexity: O(n) in the worst case.
        public int Find(int roll)
        {
            // Iterate over only the in-use portion of the array.
            for (int i = 0; i < _size; i++)
                if (_rolls[i] == roll) return i;  // found → return index immediately
            return -1;                            // not found
        }

        // ---------- DELETE (Task 05: student drops) ----------

        // Removes a student by roll number (search + shift-left delete).
        public (bool ok, string msg) Delete(int roll)
        {
            // Edge case: nothing to delete
            if (_size == 0)
                return (false, "No students to delete.");

            // Find the position of the roll number first
            int pos = Find(roll);

            // If Find returned -1, the roll doesn't exist
            if (pos == -1)
                return (false, $"Roll number {roll} not found.");

            // Shift every element AFTER 'pos' one slot to the left.
            // Starts at i = pos, ends at i = _size-2 (inclusive).
            // Copies _rolls[i+1] into _rolls[i] each iteration.
            for (int i = pos; i < _size - 1; i++)
                _rolls[i] = _rolls[i + 1];

            // Shrink the logical size by one. The old last element is now "garbage"
            // (outside _size) and will be overwritten by the next Add().
            _size--;

            return (true, $"Student {roll} removed (dropped).");
        }

        // ---------- UPDATE (Task 05: correct a roll number) ----------

        // Replaces an existing roll number with a new one (in place, O(1) after Find).
        public (bool ok, string msg) Update(int oldRoll, int newRoll)
        {
            // Look up the current index of the old roll number
            int pos = Find(oldRoll);

            // If it doesn't exist → nothing to update
            if (pos == -1)
                return (false, $"Roll number {oldRoll} not found.");

            // Overwrite the value at 'pos' with the new roll number
            _rolls[pos] = newRoll;

            // '->' is just text here — shows old and new values in the message
            return (true, $"Roll number updated: {oldRoll} -> {newRoll}.");
        }

        // ---------- SEARCH (Task 05: attendance verification) ----------

        // Public search API. Returns (found, position) so the controller
        // can build the right response (200 OK with index vs 404 Not Found).
        public (bool found, int pos) Search(int roll)
        {
            // Reuse the internal Find helper
            int pos = Find(roll);

            // 'pos != -1' → true if roll was found, false otherwise.
            // Return the tuple — the caller can read .found and .pos.
            return (pos != -1, pos);
        }
    }
}