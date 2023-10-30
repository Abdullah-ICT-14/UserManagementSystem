namespace UserManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserManagementDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Salt = c.String(),
                        HashedPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.String(),
                        Name = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserId, unique: true);
            
            CreateTable(
                "dbo.SpecialCodes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Code = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passwords", "UserId", "dbo.Users");
            DropForeignKey("dbo.SpecialCodes", "UserId", "dbo.Users");
            DropIndex("dbo.SpecialCodes", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "UserId" });
            DropIndex("dbo.Passwords", new[] { "UserId" });
            DropTable("dbo.SpecialCodes");
            DropTable("dbo.Users");
            DropTable("dbo.Passwords");
        }
    }
}
