using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This code was adapted from GeeksforGeeks
// Name of Website: Insertion in an AVL Tree
// Date Updated: 8 August 2024
// Link: https://www.geeksforgeeks.org/insertion-in-an-avl-tree/
// Date Accessed: 14 November 2023

namespace CitiConnect
{
    // AVL Tree class to maintain a balanced binary search tree
    public class AVLTree
    {
        // Root node of the AVL tree
        public ServiceRequestNode Root;

        // Function to get the height of a node
        // This helps in calculating the balance factor and ensuring the tree stays balanced
        private int Height(ServiceRequestNode node) => node == null ? 0 : Math.Max(Height(node.Left), Height(node.Right)) + 1;

        // Function to perform a right rotation on the subtree rooted at 'y'
        // Right rotation is used to balance the tree when the left subtree is heavier
        private ServiceRequestNode RotateRight(ServiceRequestNode y)
        {
            ServiceRequestNode x = y.Left; 
            ServiceRequestNode T2 = x.Right; 
            x.Right = y; 
            y.Left = T2; 
            return x; 
        }

        // Function to perform a left rotation on the subtree rooted at 'x'
        // Left rotation is used to balance the tree when the right subtree is heavier
        private ServiceRequestNode RotateLeft(ServiceRequestNode x)
        {
            ServiceRequestNode y = x.Right; 
            ServiceRequestNode T2 = y.Left; 
            y.Left = x; 
            x.Right = T2; 
            return y; 
        }

        // Function to calculate the balance factor of a node
        private int BalanceFactor(ServiceRequestNode node) => node == null ? 0 : Height(node.Left) - Height(node.Right);

        // Function to insert a new service request in the AVL tree
        public ServiceRequestNode Insert(ServiceRequestNode node, ServiceRequestNode newRequest)
        {
            if (node == null) return newRequest;

            // Insert in the left or right subtree based on the ID
            if (newRequest.Id < node.Id)
                node.Left = Insert(node.Left, newRequest);
            else if (newRequest.Id > node.Id)
                node.Right = Insert(node.Right, newRequest);
            else
                return node; // Duplicate IDs are not allowed

            // Calculate the balance factor to check if the node is unbalanced
            int balance = BalanceFactor(node);

            // Perform rotations to balance the tree
            if (balance > 1 && newRequest.Id < node.Left.Id) 
                return RotateRight(node);
            if (balance < -1 && newRequest.Id > node.Right.Id) 
                return RotateLeft(node);
            if (balance > 1 && newRequest.Id > node.Left.Id) 
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && newRequest.Id < node.Right.Id) 
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }
    }
}
