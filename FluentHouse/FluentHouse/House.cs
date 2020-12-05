using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHouse
{
    public class House
    {
        public List<Floor> Floors { get; set; }
        public Garage Garage { get; set; }
        public Garden Garden { get; set; }
        public Pool Pool { get; set; }
    }

    public class Floor
    {
        public int Level { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();
    }

    public class Room
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }

    public class Garage
    {
    }

    public class Garden
    {
    }

    public class Pool
    {
    }
}
