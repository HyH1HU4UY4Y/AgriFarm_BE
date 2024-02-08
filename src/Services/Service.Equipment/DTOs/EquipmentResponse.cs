﻿namespace Service.Equipment.DTOs
{
    public class EquipmentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Value { get; set; }
        public string Unit { get; set; }
        public string? Notes { get; set; }
    }
}