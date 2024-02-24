// ******************************************************************
//       /\ /|       @file       CombinationStateNode.cs
//       \ V/        @brief      组合状态节点
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2022-05-16 11:38:15
//    *(__\_\        @Copyright  Copyright (c) 2022, Shadowrabbit
// ******************************************************************

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rabi
{
    [Serializable]
    public struct CombinationStateNode
    {
        [SerializeField] [LabelText("控制节点类型")] private StateNodeType stateNodeType; //控制类型
        public ActiveStateNode activeStateNode; //激活控制节点
        public ColorStateNode colorStateNode; //颜色控制节点
        public MeshProColorStateNode meshProColorStateNode; //文本颜色控制节点
        public GrayStateNode grayStateNode; //灰度控制节点
        public SpriteStateNode spriteStateNode; //sp图片控制节点
        public TextureStateNode textureStateNode; //tex图片控制节点
        public TextStateNode textStateNode; //文本内容控制节点

        public CombinationStateNode(StateNodeType stateNodeType)
        {
            this.stateNodeType = stateNodeType;
            activeStateNode = new ActiveStateNode();
            colorStateNode = new ColorStateNode();
            grayStateNode = new GrayStateNode();
            meshProColorStateNode = new MeshProColorStateNode();
            spriteStateNode = new SpriteStateNode();
            textureStateNode = new TextureStateNode();
            textStateNode = new TextStateNode();
        }

        public void OnInit(GameObject obj)
        {
            switch (stateNodeType)
            {
                case StateNodeType.WaitSelect:
                    break;
                case StateNodeType.Active:
                    activeStateNode?.OnInit(obj);
                    break;
                case StateNodeType.Color:
                    colorStateNode?.OnInit(obj);
                    break;
                case StateNodeType.Gray:
                    grayStateNode?.OnInit(obj);
                    break;
                case StateNodeType.MeshProColor:
                    meshProColorStateNode?.OnInit(obj);
                    break;
                case StateNodeType.Sprite:
                    spriteStateNode?.OnInit(obj);
                    break;
                case StateNodeType.Texture:
                    textureStateNode?.OnInit(obj);
                    break;
                case StateNodeType.Text:
                    textStateNode?.OnInit(obj);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnStateChanged(int state)
        {
            switch (stateNodeType)
            {
                case StateNodeType.WaitSelect:
                    break;
                case StateNodeType.Active:
                    activeStateNode?.OnStateChanged(state);
                    break;
                case StateNodeType.Color:
                    colorStateNode?.OnStateChanged(state);
                    break;
                case StateNodeType.Gray:
                    grayStateNode?.OnStateChanged(state);
                    break;
                case StateNodeType.MeshProColor:
                    meshProColorStateNode?.OnStateChanged(state);
                    break;
                case StateNodeType.Sprite:
                    spriteStateNode?.OnStateChanged(state);
                    break;
                case StateNodeType.Texture:
                    textureStateNode?.OnStateChanged(state);
                    break;
                case StateNodeType.Text:
                    textStateNode?.OnStateChanged(state);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}