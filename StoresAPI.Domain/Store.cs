﻿namespace StoresAPI.Domain
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public int CompanyId { get; set; }
    }
}
