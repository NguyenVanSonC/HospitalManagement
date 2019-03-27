namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMedicineTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Patients", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Doctors", "UserId");
            CreateIndex("dbo.Patients", "UserId");
            AddForeignKey("dbo.Doctors", "UserId", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.Patients", "UserId", "dbo.ApplicationUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Doctors", "UserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Patients", new[] { "UserId" });
            DropIndex("dbo.Doctors", new[] { "UserId" });
            DropColumn("dbo.Patients", "UserId");
            DropColumn("dbo.Doctors", "UserId");
        }
    }
}
