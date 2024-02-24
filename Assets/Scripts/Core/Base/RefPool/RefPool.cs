// ******************************************************************
//       /\ /|       @file       RefPool.cs
//       \ V/        @brief      引用池
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-03-20 05:21:11
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;

namespace Rabi
{
    public class RefPool
    {
        private readonly Stack<IPoolRef> _refs = new Stack<IPoolRef>(); //池内引用实例

        public void Push(IPoolRef poolRef)
        {
            poolRef.OnReset();
            _refs.Push(poolRef);
        }

        public IPoolRef Pop()
        {
            return _refs.Count <= 0 ? null : _refs.Pop();
        }

        public void ReleaseAll()
        {
            _refs.Clear();
        }
    }
}