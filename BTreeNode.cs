using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HuffmanCodingText
{
    public class BTreeNode<T>
    {
        public const int LEFT_SIDE = 0;
        public const int RIGHT_SIDE = 1;
        public T nodeValue;
        private BTreeNode<T> LeftChildNode;
        private BTreeNode<T> RightChildNode;
        private BTreeNode<T> ParentNode;
        public Boolean hasChild;
        public Boolean hasParent;
        public int onParentSide;
        
        public BTreeNode(T aNodeValue)
        {
            nodeValue = aNodeValue;
            hasChild = false;
            hasParent = false;
        }

        //Add left child
        public void addLeftChild(BTreeNode<T> aChild)
        {
            LeftChildNode = aChild;
            aChild.onParentSide = LEFT_SIDE;
            LeftChildNode.ParentNode = this;
            LeftChildNode.hasParent = true;
            this.hasChild = true;
        }
        //Add right child
        public void addRightChild(BTreeNode<T> aChild)
        {
            RightChildNode = aChild;
            aChild.onParentSide = RIGHT_SIDE;
            RightChildNode.ParentNode = this;
            RightChildNode.hasParent = true;
            this.hasChild = true;
        }
        //Get parent
        public BTreeNode<T> getParent()
        {
            return this.ParentNode;
        }
        //huffman transverse

        //Get root
        public BTreeNode<T> getRoot()
        {
            BTreeNode<T> aNode = this;
            while (aNode.hasParent)
            {
                aNode = aNode.ParentNode;
            }
            return aNode;
        }

        //to String
        public String toString()
        {
            String s = "Node {Value: ";
            s += nodeValue.ToString() + " ";
            s += "Left Child " + this.LeftChildNode.nodeValue.ToString() + " ";
            s += "Right Child " + this.RightChildNode.nodeValue.ToString() + " ";
            s += "Parent Node: " + this.ParentNode.nodeValue.ToString() + " ";
            return s;
        }

    }

}