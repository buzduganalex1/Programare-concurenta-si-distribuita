﻿using FTI.ConversionFunction;

namespace FIT.ConversionFunctionAnnonymous
{
    public class Item
    {
        public Item(string description, Amount price)
        {
            this.Description = description;
            this.Price = price;
        }

        public string Description { get; private set; }

        public Amount Price { get; private set; }
    }
}