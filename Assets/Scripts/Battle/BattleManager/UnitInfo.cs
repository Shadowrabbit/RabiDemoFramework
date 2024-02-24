// ******************************************************************
//       /\ /|       @file       UnitInfo
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-02-24 20:49
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

namespace Rabi
{
    public class UnitInfo
    {
        public string unitType;
        public string camp;

        public UnitInfo()
        {
        }

        public UnitInfo(string unitType, string camp)
        {
            this.unitType = unitType;
            this.camp = camp;
        }
    }
}