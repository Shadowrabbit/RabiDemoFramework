// ******************************************************************
//       /\ /|       @file       EventManager.cs
//       \ V/        @brief      事件管理器
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-03-27 12:47:46
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;

namespace Rabi
{
    public sealed class EventManager : BaseSingleTon<EventManager>
    {
        public delegate void RabiEvent();

        public delegate void RabiEvent<in T1>(T1 t1);

        public delegate void RabiEvent<in T1, in T2>(T1 t1, T2 t2); //逆变

        public delegate void RabiEvent<in T1, in T2, in T3>(T1 t1, T2 t2, T3 t3);

        public delegate void RabiEvent<in T1, in T2, in T3, in T4>(T1 t1, T2 t2, T3 t3, T4 t4);

        //存储全部事件的字典
        private readonly Dictionary<int, Delegate> _eventDict = new Dictionary<int, Delegate>();

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        public void AddListener(int eventId, RabiEvent callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                _eventDict.Add(eventId, null);
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate != null && curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent) _eventDict[eventId] + callBack;
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        public void AddListener<T1>(int eventId, RabiEvent<T1> callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                _eventDict.Add(eventId, null);
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate != null && curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent<T1>) _eventDict[eventId] + callBack;
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public void AddListener<T1, T2>(int eventId, RabiEvent<T1, T2> callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                _eventDict.Add(eventId, null);
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate != null && curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent<T1, T2>) _eventDict[eventId] + callBack;
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        public void AddListener<T1, T2, T3>(int eventId, RabiEvent<T1, T2, T3> callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                _eventDict.Add(eventId, null);
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate != null && curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent<T1, T2, T3>) _eventDict[eventId] + callBack;
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        public void AddListener<T1, T2, T3, T4>(int eventId, RabiEvent<T1, T2, T3, T4> callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                _eventDict.Add(eventId, null);
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate != null && curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent<T1, T2, T3, T4>) _eventDict[eventId] + callBack;
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        public void AddListener(EventId eventId, RabiEvent callBack)
        {
            AddListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        public void AddListener<T1>(EventId eventId, RabiEvent<T1> callBack)
        {
            AddListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public void AddListener<T1, T2>(EventId eventId, RabiEvent<T1, T2> callBack)
        {
            AddListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        public void AddListener<T1, T2, T3>(EventId eventId, RabiEvent<T1, T2, T3> callBack)
        {
            AddListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        public void AddListener<T1, T2, T3, T4>(EventId eventId, RabiEvent<T1, T2, T3, T4> callBack)
        {
            AddListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        public void RemoveListener(int eventId, RabiEvent callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                return;
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate == null)
            {
                return;
            }

            if (curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent) _eventDict[eventId] - callBack;
            if (_eventDict[eventId] == null)
            {
                _eventDict.Remove(eventId);
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        public void RemoveListener<T1>(int eventId, RabiEvent<T1> callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                return;
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate == null)
            {
                return;
            }

            if (curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent<T1>) _eventDict[eventId] - callBack;
            if (_eventDict[eventId] == null)
            {
                _eventDict.Remove(eventId);
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public void RemoveListener<T1, T2>(int eventId, RabiEvent<T1, T2> callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                return;
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate == null)
            {
                return;
            }

            if (curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent<T1, T2>) _eventDict[eventId] - callBack;
            if (_eventDict[eventId] == null)
            {
                _eventDict.Remove(eventId);
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        public void RemoveListener<T1, T2, T3>(int eventId, RabiEvent<T1, T2, T3> callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                return;
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate == null)
            {
                return;
            }

            if (curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent<T1, T2, T3>) _eventDict[eventId] - callBack;
            if (_eventDict[eventId] == null)
            {
                _eventDict.Remove(eventId);
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        public void RemoveListener<T1, T2, T3, T4>(int eventId, RabiEvent<T1, T2, T3, T4> callBack)
        {
            if (!_eventDict.ContainsKey(eventId))
            {
                return;
            }

            var curDelegate = _eventDict[eventId];
            if (curDelegate == null)
            {
                return;
            }

            if (curDelegate.GetType() != callBack.GetType())
            {
                Logger.Error(
                    $"事件参数类型错误 eventId:{eventId} curDelegate:{curDelegate.GetType().Name} callBack:{callBack.GetType().Name}");
                return;
            }

            _eventDict[eventId] = (RabiEvent<T1, T2, T3, T4>) _eventDict[eventId] - callBack;
            if (_eventDict[eventId] == null)
            {
                _eventDict.Remove(eventId);
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        public void RemoveListener(EventId eventId, RabiEvent callBack)
        {
            RemoveListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        public void RemoveListener<T1>(EventId eventId, RabiEvent<T1> callBack)
        {
            RemoveListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public void RemoveListener<T1, T2>(EventId eventId, RabiEvent<T1, T2> callBack)
        {
            RemoveListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        public void RemoveListener<T1, T2, T3>(EventId eventId, RabiEvent<T1, T2, T3> callBack)
        {
            RemoveListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callBack"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        public void RemoveListener<T1, T2, T3, T4>(EventId eventId, RabiEvent<T1, T2, T3, T4> callBack)
        {
            RemoveListener(eventId.GetHashCode(), callBack);
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        public void Dispatch(int eventId)
        {
            if (_eventDict.TryGetValue(eventId, out var curDelegate))
            {
                ((RabiEvent) curDelegate)();
            }
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="param1"></param>
        /// <typeparam name="T1"></typeparam>
        public void Dispatch<T1>(int eventId, T1 param1)
        {
            if (_eventDict.TryGetValue(eventId, out var curDelegate))
            {
                ((RabiEvent<T1>) curDelegate)(param1);
            }
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public void Dispatch<T1, T2>(int eventId, T1 param1, T2 param2)
        {
            if (_eventDict.TryGetValue(eventId, out var curDelegate))
            {
                ((RabiEvent<T1, T2>) curDelegate)(param1, param2);
            }
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        public void Dispatch<T1, T2, T3>(int eventId, T1 param1, T2 param2, T3 param3)
        {
            if (_eventDict.TryGetValue(eventId, out var curDelegate))
            {
                ((RabiEvent<T1, T2, T3>) curDelegate)(param1, param2, param3);
            }
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        public void Dispatch<T1, T2, T3, T4>(int eventId, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (_eventDict.TryGetValue(eventId, out var curDelegate))
            {
                ((RabiEvent<T1, T2, T3, T4>) curDelegate)(param1, param2, param3, param4);
            }
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        public void Dispatch(EventId eventId)
        {
            Dispatch(eventId.GetHashCode());
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="param1"></param>
        /// <typeparam name="T1"></typeparam>
        public void Dispatch<T1>(EventId eventId, T1 param1)
        {
            Dispatch(eventId.GetHashCode(), param1);
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public void Dispatch<T1, T2>(EventId eventId, T1 param1, T2 param2)
        {
            Dispatch(eventId.GetHashCode(), param1, param2);
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        public void Dispatch<T1, T2, T3>(EventId eventId, T1 param1, T2 param2, T3 param3)
        {
            Dispatch(eventId.GetHashCode(), param1, param2, param3);
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        public void Dispatch<T1, T2, T3, T4>(EventId eventId, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            Dispatch(eventId.GetHashCode(), param1, param2, param3, param4);
        }

        /// <summary>
        /// 删除指定事件名
        /// </summary>
        /// <param name="eventId"></param>
        public void RemoveEvent(int eventId)
        {
            if (_eventDict.ContainsKey(eventId))
            {
                _eventDict.Remove(eventId);
            }
        }

        /// <summary>
        /// 删除全部事件名
        /// </summary>
        public void RemoveAllEvents()
        {
            _eventDict.Clear();
        }
    }
}