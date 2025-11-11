using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiConnect
{
    // This code was adapted from Design Gurus
    // Name of the Website: How to implement Heap data structure in C#?
    // Link: https://www.designgurus.io/answers/detail/how-to-implement-heap-data-structure-in-c

    // Class representing a MinHeap data structure
    public class MinHeap
    {
        // List to store heap elements
        private List<ServiceRequestNode> heap;

        // Constructor to initialize the heap
        public MinHeap() => heap = new List<ServiceRequestNode>();

        // Add a new node to the heap
        public void Add(ServiceRequestNode node)
        {
            heap.Add(node); // Add the new node at the end
            int i = heap.Count - 1;

            while (i > 0 && heap[(i - 1) / 2].RequestDate > heap[i].RequestDate)
            {
                // Swap the current node with its parent if it's smaller
                (heap[(i - 1) / 2], heap[i]) = (heap[i], heap[(i - 1) / 2]);
                i = (i - 1) / 2; 
            }
        }

        // Extract the smallest element (root) from the heap
        public ServiceRequestNode ExtractMin()
        {
            if (heap.Count == 0) return null; // Return null if the heap is empty

            var min = heap[0]; 
            heap[0] = heap[heap.Count - 1]; 
            heap.RemoveAt(heap.Count - 1);
            MinHeapify(0); 
            return min; 
        }

        // Helper method to maintain the MinHeap property
        private void MinHeapify(int i)
        {
            int smallest = i;
            int left = 2 * i + 1; 
            int right = 2 * i + 2; 

            // Compare the current node with its left and right children
            if (left < heap.Count && heap[left].RequestDate < heap[smallest].RequestDate)
                smallest = left;
            if (right < heap.Count && heap[right].RequestDate < heap[smallest].RequestDate)
                smallest = right;

            // If the current node is not the smallest, swap and continue heapifying
            if (smallest != i)
            {
                (heap[i], heap[smallest]) = (heap[smallest], heap[i]);
                MinHeapify(smallest); 
            }
        }
    }
}
