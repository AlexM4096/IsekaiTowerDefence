namespace Tools.BinaryTree
{
    public class BinaryTree<T>
    {
        public BinaryTreeNode<T> Root { get; private set; }

        public BinaryTree(BinaryTreeNode<T> root)
        {
            Root = root;
        }
        
        public BinaryTree(T value) : this(new BinaryTreeNode<T>(value)) {}
    }
}