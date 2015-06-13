using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HuffmanCodingText
{
    public class Tree<T>
    {
        protected List<T> values = new List<T>();
        protected List<int> parentIndex = new List<int>();
        public Tree()
        {

        }
        public Tree(List<T> nodes){
            values = nodes;
            for (int i=0; i<values.Count; i++){
                parentIndex.Insert(i,i); // set the parent of everything to itself
            }
        }
        //add an item to the tree
        public void addNode(T obj)
        {
            //add a node to the tree. give it no parent - i.e. make the parent itself
            values.Add(obj);
            parentIndex.Add(parentIndex.Count);

        }
        public void addNode(T obj, int parent)
        {
            //add a node with as set index of the parent
            values.Add(obj);
            this.parentIndex.Add(parent);

        }
        public void addNode(T obj, T parentObj)
        {
            //add a node with a set object as the parent.
            values.Add(obj);
            parentIndex.Add(values.IndexOf(parentObj));
        }
        public void removeNode(T obj)
        {
            //remove the first index of the object
            int indexOfRemovedValue = values.IndexOf(obj);
            if (indexOfRemovedValue != -1)
            {
                values.RemoveAt(indexOfRemovedValue);
                parentIndex.RemoveAt(indexOfRemovedValue);
                //remove the parent of any object linking to the removed parent
                //further, anything whose parent was an index greater than i now needs to be shifted down by one
                for (int i = 0; i < values.Count; i++)
                {
                    if (parentIndex[i] == indexOfRemovedValue) parentIndex[i] = i;
                    if (parentIndex[i] > indexOfRemovedValue) parentIndex[i]--;
                }
            }
        }
        public int getLevel(T obj)
        {
            int count = 0;
            while (!(getParent(obj).Equals(obj)))
            {
                count++;
                obj = getParent(getParent(obj));
            }
            return count;

        }
        public T getParent(T obj)
        {
            //return the parent of the first instance of hte object
            int indexOfObj = values.IndexOf(obj);
            int indexOfParent = parentIndex[indexOfObj];
            return values[indexOfParent];
        }
        public T getRoot(T obj) {
            //find the root of the object by ascending up the tree
            while (!(getParent(obj).Equals(obj)))
            {
                obj = getParent(getParent(obj));
            }
            return obj;
        }
        public bool hasParent(T obj)
        {
            //see if the object has a parent
            if (getParent(obj).Equals(obj)) return false;
            return true;
        }
        public void setParent(T obj, T objParent)
        {
            //set the parent of the object
            int indexOfParent = values.IndexOf(objParent);
            int indexOfObj = values.IndexOf(obj);
            parentIndex[indexOfObj] = indexOfParent;
        }
        public void removeParent(T obj)
        {
            //remove the parent of the object
            parentIndex[values.IndexOf(obj)] = values.IndexOf(obj);
        }

   
    }
}