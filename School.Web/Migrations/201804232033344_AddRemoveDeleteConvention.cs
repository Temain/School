namespace School.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRemoveDeleteConvention : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.LessonDetails", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.LessonDetails", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Lessons", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.Disciplines", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            AddColumn("dbo.Disciplines", "ClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.Disciplines", "ClassId");
            AddForeignKey("dbo.Disciplines", "ClassId", "dbo.Classes", "ClassId");
            AddForeignKey("dbo.Students", "ClassId", "dbo.Classes", "ClassId");
            AddForeignKey("dbo.LessonDetails", "LessonId", "dbo.Lessons", "LessonId");
            AddForeignKey("dbo.LessonDetails", "StudentId", "dbo.Students", "StudentId");
            AddForeignKey("dbo.Lessons", "DisciplineId", "dbo.Disciplines", "DisciplineId");
            AddForeignKey("dbo.Disciplines", "TeacherId", "dbo.Teachers", "TeacherId");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Disciplines", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Lessons", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.LessonDetails", "StudentId", "dbo.Students");
            DropForeignKey("dbo.LessonDetails", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Students", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Disciplines", "ClassId", "dbo.Classes");
            DropIndex("dbo.Disciplines", new[] { "ClassId" });
            DropColumn("dbo.Disciplines", "ClassId");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Disciplines", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
            AddForeignKey("dbo.Lessons", "DisciplineId", "dbo.Disciplines", "DisciplineId", cascadeDelete: true);
            AddForeignKey("dbo.LessonDetails", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
            AddForeignKey("dbo.LessonDetails", "LessonId", "dbo.Lessons", "LessonId", cascadeDelete: true);
            AddForeignKey("dbo.Students", "ClassId", "dbo.Classes", "ClassId", cascadeDelete: true);
        }
    }
}
