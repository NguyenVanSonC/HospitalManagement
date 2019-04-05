namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAppointmentFinal : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Appoinments");
            AddColumn("dbo.Appoinments", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Appoinments", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Appoinments");
            DropColumn("dbo.Appoinments", "ID");
            AddPrimaryKey("dbo.Appoinments", new[] { "DoctorID", "PatientID" });
        }
    }
}
