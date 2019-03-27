namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appoinments", "FullName", c => c.String(maxLength: 256));
            AddColumn("dbo.Appoinments", "Address", c => c.String(maxLength: 256));
            AddColumn("dbo.Appoinments", "BirthDay", c => c.DateTime());
            AddColumn("dbo.Appoinments", "Gender", c => c.Boolean());
            AddColumn("dbo.Appoinments", "Healthinsurance", c => c.Boolean(nullable: false));
            DropColumn("dbo.Patients", "Healthinsurance");
            DropColumn("dbo.Patients", "FullName");
            DropColumn("dbo.Patients", "Address");
            DropColumn("dbo.Patients", "BirthDay");
            DropColumn("dbo.Patients", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Gender", c => c.Boolean());
            AddColumn("dbo.Patients", "BirthDay", c => c.DateTime());
            AddColumn("dbo.Patients", "Address", c => c.String(maxLength: 256));
            AddColumn("dbo.Patients", "FullName", c => c.String(maxLength: 256));
            AddColumn("dbo.Patients", "Healthinsurance", c => c.Boolean(nullable: false));
            DropColumn("dbo.Appoinments", "Healthinsurance");
            DropColumn("dbo.Appoinments", "Gender");
            DropColumn("dbo.Appoinments", "BirthDay");
            DropColumn("dbo.Appoinments", "Address");
            DropColumn("dbo.Appoinments", "FullName");
        }
    }
}
