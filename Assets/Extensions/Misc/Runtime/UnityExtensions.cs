/****************************************************
	文件：UnityExtensions.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：2022/03/16 13:33:11
	功能：Unity相关的扩展
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rabi
{
    public static class UnityExtensions
    {
        public static void StopCoroutineSafe(this MonoBehaviour mono, Coroutine coroutine)
        {
            if (coroutine != null)
            {
                mono.StopCoroutine(coroutine);
            }
        }

        public static void DestroySafe<T>(this MonoBehaviour mono, T t) where T : Component
        {
            if (t != null)
            {
                UnityEngine.Object.Destroy(t);
            }
        }

        /// <summary>
        /// 更加安全的销毁方式
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="go"></param>
        public static void DestroySafe(this MonoBehaviour mono, GameObject go)
        {
            if (go != null)
            {
                UnityEngine.Object.Destroy(go);
            }
        }

        /// <summary>
        /// 从子物体里找到对应名字的对象的Transform组件
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform FindTransInChildrens(this Transform trans, string name)
        {
            foreach (Transform item in trans)
            {
                if (item.name == name)
                {
                    return item;
                }

                return FindTransInChildrens(item, name);
            }

            return null;
        }

        /// <summary>
        /// 从子物体里找到对应Tag的对象的Transform组件
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform FindTransWithTagInChildrens(this Transform trans, string tag)
        {
            foreach (Transform item in trans)
            {
                if (item.tag == tag)
                {
                    return item;
                }

                return FindTransInChildrens(item, tag);
            }

            return null;
        }
    }

    public enum Operation
    {
        None,
        Add,
        Multiply
    }

    public static class BehaviourExtension
    {
        public static T Enable<T>(this T behaviour) where T : Behaviour
        {
            behaviour.enabled = true;
            return behaviour;
        }

        public static T Disable<T>(this T behaviour) where T : Behaviour
        {
            behaviour.enabled = false;
            return behaviour;
        }
    }

    public static class ColorExtension
    {
        public static void Example()
        {
            Color color = "#FFFFFFF".StringToColor();
        }

        public static Color StringToColor(this string str)
        {
            Color tempColor;
            bool succeed = ColorUtility.TryParseHtmlString(str, out tempColor);
            return succeed ? tempColor : Color.black;
        }
    }

    public static class GameObjectExtension
    {
        /// <summary>
        /// 便捷的显示隐藏
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static GameObject Show(this GameObject go)
        {
            go.SetActive(true);
            return go;
        }

        public static GameObject Hide(this GameObject go)
        {
            go.SetActive(false);
            return go;
        }

        /// <summary>
        /// 销毁游戏物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public static void DestroyGameObject<T>(this T component) where T : Component
        {
            if (component && component.gameObject)
            {
                UnityEngine.Object.Destroy(component.gameObject);
            }
        }

        public static T DestroyGameObject<T>(this T component, float delay = 0) where T : Component
        {
            if (component && component.gameObject)
            {
                UnityEngine.Object.Destroy(component.gameObject, delay);
            }

            return component;
        }

        /// <summary>
        /// 设置游戏物体层级
        /// </summary>
        /// <param name="go"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static GameObject Layer(this GameObject go, int layer)
        {
            go.layer = layer;
            return go;
        }

        public static GameObject Layer(this GameObject go, string layer)
        {
            go.layer = LayerMask.NameToLayer(layer);
            return go;
        }

        public static T Layer<T>(this T component, int layer) where T : Component
        {
            component.gameObject.layer = layer;
            return component;
        }

        public static T Layer<T>(this T component, string layer) where T : Component
        {
            component.gameObject.layer = LayerMask.NameToLayer(layer);
            return component;
        }

        public static Component GetOrAddComponent(this GameObject go, Type type)
        {
            Component component = go.GetComponent(type);
            return component ? component : go.AddComponent(type);
        }
    }

    public static class GraphicExtension
    {
        public static T SetAlpha<T>(this T graphic, float alpha) where T : Graphic
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
            return graphic;
        }
    }

    public static class RectTransExtension
    {
        public static RectTransform AnchorPosX(this RectTransform rectTrans, float anchorPosX)
        {
            Vector2 pos = rectTrans.anchoredPosition;
            pos.x = anchorPosX;
            rectTrans.anchoredPosition = pos;
            return rectTrans;
        }

        public static RectTransform AnchorPosY(this RectTransform rectTrans, float anchorPosY)
        {
            Vector2 pos = rectTrans.anchoredPosition;
            pos.y = anchorPosY;
            rectTrans.anchoredPosition = pos;
            return rectTrans;
        }

        public static RectTransform SetSizeWidth(this RectTransform rectTrans, float width)
        {
            Vector2 size = rectTrans.sizeDelta;
            size.x = width;
            rectTrans.sizeDelta = size;
            return rectTrans;
        }

        public static RectTransform SetSizeHeight(this RectTransform rectTrans, float height)
        {
            Vector2 size = rectTrans.sizeDelta;
            size.y = height;
            rectTrans.sizeDelta = size;
            return rectTrans;
        }
    }

    public static class TransformExtension
    {
        /// <summary>
        /// 设置父物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <param name="parentComp"></param>
        /// <returns></returns>
        public static T Parent<T>(this T component, Component parentComp) where T : Component
        {
            component.transform.SetParent(parentComp == null ? null : parentComp.transform, false);
            return component;
        }

        public static T AsRootTrans<T>(this T component) where T : Component
        {
            component.transform.SetParent(null, false);
            return component;
        }

        /// <summary>
        /// 局部属性归一化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static T LocalIdentity<T>(this T component) where T : Component
        {
            component.transform.localPosition = Vector3.zero;
            component.transform.localRotation = Quaternion.identity;
            component.transform.localScale = Vector3.one;
            return component;
        }

        public static T Identity<T>(this T component) where T : Component
        {
            component.transform.position = Vector3.zero;
            component.transform.rotation = Quaternion.identity;
            component.transform.localScale = Vector3.one;
            return component;
        }

        #region Position

        public static Vector3 GetLocalPostion<T>(this T component) where T : Component
        {
            return component.transform.localPosition;
        }

        public static T LocalPosition<T>(this T component, Vector3 localPos) where T : Component
        {
            component.transform.localPosition = localPos;
            return component;
        }

        public static T LocalPosition<T>(this T component, float x, float y, float z) where T : Component
        {
            component.transform.localPosition = new Vector3(x, y, z);
            return component;
        }

        public static T LocalPosition<T>(this T component, float x, float y) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.x = x;
            localPos.y = y;
            component.transform.localPosition = localPos;
            return component;
        }

        public static T LocalPosition<T>(this T component, Func<Vector3, Vector3> Setter) where T : Component
        {
            component.transform.localPosition = Setter(component.transform.localPosition);
            return component;
        }


        public static T LocalPosX<T>(this T component, float x) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.x = x;
            component.transform.localPosition = localPos;
            return component;
        }

        public static T LocalPosX<T>(this T component, Func<float, float> xSetter) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.x = xSetter(localPos.x);
            component.transform.localPosition = localPos;
            return component;
        }


        public static T LocalPosY<T>(this T component, float y) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.y = y;
            component.transform.localPosition = localPos;
            return component;
        }

        public static T LocalPosY<T>(this T component, Func<float, float> ySetter) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.y = ySetter(localPos.y);
            component.transform.localPosition = localPos;
            return component;
        }


        public static T LocalPosZ<T>(this T component, float z) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.z = z;
            component.transform.localPosition = localPos;
            return component;
        }

        public static T LocalPosZ<T>(this T component, Func<float, float> zSetter) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.z = zSetter(localPos.z);
            component.transform.localPosition = localPos;
            return component;
        }


        public static T LocalPosXY<T>(this T component, float x, float y) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.x = x;
            localPos.y = y;
            component.transform.localPosition = localPos;
            return component;
        }

        public static T LocalPosXZ<T>(this T component, float x, float z) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.x = x;
            localPos.z = z;
            component.transform.localPosition = localPos;
            return component;
        }

        public static T LocalPosYZ<T>(this T component, float y, float z) where T : Component
        {
            Vector3 localPos = component.transform.localPosition;
            localPos.y = y;
            localPos.z = z;
            component.transform.localPosition = localPos;
            return component;
        }


        public static T LocalPosIdentity<T>(this T component) where T : Component
        {
            component.transform.localPosition = Vector3.zero;
            return component;
        }


        public static Vector3 GetPostion<T>(this T component) where T : Component
        {
            return component.transform.position;
        }

        public static T SetPosition<T>(this T component, Vector3 pos) where T : Component
        {
            component.transform.position = pos;
            return component;
        }

        public static T SetPosition<T>(this T component, float x, float y, float z) where T : Component
        {
            component.transform.position = new Vector3(x, y, z);
            return component;
        }

        public static T PosX<T>(this T component, float x) where T : Component
        {
            Vector3 pos = component.transform.position;
            pos.x = x;
            component.transform.position = pos;
            return component;
        }

        public static T PosX<T>(this T component, Func<float, float> xSetter) where T : Component
        {
            Vector3 pos = component.transform.position;
            pos.x = xSetter(pos.x);
            component.transform.position = pos;
            return component;
        }

        public static T PosY<T>(this T component, float y) where T : Component
        {
            Vector3 pos = component.transform.position;
            pos.y = y;
            component.transform.position = pos;
            return component;
        }

        public static T PosY<T>(this T component, Func<float, float> ySetter) where T : Component
        {
            Vector3 pos = component.transform.position;
            pos.y = ySetter(pos.y);
            component.transform.position = pos;
            return component;
        }

        public static T PosZ<T>(this T component, float z) where T : Component
        {
            Vector3 pos = component.transform.position;
            pos.z = z;
            component.transform.position = pos;
            return component;
        }

        public static T PosZ<T>(this T component, Func<float, float> zSetter) where T : Component
        {
            Vector3 pos = component.transform.position;
            pos.z = zSetter(pos.z);
            component.transform.position = pos;
            return component;
        }

        public static T PosXY<T>(this T component, float x, float y) where T : Component
        {
            Vector3 pos = component.transform.position;
            pos.x = x;
            pos.y = y;
            component.transform.position = pos;
            return component;
        }

        public static T PosXZ<T>(this T component, float x, float z) where T : Component
        {
            Vector3 pos = component.transform.position;
            pos.x = x;
            pos.z = z;
            component.transform.position = pos;
            return component;
        }

        public static T PosYZ<T>(this T component, float y, float z) where T : Component
        {
            Vector3 pos = component.transform.position;
            pos.y = y;
            pos.z = z;
            component.transform.position = pos;
            return component;
        }

        public static T PosIdentity<T>(this T component) where T : Component
        {
            component.transform.position = Vector3.zero;
            return component;
        }

        #endregion

        #region Rotation

        public static Quaternion GetLocalRotation<T>(this T component) where T : Component
        {
            return component.transform.localRotation;
        }

        public static T LocalRotation<T>(this T component, Quaternion localRotation) where T : Component
        {
            component.transform.localRotation = localRotation;
            return component;
        }

        public static T LocalRotationIdentity<T>(this T component) where T : Component
        {
            component.transform.localRotation = Quaternion.identity;
            return component;
        }

        public static Quaternion GetRotation<T>(this T component) where T : Component
        {
            return component.transform.rotation;
        }

        public static T Rotation<T>(this T component, Quaternion rotation) where T : Component
        {
            component.transform.rotation = rotation;
            return component;
        }

        public static T RotationIdentity<T>(this T component) where T : Component
        {
            component.transform.rotation = Quaternion.identity;
            return component;
        }

        #endregion

        #region Scale

        public static Vector3 GetScale<T>(this T component) where T : Component
        {
            return component.transform.lossyScale;
        }

        public static T Scale<T>(this T component, Vector3 scale, Operation operation = Operation.None)
            where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    component.transform.localScale = scale;
                    break;
                case Operation.Add:
                    tempScale.x += scale.x;
                    tempScale.y += scale.y;
                    tempScale.z += scale.z;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale = component.transform.localScale;
                    tempScale.x *= scale.x;
                    tempScale.y *= scale.y;
                    tempScale.z *= scale.z;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T Scale<T>(this T component, float x, float y, float z, Operation operation = Operation.None)
            where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    component.transform.localScale = new Vector3(x, y, z);
                    break;
                case Operation.Add:
                    tempScale.x += x;
                    tempScale.y += y;
                    tempScale.z += z;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale = component.transform.localScale;
                    tempScale.x *= x;
                    tempScale.y *= y;
                    tempScale.z *= z;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T Scale<T>(this T component, float change, Operation operation = Operation.None)
            where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    component.transform.localScale = Vector3.one * change;
                    break;
                case Operation.Add:
                    tempScale.x += change;
                    tempScale.y += change;
                    tempScale.z += change;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale = component.transform.localScale;
                    tempScale.x *= change;
                    tempScale.y *= change;
                    tempScale.z *= change;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T Scale<T>(this T component, Vector2 scale, Operation operation = Operation.None)
            where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    component.transform.localScale = new Vector3(scale.x, scale.y, component.transform.localScale.z);
                    break;
                case Operation.Add:
                    tempScale.x += scale.x;
                    tempScale.y += scale.y;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale = component.transform.localScale;
                    tempScale.x *= scale.x;
                    tempScale.y *= scale.y;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T Scale<T>(this T component, float x, float y, Operation operation = Operation.None)
            where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    component.transform.localScale = new Vector3(x, y, component.transform.localScale.z);
                    break;
                case Operation.Add:
                    tempScale.x += x;
                    tempScale.y += y;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale = component.transform.localScale;
                    tempScale.x *= x;
                    tempScale.y *= y;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T ScaleX<T>(this T component, float x, Operation operation = Operation.None) where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    tempScale.x = x;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Add:
                    tempScale.x += x;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale.x *= x;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T ScaleY<T>(this T component, float y, Operation operation = Operation.None) where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    tempScale.y = y;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Add:
                    tempScale.y += y;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale.y *= y;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T ScaleZ<T>(this T component, float z, Operation operation = Operation.None) where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    tempScale.z = z;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Add:
                    tempScale.z += z;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale.z *= z;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T ScaleXY<T>(this T component, float x, float y, Operation operation = Operation.None)
            where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    tempScale.x = x;
                    tempScale.y = y;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Add:
                    tempScale.x += x;
                    tempScale.y += y;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale.x *= x;
                    tempScale.y *= y;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T ScaleXZ<T>(this T component, float x, float z, Operation operation = Operation.None)
            where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    tempScale.x = x;
                    tempScale.z = z;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Add:
                    tempScale.x += x;
                    tempScale.z += z;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale.x *= x;
                    tempScale.z *= z;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T ScaleYZ<T>(this T component, float y, float z, Operation operation = Operation.None)
            where T : Component
        {
            Vector3 tempScale = component.transform.localScale;
            switch (operation)
            {
                case Operation.None:
                    tempScale.y = y;
                    tempScale.z = z;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Add:
                    tempScale.y += y;
                    tempScale.z += z;
                    component.transform.localScale = tempScale;
                    break;
                case Operation.Multiply:
                    tempScale.y *= y;
                    tempScale.z *= z;
                    component.transform.localScale = tempScale;
                    break;
                default:
                    break;
            }

            return component;
        }

        public static T Scale<T>(this T component, Func<Vector3, Vector3> Setter) where T : Component
        {
            component.transform.localScale = Setter(component.transform.localScale);
            return component;
        }

        #endregion

        #region DestroyAllChild

        public static T DestroyAllChild<T>(this T component) where T : Component
        {
            foreach (Transform childTrans in component.transform)
            {
                UnityEngine.Object.Destroy(childTrans.gameObject);
            }

            return component;
        }

        public static GameObject DestroyAllChild(this GameObject go)
        {
            foreach (Transform childTrans in go.transform)
            {
                UnityEngine.Object.Destroy(childTrans.gameObject);
            }

            return go;
        }

        #endregion

        #region Sibling Index

        public static T AsFirstSibling<T>(this T component) where T : Component
        {
            component.transform.SetAsFirstSibling();
            return component;
        }

        public static T AsLastSibling<T>(this T component) where T : Component
        {
            component.transform.SetAsLastSibling();
            return component;
        }

        public static T SibilingIndex<T>(this T component, int index) where T : Component
        {
            component.transform.SetSiblingIndex(index);
            return component;
        }

        #endregion

        //这里需要测试一下
        public static Transform SeekTrans(this Transform trans, string name)
        {
            Transform tempTrans = trans.Find(name);
            if (tempTrans != null)
                return tempTrans;
            foreach (Transform transform in trans)
            {
                tempTrans = transform.SeekTrans(name);
                if (tempTrans != null)
                    return tempTrans;
            }

            return null;
        }

        public static T ShowChildByName<T>(this T component, string name) where T : Component
        {
            component.transform.Find(name).gameObject.Show();
            return component;
        }

        public static T HideChildByName<T>(this T component, string name) where T : Component
        {
            component.transform.Find(name).gameObject.Hide();
            return component;
        }
    }

    public static class NoClassify
    {
        //随机数相关
        public static T RandomFromList<T>(this List<T> list, bool remove = false)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            T t = list[index];
            if (remove)
            {
                list.RemoveAt(index);
            }

            return t;
        }

        public static List<T> RandomFromList<T>(this List<T> list, int count, bool remove = false)
        {
            if (count > list.Count || count <= 0)
                return null;
            List<T> result = new List<T>(count);
            List<T> temp;
            if (!remove)
            {
                temp = new List<T>(list.Count);
                foreach (var item in list)
                {
                    temp.Add(item);
                }
            }
            else
            {
                temp = list;
            }

            for (int i = 0; i < count; i++)
            {
                int index = UnityEngine.Random.Range(0, temp.Count);
                result.Add(temp[index]);
                temp.RemoveAt(index);
            }

            return result;
        }

        public static T RandomFromParams<T>(params T[] values)
        {
            return values[UnityEngine.Random.Range(0, values.Length)];
        }
    }
}