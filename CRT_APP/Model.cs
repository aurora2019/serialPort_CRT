using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRT_APP
{
    public class Model
    {
        //标定位
        private String _demarcate;
        //节点号
        private String _node;
        //回路号
        private String _loop;
        //单元号
        private String _unit;
        //单元类型
        private String _unitType;
        //楼栋号
        private String _building;
        //楼层号
        private String _floor;
        //区域号
        private String _area;
        //年
        private String _year;
        //月
        private String _month;
        //日
        private String _day;
        //时
        private String _hour;
        //分
        private String _minute;
        //秒
        private String _second;
        //事件
        private String _events;
        //未用
        private String _unused;
        //结束校验位
        private String _check;
        //时间
        private String _time;

        public string Demarcate { get => _demarcate; set => _demarcate = value; }
        public string Node { get => _node; set => _node = value; }
        public string Loop { get => _loop; set => _loop = value; }
        public string Unit { get => _unit; set => _unit = value; }
        public string UnitType { get => _unitType; set => _unitType = value; }
        public string Building { get => _building; set => _building = value; }
        public string Floor { get => _floor; set => _floor = value; }
        public string Area { get => _area; set => _area = value; }
        public string Year { get => _year; set => _year = value; }
        public string Month { get => _month; set => _month = value; }
        public string Day { get => _day; set => _day = value; }
        public string Hour { get => _hour; set => _hour = value; }
        public string Minute { get => _minute; set => _minute = value; }
        public string Second { get => _second; set => _second = value; }
        public string Events { get => _events; set => _events = value; }
        public string Unused { get => _unused; set => _unused = value; }
        public string Check { get => _check; set => _check = value; }
        public string Time { get => _time; set => _time = value; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
