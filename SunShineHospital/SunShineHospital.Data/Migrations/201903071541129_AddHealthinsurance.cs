namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHealthinsurance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "Gender", c => c.Boolean());
            AddColumn("dbo.Patients", "Healthinsurance", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "Healthinsurance");
            DropColumn("dbo.ApplicationUsers", "Gender");
        }
    }
}
