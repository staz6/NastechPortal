using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ProjectManagement.Helper.ProjectPiority;

namespace ProjectManagement.Dto
{
    public class ProjectTaskCreateDto
    {
        [Required]
        public int ProjectPhaseId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Attachement { get; set; }
        public string Legend { get; set; }
        public ProjectTaskPiority Piority { get; set; }
        public List<ProjectTaskAsigneeCreateDtoWithoutId> ProjectAsignee{get;set;}
        [Required]
        public string Reporter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Position { get; set; }
    }
}