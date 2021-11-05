using System;
using System.Collections.Generic;

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
        public Room(string name, int size, int? roomNr, int numberOfWallSockets, int numberOfWaterSupplies, string color, DateTime renovatedDate)
        {
            Name = name;
            Size = size;
            RoomNr = roomNr;
            NumberOfWallSockets = numberOfWallSockets;
            NumberOfWaterSupplies = numberOfWaterSupplies;
            Color = color;
            RenovatedDate = renovatedDate;
        }

        public string Name { get; }
        public int Size { get; }
        public int? RoomNr { get; }
        public int NumberOfWallSockets { get; }
        public int NumberOfWaterSupplies { get; }
        public string Color { get; }
        public DateTime RenovatedDate { get; }
        public string Occupant { get; set; }
        public string Comment { get; set; }
        public DateTime? CommentDate { get; set; }
    }

    [Flags]
    public enum RoomUsage
    {
        Bedroom = 1,
        Living = 2,
        Kitchen = 4,
        Dining = 8,
        Office = 16,
        Entrance = 32,
        Bath = 64,
        Other = 128
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
