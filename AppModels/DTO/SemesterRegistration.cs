using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class SemesterRegistration
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ChildId { get; set; }
        [ForeignKey(nameof(ChildId))]
        public Child Child { get; set; }

        public Guid SemesterId { get; set; }
        [ForeignKey(nameof(SemesterId))]
        public Semester Semester { get; set; }

        public Guid SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        public DateTime DateAdded { get; set; }

        public double ClassScore { get; set; }
        public double ExamScore { get; set; }

        // calculated fields
        private string innerGrade;
        private string innerRemarks;

        public double Total 
        {
            get 
            {
                return ClassScore + ExamScore;
            } 
        }
        public string Grade 
        {
            get 
            {
                if ((100 >= Total) && (Total >= 80))
                    innerGrade = "A";
                else if ((79 >= Total) && (Total >= 60))
                    innerGrade = "B";


                return innerGrade;
            }
        }
        public string Remarks 
        {
            get 
            {
                switch (innerGrade) 
                {
                    case "A":
                        innerRemarks = "Excellent";
                        break;
                    case "B":
                        innerRemarks = "Very Good";
                        break;
                }

                return innerRemarks;
            }
        }
    }
}
