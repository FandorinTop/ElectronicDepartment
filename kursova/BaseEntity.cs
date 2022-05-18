﻿using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.DomainEntities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime DeletedAt { get; set; }
    }
}