using System.Data.Entity.Migrations;
using SurveyService.Data;
using SurveyService.Data.Model;

namespace SurveyService.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(SurveyServiceContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default"
            });

            context.SaveChanges();
        }
    }
}
