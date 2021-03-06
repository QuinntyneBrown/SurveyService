using System;
using System.Collections.Generic;
using SurveyService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class SurveyResult: ILoggable
    {
        public int Id { get; set; }
        
        [ForeignKey("Survey")]
        public int? SurveyId { get; set; }

		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }
        
        public ICollection<Response> Responses { get; set; } = new HashSet<Response>();
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Survey Survey { get; set; }
    }
}
