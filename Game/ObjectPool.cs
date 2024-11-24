using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    
    public class ObjectPool<T> where T : IPoolable,new()
    {
        private readonly List<T> availableObjects = new List<T>();
        private readonly List<T> inUseObjects = new List<T>();

        public T GetObject()
        {
            lock (availableObjects)
            {
                if (availableObjects.Count > 0)
                {
                    T obj = availableObjects[availableObjects.Count - 1];
                    availableObjects.RemoveAt(availableObjects.Count - 1);
                    inUseObjects.Add(obj);
                    return obj;
                }
                else
                {
                    T newObj = new T();
                    inUseObjects.Add(newObj);
                    return newObj;
                }
            }
        }

        public void ReleaseObject(T obj)
        {
            lock (availableObjects)
            {
                obj.Reset();
                inUseObjects.Remove(obj);
                availableObjects.Add(obj);
            }
        }
    }

}
