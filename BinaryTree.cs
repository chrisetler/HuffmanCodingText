using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HuffmanCodingText
{
    public class BinaryTree<T> : Tree<T>
    {
        public const int LEFT_CHILD = 1;
        public const int RIGHT_CHILD = 0;
        private List<T> leftChild = new List<T>();
        private List<T> rightChild = new List<T>();
       
        public BinaryTree(List<T> nodes)
        {
            values = nodes;
            for (int i = 0; i < values.Count; i++)
            {
                parentIndex.Insert(i, i); // set the parent of everything to itself
                leftChild.Insert(i, values[i]); //set its child to itself 
                rightChild.Insert(i, values[i]);
            }
        }
        public void addChild(T obj, T child, int side)
        {
            //remove the child that is currently there
            if (hasChild(obj, side))
            {
                int indexOfNewlyParentLessObj = values.IndexOf(getChild(obj, side));
                parentIndex[indexOfNewlyParentLessObj] = indexOfNewlyParentLessObj;
            }
            //add the new child
            if (side == LEFT_CHILD)
            {
                int pindex = values.IndexOf(obj);
                leftChild[pindex] = child;
                parentIndex[values.IndexOf(child)] = pindex;
                //leftChild.Insert(parentIndex, child);
            }
            else if (side == RIGHT_CHILD)
            {
                int pindex = values.IndexOf(obj);
                rightChild[pindex] = child;
                parentIndex[values.IndexOf(child)] = pindex;
                //rightChild.Insert(parentIndex, child);
            }
        }
        public T getChild(T obj, int side)
        {
            //get the child at this spot
            int indexOfParent = values.IndexOf(obj);
            if (side == LEFT_CHILD)
            {
                try
                {
                    return leftChild[indexOfParent];
                }
                catch
                {
                    return default(T);
                }
               
            }
            else if (side == RIGHT_CHILD)
            {
                try
                {
                    return rightChild[indexOfParent];
                }
                catch
                {
                    return default(T);
                }
            }
            return default(T);


        }
        public bool hasChild(T obj, int side)
        {
            if (getChild(obj,side).Equals(obj)){
                return false;
            }
            return true;
        }
        //overrides parent 
        public void removeNode(T obj)
        {
            //remove the first index of the object
            int indexOfRemovedValue = values.IndexOf(obj);
            if (indexOfRemovedValue != -1)
            {
                values.RemoveAt(indexOfRemovedValue);
                parentIndex.RemoveAt(indexOfRemovedValue);
                leftChild.RemoveAt(indexOfRemovedValue);
                rightChild.RemoveAt(indexOfRemovedValue);
                //remove the parent of any object linking to the removed parent
                //further, anything whose parent was an index greater than i now needs to be shifted down by one
                for (int i = 0; i < values.Count; i++)
                {
                    if (parentIndex[i] == indexOfRemovedValue) parentIndex[i] = i;
                    if (parentIndex[i] > indexOfRemovedValue) parentIndex[i]--;
                }
            }
        }
        //overrides parent
        //tries to add the child to the left, if not adds it to the right.
        public void setParent(T obj, T objParent)
        {
            //set the parent of the object
            int indexOfParent = values.IndexOf(objParent);
            int indexOfObj = values.IndexOf(obj);
            parentIndex[indexOfObj] = indexOfParent;
            if (hasChild(objParent, LEFT_CHILD))
            {
                addChild(objParent, obj, RIGHT_CHILD);
            }
            else
            {
                addChild(objParent, obj, LEFT_CHILD);
            }
        }

    }
}