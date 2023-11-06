using Tools.BinaryTree;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void ToTest()
    {
        BinaryTree<int> binaryTree = new BinaryTree<int>(8);

        binaryTree.Root.Right = new BinaryTreeNode<int>(5);
        binaryTree.Root.Left = new BinaryTreeNode<int>(7);
        binaryTree.Root.Left.Right = new BinaryTreeNode<int>(14);
        binaryTree.Root.Left.Left = new BinaryTreeNode<int>(56);

        var a = binaryTree.GetLeaves();

        foreach (var VARIABLE in a)
        {
            print(VARIABLE.Value);
        }
    }
}