using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public interface IObjectPool<T>
    {
        T Get();
        void Release(T obj);
    }
}

