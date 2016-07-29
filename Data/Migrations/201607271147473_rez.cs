namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rez : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        IdNo = c.String(),
                        contactNo = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Allocations",
                c => new
                    {
                        AllocationRefNo = c.Int(nullable: false, identity: true),
                        bookingId = c.Int(nullable: false),
                        residenceCode = c.String(),
                        roomId = c.String(maxLength: 128),
                        RoomType = c.String(),
                        blockCode = c.String(),
                    })
                .PrimaryKey(t => t.AllocationRefNo)
                .ForeignKey("dbo.Bookings", t => t.bookingId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.roomId)
                .Index(t => t.bookingId)
                .Index(t => t.roomId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        bookingId = c.Int(nullable: false, identity: true),
                        studentNo = c.String(maxLength: 128),
                        residenceCode = c.String(maxLength: 128),
                        RoomType = c.String(),
                        roomId = c.String(),
                        Bookingdate = c.DateTime(nullable: false),
                        year = c.String(),
                        blockCode = c.String(),
                        ResCode = c.String(),
                    })
                .PrimaryKey(t => t.bookingId)
                .ForeignKey("dbo.Students", t => t.studentNo)
                .ForeignKey("dbo.Residences", t => t.residenceCode)
                .Index(t => t.studentNo)
                .Index(t => t.residenceCode);
            
            CreateTable(
                "dbo.Registractions",
                c => new
                    {
                        RegNum = c.Int(nullable: false, identity: true),
                        studentNo = c.String(maxLength: 128),
                        bookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegNum)
                .ForeignKey("dbo.Bookings", t => t.bookingId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.studentNo)
                .Index(t => t.studentNo)
                .Index(t => t.bookingId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        studentNo = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        gender = c.String(),
                        DOB = c.String(),
                        emailAddress = c.String(),
                        contactNo = c.Int(nullable: false),
                        blockCode = c.String(),
                        yearOfStudy = c.String(),
                        course = c.String(),
                        physicalAddress = c.String(),
                        registrationYr = c.String(),
                        NoOfModules = c.Int(nullable: false),
                        funder = c.String(),
                        levelOfStudy = c.String(),
                        financialStatus = c.String(),
                    })
                .PrimaryKey(t => t.studentNo);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        visitorId = c.String(nullable: false, maxLength: 128),
                        Fname = c.String(),
                        timeIn = c.DateTime(nullable: false),
                        timeOut = c.DateTime(nullable: false),
                        studentNo = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.visitorId)
                .ForeignKey("dbo.Students", t => t.studentNo)
                .Index(t => t.studentNo);
            
            CreateTable(
                "dbo.Residences",
                c => new
                    {
                        residenceCode = c.String(nullable: false, maxLength: 128),
                        ResName = c.String(),
                        Gender = c.String(),
                        address = c.String(),
                        campus = c.String(),
                        contactNo = c.String(),
                        noOfRooms = c.Int(nullable: false),
                        RoomType = c.String(),
                        capacity = c.Int(nullable: false),
                        Levels = c.String(),
                        Courses = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.residenceCode);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        roomId = c.String(nullable: false, maxLength: 128),
                        beds = c.Int(nullable: false),
                        roomType = c.String(),
                        Quantity = c.Int(nullable: false),
                        status = c.String(),
                        residenceCode = c.String(maxLength: 128),
                        ResCode = c.String(),
                        AllocatedTimes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.roomId)
                .ForeignKey("dbo.Residences", t => t.residenceCode)
                .Index(t => t.residenceCode);
            
            CreateTable(
                "dbo.ApplyingCommands",
                c => new
                    {
                        ApplyingYear = c.Int(nullable: false),
                        ApplyingO_Date = c.DateTime(nullable: false),
                        ApplyingC_Date = c.DateTime(nullable: false),
                        Semester = c.String(),
                    })
                .PrimaryKey(t => t.ApplyingYear);
            
            CreateTable(
                "dbo.Financials",
                c => new
                    {
                        funderId = c.Int(nullable: false, identity: true),
                        funder = c.String(),
                        refNo = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.funderId)
                .ForeignKey("dbo.OnlinePayments", t => t.refNo)
                .Index(t => t.refNo);
            
            CreateTable(
                "dbo.OnlinePayments",
                c => new
                    {
                        RefNo = c.String(nullable: false, maxLength: 128),
                        cardType = c.String(),
                        cardholderName = c.String(),
                        cardNumber = c.String(),
                        ccvno = c.String(),
                        expiryDate = c.String(),
                        amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RefNo);
            
            CreateTable(
                "dbo.OtherUsers",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        Fname = c.String(),
                        Lname = c.String(),
                        gender = c.String(),
                        emailAddress = c.String(),
                        contactNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Financials", "refNo", "dbo.OnlinePayments");
            DropForeignKey("dbo.Rooms", "residenceCode", "dbo.Residences");
            DropForeignKey("dbo.Allocations", "roomId", "dbo.Rooms");
            DropForeignKey("dbo.Bookings", "residenceCode", "dbo.Residences");
            DropForeignKey("dbo.Visitors", "studentNo", "dbo.Students");
            DropForeignKey("dbo.Registractions", "studentNo", "dbo.Students");
            DropForeignKey("dbo.Bookings", "studentNo", "dbo.Students");
            DropForeignKey("dbo.Registractions", "bookingId", "dbo.Bookings");
            DropForeignKey("dbo.Allocations", "bookingId", "dbo.Bookings");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Financials", new[] { "refNo" });
            DropIndex("dbo.Rooms", new[] { "residenceCode" });
            DropIndex("dbo.Visitors", new[] { "studentNo" });
            DropIndex("dbo.Registractions", new[] { "bookingId" });
            DropIndex("dbo.Registractions", new[] { "studentNo" });
            DropIndex("dbo.Bookings", new[] { "residenceCode" });
            DropIndex("dbo.Bookings", new[] { "studentNo" });
            DropIndex("dbo.Allocations", new[] { "roomId" });
            DropIndex("dbo.Allocations", new[] { "bookingId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OtherUsers");
            DropTable("dbo.OnlinePayments");
            DropTable("dbo.Financials");
            DropTable("dbo.ApplyingCommands");
            DropTable("dbo.Rooms");
            DropTable("dbo.Residences");
            DropTable("dbo.Visitors");
            DropTable("dbo.Students");
            DropTable("dbo.Registractions");
            DropTable("dbo.Bookings");
            DropTable("dbo.Allocations");
            DropTable("dbo.Admins");
        }
    }
}
