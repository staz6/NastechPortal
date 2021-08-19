using System;

namespace ProjectManagement.Entites
{
    public class BaseClass 
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        

    }

}