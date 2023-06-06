﻿namespace project_backend.Models
{
    public class Establishment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Ruc { get; set; }

        public List<Cash> Cashs { get; } = new List<Cash>();
    }
}
