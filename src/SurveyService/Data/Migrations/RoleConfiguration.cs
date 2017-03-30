using System.Data.Entity.Migrations;
using SurveyService.Data;
using SurveyService.Data.Model;
using SurveyService.Features.Users;

namespace SurveyService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(SurveyServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.PRODUCT
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
