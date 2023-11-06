namespace Tools.BinaryTree
{
    public class BinaryTreeNode<T>
    {
        public T Value { get; private set; }
        
        public BinaryTreeNode<T> Right;
        public BinaryTreeNode<T> Left;

        public BinaryTreeNode(T value)
        {
            Value = value;
        }
    }
}