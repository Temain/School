namespace School.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Prefix = c.String(),
                        Direction = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClassId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        ClassId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.LessonDetails",
                c => new
                    {
                        LessonDetailId = c.Int(nullable: false, identity: true),
                        LessonId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        Comment = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LessonDetailId)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.LessonId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        LessonDate = c.DateTime(nullable: false),
                        DisciplineId = c.Int(nullable: false),
                        HomeWork = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LessonId)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId, cascadeDelete: true)
                .Index(t => t.DisciplineId);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        DisciplineId = c.Int(nullable: false, identity: true),
                        DisciplineName = c.String(),
                        TeacherId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DisciplineId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(),
                        Authors = c.String(),
                        Pages = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        Speciality = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId);
            
            CreateTable(
                "dbo.MaterialDisciplines",
                c => new
                    {
                        Material_MaterialId = c.Int(nullable: false),
                        Discipline_DisciplineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Material_MaterialId, t.Discipline_DisciplineId })
                .ForeignKey("dbo.Materials", t => t.Material_MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplines", t => t.Discipline_DisciplineId, cascadeDelete: true)
                .Index(t => t.Material_MaterialId)
                .Index(t => t.Discipline_DisciplineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LessonDetails", "StudentId", "dbo.Students");
            DropForeignKey("dbo.LessonDetails", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Disciplines", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.MaterialDisciplines", "Discipline_DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.MaterialDisciplines", "Material_MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Lessons", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.Students", "ClassId", "dbo.Classes");
            DropIndex("dbo.MaterialDisciplines", new[] { "Discipline_DisciplineId" });
            DropIndex("dbo.MaterialDisciplines", new[] { "Material_MaterialId" });
            DropIndex("dbo.Disciplines", new[] { "TeacherId" });
            DropIndex("dbo.Lessons", new[] { "DisciplineId" });
            DropIndex("dbo.LessonDetails", new[] { "StudentId" });
            DropIndex("dbo.LessonDetails", new[] { "LessonId" });
            DropIndex("dbo.Students", new[] { "ClassId" });
            DropTable("dbo.MaterialDisciplines");
            DropTable("dbo.Teachers");
            DropTable("dbo.Materials");
            DropTable("dbo.Disciplines");
            DropTable("dbo.Lessons");
            DropTable("dbo.LessonDetails");
            DropTable("dbo.Students");
            DropTable("dbo.Classes");
        }
    }
}
