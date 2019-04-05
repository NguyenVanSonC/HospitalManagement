namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPresTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescriptions", "Diagnose", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prescriptions", "Diagnose");
        }
    }
}
