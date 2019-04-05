namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNameMedicineTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medicines", "Company", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medicines", "Company", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
