namespace School.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttentionField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LessonDetails", "Attention", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LessonDetails", "Attention");
        }
    }
}
