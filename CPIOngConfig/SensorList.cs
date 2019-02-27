﻿using System.Collections.Generic;
using System.Linq;

namespace R_Flow_Hardware
{
    public class Sensor
    {
        public const string
            Undefined = "Undefined",
            ANALOG = "ANALOG",
            DIGITAL = "DIGITAL",
            COUNT = "COUNT",
            TYPE = "TYPE";

        public byte ModulId;
        public int CanIdentifier;
        public int CanIndex;
        public string sensorName;
        public uint workPlace;
        public uint workLine;
        public string workPosition;

        public Sensor(byte ModulId, int CanIdentifier, int CanIndex, string sensorName, uint workPlace, uint workLine, string workPosition)
        {
            this.ModulId = ModulId;
            this.CanIdentifier = CanIdentifier;
            this.CanIndex = CanIndex;
            this.sensorName = sensorName;
            this.workPlace = workPlace;
            this.workLine = workLine;
            this.workPosition = workPosition;
        }
    }

    public class SensorList
    {
        private List<Sensor> sensors;

        public SensorList(List<string> selSensorList, uint workPlace1, uint workPlace2=0)
        {
            sensors = new List<Sensor>();
            sensors = InitAllSensors(selSensorList, workPlace1, workPlace2);
        }

        public List<Sensor> GetAllSensors()
        {
            return sensors.FindAll(x => x.sensorName != "");
        }

        public List<Sensor> GetCanIdentifiers(int CanIdentifier)
        {
            return sensors.FindAll(x => x.CanIdentifier == CanIdentifier).OrderBy(x => x.CanIndex).ToList();
        }

        public string GetSensorType(string sensorName)
        {
            var result = Sensor.Undefined;
            if (sensorName == "Cnt")
                result = Sensor.COUNT;
            else if (sensorName == "Pa"
                || sensorName == "Pd"
                || sensorName == "Pdi"
                || sensorName == "Hu"
                || sensorName == "T")
                result = Sensor.ANALOG;
            else if (sensorName == "Ps")
                result = Sensor.DIGITAL;
            else if (sensorName == "Typ")
                result = Sensor.TYPE;
            return result;
        }

        private List<Sensor> InitAllSensors(List<string> selSensorList, uint workPlace1, uint workPlace2)
        {
            var allSensors = new List<Sensor>
            {
				
				// byte ModulId, int CanIdentifier, int CanIndex, string sensorName, uint workPlace, uint workLine, string workPosition
            new Sensor(11,93,0,"Cnt",0,1,"")
            , new Sensor(11,93,0,"Cnt",0,1,"1")
            , new Sensor(11,94,0,"Cnt",0,1,"2")
            , new Sensor(11,95,0,"Cnt",0,1,"3")
            , new Sensor(12,101,0,"Cnt",0,1,"4")
            , new Sensor(12,102,0,"Cnt",0,1,"5")
            , new Sensor(12,103,0,"Cnt",0,1,"6")
            , new Sensor(13,109,0,"Cnt",0,1,"7")
            , new Sensor(13,110,0,"Cnt",0,1,"8")
            , new Sensor(13,111,0,"Cnt",0,1,"9")
            , new Sensor(14,117,0,"Cnt",0,2,"1")
            , new Sensor(14,118,0,"Cnt",0,2,"2")
            , new Sensor(14,119,0,"Cnt",0,2,"3")
            , new Sensor(15,125,0,"Cnt",0,2,"4")
            , new Sensor(15,126,0,"Cnt",0,2,"5")
            , new Sensor(15,127,0,"Cnt",0,2,"6")
            , new Sensor(16,133,0,"Cnt",0,2,"7")
            , new Sensor(16,134,0,"Cnt",0,2,"8")
            , new Sensor(16,135,0,"Cnt",0,2,"9")
            , new Sensor(17,141,0,"Cnt",0,3,"1")
            , new Sensor(17,142,0,"Cnt",0,3,"2")
            , new Sensor(17,143,0,"Cnt",0,3,"3")
            , new Sensor(18,149,0,"Cnt",0,3,"4")
            , new Sensor(18,150,0,"Cnt",0,3,"5")
            , new Sensor(18,151,0,"Cnt",0,3,"6")
            , new Sensor(19,157,0,"Cnt",0,3,"7")
            , new Sensor(19,158,0,"Cnt",0,3,"8")
            , new Sensor(19,159,0,"Cnt",0,3,"9")
            , new Sensor(20,165,0,"Cnt",0,4,"1")
            , new Sensor(20,166,0,"Cnt",0,4,"2")
            , new Sensor(20,167,0,"Cnt",0,4,"3")
            , new Sensor(21,173,0,"Cnt",0,4,"4")
            , new Sensor(21,174,0,"Cnt",0,4,"5")
            , new Sensor(21,175,0,"Cnt",0,4,"6")
            , new Sensor(22,181,0,"Cnt",0,4,"7")
            , new Sensor(22,182,0,"Cnt",0,4,"8")
            , new Sensor(22,183,0,"Cnt",0,4,"9")
            , new Sensor(8,392,0,"CSl",0,1,"")
            , new Sensor(40,680,3,"Hu",0,0,"1")
            , new Sensor(40,680,2,"Pa",0,0,"1")
            , new Sensor(40,680,1,"Pd",0,0,"1")
            , new Sensor(39,679,0,"Pd",0,0,"10")
            , new Sensor(40,936,0,"Pd",0,0,"10")
            , new Sensor(39,679,1,"Pd",0,0,"11")
            , new Sensor(40,936,1,"Pd",0,0,"11")
            , new Sensor(39,679,2,"Pd",0,0,"12")
            , new Sensor(40,936,2,"Pd",0,0,"12")
            , new Sensor(39,679,3,"Pd",0,0,"13")
            , new Sensor(39,935,0,"Pd",0,0,"14")
            , new Sensor(39,935,1,"Pd",0,0,"15")
            , new Sensor(39,935,2,"Pd",0,0,"16")
            , new Sensor(39,935,3,"Pd",0,0,"17")
            , new Sensor(39,1191,0,"Pd",0,0,"18")
            , new Sensor(39,1191,1,"Pd",0,0,"19")
            , new Sensor(40,936,1,"Pd",0,0,"2")
            , new Sensor(39,1191,2,"Pd",0,0,"20")
            , new Sensor(39,1191,3,"Pd",0,0,"21")
            , new Sensor(40,936,3,"Pd",0,0,"3")
            , new Sensor(40,1092,1,"Pd",0,0,"4")
            , new Sensor(40,936,0,"Pd",0,1,"")
            , new Sensor(41,681,2,"Pd",0,1,"1")
            , new Sensor(42,682,2,"Pd",0,2,"1")
            , new Sensor(43,683,2,"Pd",0,3,"1")
            , new Sensor(44,684,2,"Pd",0,4,"1")
            , new Sensor(40,680,2,"Pd",0,0,"Q2")
            , new Sensor(40,936,2,"Pd",0,0,"Q3")
            , new Sensor(41,681,3,"Pdi",0,1,"1")
            , new Sensor(41,937,0,"Pdi",0,1,"2")
            , new Sensor(41,937,1,"Pdi",0,1,"3")
            , new Sensor(41,937,2,"Pdi",0,1,"4")
            , new Sensor(41,937,3,"Pdi",0,1,"5")
            , new Sensor(41,1193,0,"Pdi",0,1,"6")
            , new Sensor(41,1193,1,"Pdi",0,1,"7")
            , new Sensor(41,1193,2,"Pdi",0,1,"8")
            , new Sensor(41,1193,3,"Pdi",0,1,"9")
            , new Sensor(42,682,3,"Pdi",0,2,"1")
            , new Sensor(41,938,0,"Pdi",0,2,"2")
            , new Sensor(41,938,1,"Pdi",0,2,"3")
            , new Sensor(42,938,2,"Pdi",0,2,"4")
            , new Sensor(42,938,3,"Pdi",0,2,"5")
            , new Sensor(42,1194,0,"Pdi",0,2,"6")
            , new Sensor(42,1194,1,"Pdi",0,2,"7")
            , new Sensor(42,1194,2,"Pdi",0,2,"8")
            , new Sensor(42,1194,3,"Pdi",0,2,"9")
            , new Sensor(43,683,3,"Pdi",0,3,"1")
            , new Sensor(42,939,0,"Pdi",0,3,"2")
            , new Sensor(42,939,1,"Pdi",0,3,"3")
            , new Sensor(43,939,2,"Pdi",0,3,"4")
            , new Sensor(43,939,3,"Pdi",0,3,"5")
            , new Sensor(43,1195,0,"Pdi",0,3,"6")
            , new Sensor(43,1195,1,"Pdi",0,3,"7")
            , new Sensor(43,1195,2,"Pdi",0,3,"8")
            , new Sensor(43,1195,3,"Pdi",0,3,"9")
            , new Sensor(44,684,3,"Pdi",0,4,"1")
            , new Sensor(43,940,0,"Pdi",0,4,"2")
            , new Sensor(43,940,1,"Pdi",0,4,"3")
            , new Sensor(44,940,2,"Pdi",0,4,"4")
            , new Sensor(44,940,3,"Pdi",0,4,"5")
            , new Sensor(44,1196,0,"Pdi",0,4,"6")
            , new Sensor(44,1196,1,"Pdi",0,4,"7")
            , new Sensor(44,1196,2,"Pdi",0,4,"8")
            , new Sensor(44,1196,3,"Pdi",0,4,"9")
            , new Sensor(99,0,0,"Ps",0,0,"1")
            , new Sensor(40,424,0,"Ps",0,0,"1-3")
            , new Sensor(99,0,0,"Ps",0,0,"2")
            , new Sensor(99,0,0,"Ps",0,0,"3")
            , new Sensor(40,424,1,"Ps",0,0,"9-12")
            , new Sensor(99,0,0,"Ps",0,1,"1")
            , new Sensor(11,92,1,"Ps",0,1,"1-3")
            , new Sensor(11,92,0,"Ps",0,1,"1-3G")
            , new Sensor(99,0,0,"Ps",0,1,"2")
            , new Sensor(99,0,0,"Ps",0,1,"3")
            , new Sensor(99,0,0,"Ps",0,1,"4")
            , new Sensor(12,100,1,"Ps",0,1,"4-6")
            , new Sensor(12,100,0,"Ps",0,1,"4-6G")
            , new Sensor(99,0,0,"Ps",0,1,"5")
            , new Sensor(99,0,0,"Ps",0,1,"6")
            , new Sensor(13,108,1,"Ps",0,1,"7-9")
            , new Sensor(13,108,0,"Ps",0,1,"7-9G")
            , new Sensor(11,92,0,"Ps",0,0,"1-8")
            , new Sensor(14,116,1,"Ps",0,2,"1-3")
            , new Sensor(14,116,0,"Ps",0,2,"1-3G")
            , new Sensor(15,124,1,"Ps",0,2,"4-6")
            , new Sensor(15,124,0,"Ps",0,2,"4-6G")
            , new Sensor(16,132,1,"Ps",0,2,"7-9")
            , new Sensor(16,132,0,"Ps",0,2,"7-9G")
            , new Sensor(17,140,1,"Ps",0,3,"1-3")
            , new Sensor(17,140,0,"Ps",0,3,"1-3G")
            , new Sensor(18,148,1,"Ps",0,3,"4-6")
            , new Sensor(18,148,0,"Ps",0,3,"4-6G")
            , new Sensor(19,156,1,"Ps",0,3,"7-9")
            , new Sensor(19,156,0,"Ps",0,3,"7-9G")
            , new Sensor(20,164,1,"Ps",0,4,"1-3")
            , new Sensor(20,164,0,"Ps",0,4,"1-3G")
            , new Sensor(21,172,1,"Ps",0,4,"4-6")
            , new Sensor(21,172,0,"Ps",0,4,"4-6G")
            , new Sensor(22,180,1,"Ps",0,4,"7-9")
            , new Sensor(22,180,0,"Ps",0,4,"7-9G")
            , new Sensor(11,92,1,"Ps",0,0,"9-16")
            , new Sensor(40,680,0,"T",0,0,"1")
            , new Sensor(40,936,0,"T",0,0,"2")
            , new Sensor(40,936,2,"T",0,0,"3")
            , new Sensor(40,1092,0,"T",0,0,"4")
            , new Sensor(40,680,0,"T",0,1,"")
            , new Sensor(41,681,0,"T",0,1,"1")
            , new Sensor(41,681,1,"T",0,1,"2")
            , new Sensor(40,680,2,"T",0,2,"")
            , new Sensor(42,682,0,"T",0,2,"1")
            , new Sensor(42,682,1,"T",0,2,"2")
            , new Sensor(43,683,1,"T",0,3,"1")
            , new Sensor(43,683,0,"T",0,3,"2")
            , new Sensor(44,684,0,"T",0,4,"1")
            , new Sensor(44,684,1,"T",0,4,"2")
            , new Sensor(41,681,3,"T",0,5,"1")
            , new Sensor(11,0,0,"Typ",0,1,"1")
            , new Sensor(11,1957,1,"Typ",0,1,"11")
            , new Sensor(11,1957,1,"Typ",0,1,"11P")
            , new Sensor(12,1958,1,"Typ",0,1,"12")
            , new Sensor(13,1959,1,"Typ",0,1,"13")
            , new Sensor(14,1960,1,"Typ",0,2,"14")
            , new Sensor(15,1961,1,"Typ",0,2,"15")
            , new Sensor(16,1962,1,"Typ",0,2,"16")
            , new Sensor(17,1963,1,"Typ",0,3,"17")
            , new Sensor(18,1964,1,"Typ",0,3,"18")
            , new Sensor(19,1965,1,"Typ",0,3,"19")
            , new Sensor(20,1966,1,"Typ",0,4,"20")
            , new Sensor(21,1967,1,"Typ",0,4,"21")
            , new Sensor(22,1968,1,"Typ",0,4,"22")
            , new Sensor(30,1438,0,"Typ",0,0,"30")
            , new Sensor(30,1438,0,"Typ",0,0,"30P")
            , new Sensor(31,1439,0,"Typ",0,0,"31")
            , new Sensor(32,1440,0,"Typ",0,0,"32")
            , new Sensor(34,1442,0,"Typ",0,0,"34")
            , new Sensor(39,1447,0,"Typ",0,0,"39")
            , new Sensor(40,1448,0,"Typ",0,0,"40")
            , new Sensor(40,1448,0,"Typ",0,0,"40P")
            , new Sensor(41,1449,0,"Typ",0,1,"41")
            , new Sensor(42,1450,0,"Typ",0,2,"42")
            , new Sensor(43,1451,0,"Typ",0,3,"43")
            , new Sensor(44,1452,0,"Typ",0,4,"44")
            , new Sensor(11,94,0,"Cnt",1,1,"")
            , new Sensor(79,637,0,"Cnt",1,1,"1")
            , new Sensor(79,638,0,"Cnt",1,1,"2")
            , new Sensor(79,639,0,"Cnt",1,1,"3")
            , new Sensor(80,645,0,"Cnt",1,1,"4")
            , new Sensor(80,646,0,"Cnt",1,1,"5")
            , new Sensor(80,647,0,"Cnt",1,1,"6")
            , new Sensor(81,653,0,"Cnt",1,1,"7")
            , new Sensor(81,654,0,"Cnt",1,1,"8")
            , new Sensor(81,655,0,"Cnt",1,1,"9")
            , new Sensor(82,661,0,"Cnt",1,2,"1")
            , new Sensor(82,662,0,"Cnt",1,2,"2")
            , new Sensor(82,663,0,"Cnt",1,2,"3")
            , new Sensor(83,669,0,"Cnt",1,2,"4")
            , new Sensor(83,670,0,"Cnt",1,2,"5")
            , new Sensor(83,671,0,"Cnt",1,2,"6")
            , new Sensor(84,677,0,"Cnt",1,2,"7")
            , new Sensor(84,678,0,"Cnt",1,2,"8")
            , new Sensor(84,679,0,"Cnt",1,2,"9")
            , new Sensor(85,685,0,"Cnt",1,3,"1")
            , new Sensor(85,686,0,"Cnt",1,3,"2")
            , new Sensor(85,687,0,"Cnt",1,3,"3")
            , new Sensor(86,693,0,"Cnt",1,3,"4")
            , new Sensor(86,694,0,"Cnt",1,3,"5")
            , new Sensor(86,695,0,"Cnt",1,3,"6")
            , new Sensor(87,701,0,"Cnt",1,3,"7")
            , new Sensor(87,702,0,"Cnt",1,3,"8")
            , new Sensor(87,703,0,"Cnt",1,3,"9")
            , new Sensor(88,709,0,"Cnt",1,4,"1")
            , new Sensor(88,710,0,"Cnt",1,4,"2")
            , new Sensor(88,711,0,"Cnt",1,4,"3")
            , new Sensor(89,717,0,"Cnt",1,4,"4")
            , new Sensor(89,718,0,"Cnt",1,4,"5")
            , new Sensor(89,719,0,"Cnt",1,4,"6")
            , new Sensor(90,725,0,"Cnt",1,4,"7")
            , new Sensor(90,726,0,"Cnt",1,4,"8")
            , new Sensor(90,727,0,"Cnt",1,4,"9")
            , new Sensor(52,692,3,"Hu",1,0,"1")
            , new Sensor(40,680,2,"Pa",1,0,"1")
            , new Sensor(52,692,1,"Pd",1,0,"1")
            , new Sensor(51,691,0,"Pd",1,0,"10")
            , new Sensor(52,948,0,"Pd",1,0,"10")
            , new Sensor(51,691,1,"Pd",1,0,"11")
            , new Sensor(52,948,1,"Pd",1,0,"11")
            , new Sensor(51,691,2,"Pd",1,0,"12")
            , new Sensor(52,948,2,"Pd",1,0,"12")
            , new Sensor(51,691,3,"Pd",1,0,"13")
            , new Sensor(51,947,0,"Pd",1,0,"14")
            , new Sensor(51,947,1,"Pd",1,0,"15")
            , new Sensor(51,947,2,"Pd",1,0,"16")
            , new Sensor(51,947,3,"Pd",1,0,"17")
            , new Sensor(51,1203,0,"Pd",1,0,"18")
            , new Sensor(51,1203,1,"Pd",1,0,"19")
            , new Sensor(52,948,1,"Pd",1,0,"2")
            , new Sensor(51,1203,2,"Pd",1,0,"20")
            , new Sensor(51,1203,3,"Pd",1,0,"21")
            , new Sensor(52,948,3,"Pd",1,0,"3")
            , new Sensor(52,1204,1,"Pd",1,0,"4")
            , new Sensor(40,936,1,"Pd",1,1,"")
            , new Sensor(53,693,2,"Pd",1,1,"1")
            , new Sensor(54,694,2,"Pd",1,2,"1")
            , new Sensor(55,695,2,"Pd",1,3,"1")
            , new Sensor(56,696,2,"Pd",1,4,"1")
            , new Sensor(40,680,3,"Pd",1,0,"Q2")
            , new Sensor(40,936,3,"Pd",1,0,"Q3")
            , new Sensor(53,693,3,"Pdi",1,1,"1")
            , new Sensor(53,949,0,"Pdi",1,1,"2")
            , new Sensor(53,949,1,"Pdi",1,1,"3")
            , new Sensor(53,949,2,"Pdi",1,1,"4")
            , new Sensor(53,949,3,"Pdi",1,1,"5")
            , new Sensor(53,1205,0,"Pdi",1,1,"6")
            , new Sensor(53,1205,1,"Pdi",1,1,"7")
            , new Sensor(53,1205,2,"Pdi",1,1,"8")
            , new Sensor(53,1205,3,"Pdi",1,1,"9")
            , new Sensor(54,694,3,"Pdi",1,2,"1")
            , new Sensor(54,950,0,"Pdi",1,2,"2")
            , new Sensor(54,950,1,"Pdi",1,2,"3")
            , new Sensor(54,950,2,"Pdi",1,2,"4")
            , new Sensor(54,950,3,"Pdi",1,2,"5")
            , new Sensor(54,1206,0,"Pdi",1,2,"6")
            , new Sensor(54,1206,1,"Pdi",1,2,"7")
            , new Sensor(54,1206,2,"Pdi",1,2,"8")
            , new Sensor(54,1206,3,"Pdi",1,2,"9")
            , new Sensor(55,695,3,"Pdi",1,3,"1")
            , new Sensor(55,951,0,"Pdi",1,3,"2")
            , new Sensor(55,951,1,"Pdi",1,3,"3")
            , new Sensor(55,951,2,"Pdi",1,3,"4")
            , new Sensor(55,951,3,"Pdi",1,3,"5")
            , new Sensor(55,1207,0,"Pdi",1,3,"6")
            , new Sensor(55,1207,1,"Pdi",1,3,"7")
            , new Sensor(55,1207,2,"Pdi",1,3,"8")
            , new Sensor(55,1207,3,"Pdi",1,3,"9")
            , new Sensor(56,696,3,"Pdi",1,4,"1")
            , new Sensor(56,952,0,"Pdi",1,4,"2")
            , new Sensor(56,952,1,"Pdi",1,4,"3")
            , new Sensor(56,952,2,"Pdi",1,4,"4")
            , new Sensor(56,952,3,"Pdi",1,4,"5")
            , new Sensor(56,1208,0,"Pdi",1,4,"6")
            , new Sensor(56,1208,1,"Pdi",1,4,"7")
            , new Sensor(56,1208,2,"Pdi",1,4,"8")
            , new Sensor(56,1208,3,"Pdi",1,4,"9")
            , new Sensor(52,436,0,"Ps",1,0,"1-3")
            , new Sensor(52,436,1,"Ps",1,0,"9-12")
            , new Sensor(79,636,1,"Ps",1,1,"1-3")
            , new Sensor(79,636,0,"Ps",1,1,"1-3G")
            , new Sensor(80,644,1,"Ps",1,1,"4-6")
            , new Sensor(80,644,0,"Ps",1,1,"4-6G")
            , new Sensor(81,652,1,"Ps",1,1,"7-9")
            , new Sensor(81,652,0,"Ps",1,1,"7-9G")
            , new Sensor(11,92,0,"Ps",1,0,"1-8")
            , new Sensor(82,660,1,"Ps",1,2,"1-3")
            , new Sensor(82,660,0,"Ps",1,2,"1-3G")
            , new Sensor(83,668,1,"Ps",1,2,"4-6")
            , new Sensor(83,668,0,"Ps",1,2,"4-6G")
            , new Sensor(84,676,1,"Ps",1,2,"7-9")
            , new Sensor(84,676,0,"Ps",1,2,"7-9G")
            , new Sensor(85,684,1,"Ps",1,3,"1-3")
            , new Sensor(85,684,0,"Ps",1,3,"1-3G")
            , new Sensor(86,692,1,"Ps",1,3,"4-6")
            , new Sensor(86,692,0,"Ps",1,3,"4-6G")
            , new Sensor(87,700,1,"Ps",1,3,"7-9")
            , new Sensor(87,700,0,"Ps",1,3,"7-9G")
            , new Sensor(88,708,1,"Ps",1,4,"1-3")
            , new Sensor(88,708,0,"Ps",1,4,"1-3G")
            , new Sensor(89,716,1,"Ps",1,4,"4-6")
            , new Sensor(89,716,0,"Ps",1,4,"4-6G")
            , new Sensor(90,724,1,"Ps",1,4,"7-9")
            , new Sensor(90,724,0,"Ps",1,4,"7-9G")
            , new Sensor(11,92,1,"Ps",1,0,"9-16")
            , new Sensor(52,692,0,"T",1,0,"1")
            , new Sensor(52,948,0,"T",1,0,"2")
            , new Sensor(52,948,2,"T",1,0,"3")
            , new Sensor(52,1204,0,"T",1,0,"4")
            , new Sensor(40,680,1,"T",1,1,"")
            , new Sensor(53,693,0,"T",1,1,"1")
            , new Sensor(53,693,1,"T",1,1,"2")
            , new Sensor(40,680,3,"T",1,2,"")
            , new Sensor(54,694,0,"T",1,2,"1")
            , new Sensor(54,694,1,"T",1,2,"2")
            , new Sensor(55,695,1,"T",1,3,"1")
            , new Sensor(55,695,0,"T",1,3,"2")
            , new Sensor(56,696,0,"T",1,4,"1")
            , new Sensor(56,696,1,"T",1,4,"2")
            , new Sensor(53,693,3,"T",1,5,"1")
            , new Sensor(79,2025,1,"Typ",1,1,"11")
            , new Sensor(11,1957,1,"Typ",1,1,"11P")
            , new Sensor(80,2026,1,"Typ",1,1,"12")
            , new Sensor(81,2027,1,"Typ",1,1,"13")
            , new Sensor(82,2028,1,"Typ",1,2,"14")
            , new Sensor(83,2029,1,"Typ",1,2,"15")
            , new Sensor(84,2030,1,"Typ",1,2,"16")
            , new Sensor(85,2031,1,"Typ",1,3,"17")
            , new Sensor(86,2032,1,"Typ",1,3,"18")
            , new Sensor(87,2033,1,"Typ",1,3,"19")
            , new Sensor(88,2034,1,"Typ",1,4,"20")
            , new Sensor(89,2035,1,"Typ",1,4,"21")
            , new Sensor(90,2036,1,"Typ",1,4,"22")
            , new Sensor(94,1502,0,"Typ",1,0,"30")
            , new Sensor(30,1438,0,"Typ",1,0,"30P")
            , new Sensor(95,1503,0,"Typ",1,0,"31")
            , new Sensor(96,1504,0,"Typ",1,0,"32")
            , new Sensor(98,1506,0,"Typ",1,0,"34")
            , new Sensor(51,1459,0,"Typ",1,0,"39")
            , new Sensor(52,1460,0,"Typ",1,0,"40")
            , new Sensor(40,1448,0,"Typ",1,0,"40P")
            , new Sensor(40,1448,0,"Typ",1,0,"40R")
            , new Sensor(53,1461,0,"Typ",1,1,"41")
            , new Sensor(54,1462,0,"Typ",1,2,"42")
            , new Sensor(55,1463,0,"Typ",1,3,"43")
            , new Sensor(56,1464,0,"Typ",1,4,"44")
            };

            sensors.Clear();
            foreach(var item in selSensorList)
            {
                var foundSensor = allSensors.FirstOrDefault(x => x.sensorName + "." + x.workPlace + "." + x.workLine + "." + x.workPosition == item);
                if (foundSensor.workPlace == 0 && workPlace1 > 0)
                {
                    foundSensor.workPlace = workPlace1;
                    sensors.Add(foundSensor);
                }
                if (foundSensor.workPlace == 1 && workPlace2 > 0)
                {
                    foundSensor.workPlace = workPlace2;
                    sensors.Add(foundSensor);
                }
            }
            return sensors;
        }
    }
}