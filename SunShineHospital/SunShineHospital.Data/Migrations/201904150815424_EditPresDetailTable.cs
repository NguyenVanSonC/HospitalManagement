namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPresDetailTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrescriptionDetails", "TotalDay", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionDetails", "Calendar", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.PrescriptionDetails", "BeforeMeal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrescriptionDetails", "BeforeMeal");
            DropColumn("dbo.PrescriptionDetails", "Calendar");
            DropColumn("dbo.PrescriptionDetails", "TotalDay");
        }
    }
}
