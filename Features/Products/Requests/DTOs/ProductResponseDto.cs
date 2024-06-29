﻿namespace ProductAPI.VSA.Features.Products.Requests.DTOs
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberofProduct { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
