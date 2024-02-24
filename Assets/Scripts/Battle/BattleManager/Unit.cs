// ******************************************************************
//       /\ /|       @file       Unit
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-24 20:49
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public class Unit : ThingWithComps
    {
        private int _eid;
        public UnitInfo unitInfo;
        public string UnitType => unitInfo.unitType;
        public string Camp => unitInfo.camp; //阵营

        public override void OnSpawn(params object[] args)
        {
            base.OnSpawn(args);
            if (args.Length < 3)
            {
                Logger.Error($"参数数量缺失");
                return;
            }

            if (args[0] is not int eid)
            {
                Logger.Error($"eid参数错误 期望:string 实际:{args[0].GetType()}");
                return;
            }

            if (args[1] is not string unitType)
            {
                Logger.Error($"unitType参数错误 期望:string 实际:{args[1].GetType()}");
                return;
            }

            if (args[2] is not string camp)
            {
                Logger.Error($"camp参数错误 期望:string 实际:{args[2].GetType()}");
                return;
            }

            _eid = eid;
            unitInfo = new UnitInfo(unitType, camp);
        }
    }
}