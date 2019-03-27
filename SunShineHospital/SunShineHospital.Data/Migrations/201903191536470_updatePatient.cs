namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "FullName", c => c.String(maxLength: 256));
            AddColumn("dbo.Patients", "Address", c => c.String(maxLength: 256));
            AddColumn("dbo.Patients", "BirthDay", c => c.DateTime());
            AddColumn("dbo.Patients", "Gender", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "Gender");
            DropColumn("dbo.Patients", "BirthDay");
            DropColumn("dbo.Patients", "Address");
            DropColumn("dbo.Patients", "FullName");
        }
    }
}
