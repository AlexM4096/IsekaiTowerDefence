using System.Collections.Generic;

namespace Tools.BinaryTree
{
    public static class BinaryTreeTools
    {
        public static IEnumerable<BinaryTreeNode<T>> GetLeaves<T>(this BinaryTree<T> binaryTree)
        {
            List<BinaryTreeNode<T>> list = new();
            
            Stack<BinaryTreeNode<T>> stack = new();

            BinaryTreeNode<T> root = binaryTree.Root;
            stack.Push(root);

            while (stack.Count > 0)
            {
                root = stack.Peek();
                stack.Pop();

                BinaryTreeNode<T> right = root.Right;
                BinaryTreeNode<T> left = root.Left;
                
                if (right == null && left == null)
                    list.Add(root);

                if (right != null)
                    stack.Push(right);
            
                if (left != null)
                    stack.Push(left);
            }

            return list;
        }
    }
}