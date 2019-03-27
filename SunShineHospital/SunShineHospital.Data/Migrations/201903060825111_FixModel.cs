namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "Doctor_ID", "dbo.Doctors");
            DropIndex("dbo.Patients", new[] { "Doctor_ID" });
            DropColumn("dbo.Patients", "Doctor_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Doctor_ID", c => c.Int());
            CreateIndex("dbo.Patients", "Doctor_ID");
            AddForeignKey("dbo.Patients", "Doctor_ID", "dbo.Doctors", "ID");
        }
    }
}
