﻿namespace SurfboardGrp4.Models
{
    public class Board
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double Volume { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string? Equipment { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
