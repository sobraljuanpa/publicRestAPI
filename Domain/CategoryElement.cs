using System;

namespace Domain
{
    public abstract class CategoryElement
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        public Category Category { get; set; }
    }
}
