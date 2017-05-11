//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIRSS.Domain.Infrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExamSchedule
    {
        public int Id { get; set; }
        public short Semister { get; set; }
        public System.DateTime Year { get; set; }
        public System.TimeSpan ExamHour { get; set; }
        public string SectionName { get; set; }
        public int GradeLevelId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int ClassRoomId { get; set; }
    
        public virtual ClassRoom ClassRoom { get; set; }
        public virtual GradeLevel GradeLevel { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
