using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.Utility
{
    [Serializable] // Marks the SLL class as serializable for file I/O operations
    public class SLL<T> where T : IComparable<T> // Generic class definition with constraint for comparable types
    {
        private class Node // Nested private class for list nodes
        {
            public T Data { get; set; } // Data element of the node
            public Node Next { get; set; } // Pointer to the next node in the list

            public Node(T data) // Node constructor initializing the data and setting Next to null
            {
                Data = data;
                Next = null;
            }
        }

        private Node head; // Head node of the list, marking the beginning
        private int count; // Counter for the number of elements in the list

        public SLL() // Constructor for SLL initializing an empty list
        {
            head = null;
            count = 0;
        }

        // Checks if the list is empty by comparing the count of elements to zero
        public bool IsEmpty()
        {
            return count == 0;
        }

        // Clears the list by setting the head to null and count to zero
        public void Clear()
        {
            head = null;
            count = 0;
        }

        // Appends a new element at the end of the list
        public void Append(T data)
        {
            if (head == null) // If the list is empty, set the head to the new node
            {
                head = new Node(data);
            }
            else // Otherwise, traverse to the end and append the new node there
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new Node(data);
            }
            count++;
        }

        // Prepends a new element at the beginning of the list
        public void Prepend(T data)
        {
            Node newNode = new Node(data);
            newNode.Next = head;
            head = newNode;
            count++;
        }

        // Inserts a new element at the specified index in the list
        public void InsertAt(int index, T data)
        {
            if (index < 0 || index > count)
                throw new IndexOutOfRangeException("Index out of range");
            if (index == 0)
                Prepend(data);
            else
            {
                Node newNode = new Node(data);
                Node current = head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                newNode.Next = current.Next;
                current.Next = newNode;
                count++;
            }
        }

        // Replaces the element at the specified index with a new value
        public void ReplaceAt(int index, T data)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index out of range");
            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            current.Data = data;
        }

        // Returns the number of elements in the list
        public int Count()
        {
            return count;
        }

        // Removes the element at the specified index
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index out of range");
            if (index == 0)
                head = head.Next;
            else
            {
                Node current = head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                current.Next = current.Next.Next;
            }
            count--;
        }

        // Retrieves the element at the specified index
        public T GetAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index out of range");
            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data;
        }

        // Finds the index of the given element, returning -1 if not found
        public int IndexOf(T data)
        {
            int index = 0;
            Node current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return index;
                current = current.Next;
                index++;
            }
            return -1;
        }

        // Checks if the list contains the specified element
        public bool Contains(T data)
        {
            return IndexOf(data) != -1;
        }

        // Reverses the order of elements in the list
        public void Reverse()
        {
            Node previous = null;
            Node current = head;
            Node next = null;
            while (current != null)
            {
                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }
            head = previous;
        }

        // Sorts the elements of the list in ascending order
        public void Sort()
        {
            if (count <= 1)
                return;

            bool swapped;
            do
            {
                swapped = false;
                Node current = head;
                Node previous = null;

                while (current.Next != null)
                {
                    if (current.Data.CompareTo(current.Next.Data) > 0)
                    {
                        T temp = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = temp;
                        swapped = true;
                    }
                    previous = current;
                    current = current.Next;
                }
            } while (swapped);
        }

        // Converts the list to an array
        public T[] ToArray()
        {
            T[] array = new T[count];
            Node current = head;
            int index = 0;
            while (current != null)
            {
                array[index++] = current.Data;
                current = current.Next;
            }
            return array;
        }

        // Concatenates another list to the end of this list
        public void Join(SLL<T> otherList)
        {
            if (otherList == null || otherList.IsEmpty())
                return;

            if (head == null)
            {
                head = otherList.head;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = otherList.head;
            }
            count += otherList.count;
            otherList.Clear(); // Optionally clear the other list after joining
        }

        // Splits the list into two parts at the specified index
        public (SLL<T>, SLL<T>) Divide(int index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");

            SLL<T> firstHalf = new SLL<T>();
            SLL<T> secondHalf = new SLL<T>();

            Node current = head;
            int currentIndex = 0;

            while (current != null)
            {
                if (currentIndex < index)
                    firstHalf.Append(current.Data);
                else
                    secondHalf.Append(current.Data);

                current = current.Next;
                currentIndex++;
            }

            return (firstHalf, secondHalf);
        }

        // Serializes the list to a file
        public void Serialize(string fileName)
        {
            try
            {
                using (FileStream stream = File.Create(fileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Serialization error: " + ex.Message);
            }
        }

        // Deserializes a list from a file
        public static SLL<T> Deserialize(string fileName)
        {
            try
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (SLL<T>)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deserialization error: " + ex.Message);
                return null;
            }
        }
    }
}
