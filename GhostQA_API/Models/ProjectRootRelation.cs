﻿using GhostQA_API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GhostQA_API.Models
{
    public class ProjectRootRelation : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}