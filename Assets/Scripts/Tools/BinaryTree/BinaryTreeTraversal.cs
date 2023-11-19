using System.Collections.Generic;

namespace Tools.BinaryTree
{
    public static class BinaryTreeTraversal
    {
        public static IEnumerable<BinaryTreeNode<T>> PreorderTraversal<T>(this BinaryTree<T> binaryTree)
        {
            BinaryTreeNode<T> root = binaryTree.Root;

            Stack<BinaryTreeNode<T>> stack = new();
            stack.Push(root);

            List<BinaryTreeNode<T>> list = new();

            while (stack.Count > 0)
            {
                root = stack.Peek();
                stack.Pop();
                
                list.Add(root);
                
                BinaryTreeNode<T> right = root.Right;
                BinaryTreeNode<T> left = root.Left;

                if (right != null)
                    stack.Push(right);
                
                if (left != null)
                    stack.Push(left);
            }

            return list;
        }
    }
}