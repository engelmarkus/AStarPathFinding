using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding {
    static class ArrayExtensions {
        /// <summary>
        /// Vertauscht die Elemente mit den angegebenen Indizes.
        /// </summary>
        public static void Swap<T>(this T[] array, int i, int j) {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    struct KeyValuePair<K, V> {
        public K Key;
        public V Value;

        public KeyValuePair(K key, V value) {
            Key = key;
            Value = value;
        }
    }

    /// <summary>
    /// Ein binärer Heap.
    /// </summary>
    /// <typeparam name="T">Datentyp der Elemente, die auf dem Heap gespeichert werden sollen.</typeparam>
    class BinaryHeap<T> : IList<T> {
        private KeyValuePair<int, T>[] heap;

        public BinaryHeap() {
            heap = new KeyValuePair<int, T>[0];
        }

        private static int LeftChildOf(int i) {
            return 2 * i + 1;
        }

        private static int RightChildOf(int i) {
            return 2 * i + 2;
        }

        private static int ParentOf(int i) {
            return (i - 1) / 2;
        }

        /// <summary>
        /// Stellt nach dem Verändern der Elemente ggf. die Heap-Bedingung wieder her.
        /// </summary>
        /// <param name="i">Der Index des veränderten Elements.</param>
        private void Heapify(int i) {
            int x = i;

            if (LeftChildOf(i) < heap.Length && heap[LeftChildOf(i)].Key < heap[x].Key) {
                x = LeftChildOf(i);
            }

            if (RightChildOf(i) < heap.Length && heap[RightChildOf(i)].Key < heap[x].Key) {
                x = RightChildOf(i);
            }

            if (x != i) {
                heap.Swap(i, x);

                Heapify(x);
            }
        }

        /// <summary>
        /// Verringert den Schlüssel eines Elements auf dem Heap.
        /// </summary>
        /// <param name="i">Der Index des zu ändernden Elements.</param>
        /// <param name="newkey">Der neue Schlüssel des Elements.</param>
        public void Decrease(int i, int newkey) {
            heap[i].Key = newkey;

            while (i >= 0 && heap[i].Key < heap[ParentOf(i)].Key) {
                heap.Swap(i, ParentOf(i));
                i = ParentOf(i);
            }
        }

        /// <summary>
        /// Bestimmt, ob der Heap leer ist.
        /// </summary>
        public bool IsEmpty {
            get {
                return heap.Length == 0;
            }
        }

        /// <summary>
        /// Entfernt das Element mit der höchsten Priorität vom Heap und gibt es zurück.
        /// Dies ist nach der Heap-Bedingung die Wurzel.
        /// </summary>
        public KeyValuePair<int, T> ExtractMin() {
            var temp = GetMin();
            RemoveAt(0);

            return temp;
        }

        /// <summary>
        /// Gibt das Element mit der höchsten Priorität zurück.
        /// Dies ist nach der Heap-Bedingung die Wurzel.
        /// </summary>
        public KeyValuePair<int, T> GetMin() {
            return heap[0];
        }

        
        public int IndexOf(T item) {
            return Array.IndexOf(heap, item);
        }

        /// <summary>
        /// Fügt ein Element in den Heap ein.
        /// </summary>
        /// <param name="index">Die Priorität des neu einzufügenden Elements.</param>
        /// <param name="item">Das einzufügende Element.</param>
        public void Insert(int index, T item) {
            int i = heap.Length;

            Array.Resize(ref heap, i + 1);

            heap[i].Key = int.MaxValue;
            heap[i].Value = item;

            Decrease(i, index);
        }

        public void RemoveAt(int index) {
            KeyValuePair<int, T> temp = heap[index];
            int l = heap.Length - 1;

            heap.Swap(l, index);
            Array.Resize(ref heap, l);

            Heapify(index);
        }

        T IList<T>.this[int index] {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        void ICollection<T>.Add(T item) {
            throw new NotImplementedException();
        }

        void ICollection<T>.Clear() {
            heap = new KeyValuePair<int, T>[0];
        }

        bool ICollection<T>.Contains(T item) {
            return heap.Any(kvp => kvp.Value.Equals(item));
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        int ICollection<T>.Count {
            get {
                return heap.Length;
            }
        }

        bool ICollection<T>.IsReadOnly {
            get {
                return false;
            }
        }

        bool ICollection<T>.Remove(T item) {
            throw new NotImplementedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
